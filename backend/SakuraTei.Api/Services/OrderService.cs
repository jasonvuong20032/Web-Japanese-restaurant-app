using Microsoft.EntityFrameworkCore;
using SakuraTei.Api.Common;
using SakuraTei.Api.Contracts;
using SakuraTei.Api.Data;
using SakuraTei.Api.Models;

namespace SakuraTei.Api.Services;

public interface IOrderService
{
    /// <summary>Thêm đơn mới; giá lấy từ thực đơn chứ không lấy từ client.</summary>
    ServiceResult<Order> Create(CreateOrderRequest request);

    Order? FindByCode(string code);

    /// <summary>Huỷ đơn khi bếp chưa nhận làm — đổi trạng thái sang <see cref="OrderStatus.Cancelled"/>.</summary>
    ServiceResult<Order> Cancel(string code);
}

/// <summary>Đơn hàng lưu trong bảng <c>Orders</c>, các dòng món trong bảng <c>OrderLines</c>.</summary>
public sealed class OrderService(
    IDbContextFactory<SakuraDbContext> dbFactory,
    IDishService dishService,
    ILogger<OrderService> logger) : IOrderService
{
    /// <summary>Phí giao cố định; miễn phí khi đơn đạt <see cref="FreeDeliveryThreshold"/>.</summary>
    private const decimal DeliveryFee = 25_000m;

    private const decimal FreeDeliveryThreshold = 500_000m;

    public ServiceResult<Order> Create(CreateOrderRequest request)
    {
        var errors = request.Validate();
        if (errors.Count > 0)
        {
            return ServiceResult<Order>.Invalid(errors);
        }

        // Đã qua Validate nên Items chắc chắn khác null và không rỗng.
        var requestItems = request.Items!;
        var lines = new List<OrderLine>(requestItems.Count);

        for (var i = 0; i < requestItems.Count; i++)
        {
            var item = requestItems[i];
            var dish = dishService.FindById(item.DishId!);

            if (dish is null)
            {
                errors.Add(($"Items[{i}].DishId", $"Không tìm thấy món có mã '{item.DishId}'."));
                continue;
            }

            if (!dish.IsAvailable)
            {
                errors.Add(($"Items[{i}].DishId", $"Món '{dish.Name}' hiện đã hết, vui lòng bỏ khỏi giỏ hàng."));
                continue;
            }

            lines.Add(new OrderLine
            {
                DishId = dish.Id,
                DishName = dish.Name,
                DishSlug = dish.Slug,
                ImageUrl = dish.ImageUrl,
                // Lấy giá từ thực đơn chứ không tin giá client gửi lên.
                UnitPrice = dish.Price,
                Quantity = item.Quantity,
            });
        }

        if (errors.Count > 0)
        {
            return ServiceResult<Order>.Invalid(errors);
        }

        using var db = dbFactory.CreateDbContext();

        var subtotal = lines.Sum(l => l.LineTotal);
        var now = VietnamTime.Now;

        var order = new Order
        {
            Code = BookingCode.NewUnique("ST", code => db.Orders.Any(o => o.Code == code)),
            CustomerName = request.CustomerName!.Trim(),
            Phone = request.Phone!.Trim(),
            Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim(),
            Address = request.Address!.Trim(),
            Note = string.IsNullOrWhiteSpace(request.Note) ? null : request.Note.Trim(),
            Lines = lines,
            Subtotal = subtotal,
            DeliveryFee = subtotal >= FreeDeliveryThreshold ? 0m : DeliveryFee,
            CreatedAt = now,
            // Thời gian chế biến lấy theo món lâu nhất, cộng 20 phút giao hàng.
            EstimatedReadyAt = now.AddMinutes(lines.Max(l => GetPrepMinutes(l.DishId)) + 20),
        };

        // Đơn và các dòng món được ghi trong cùng một transaction.
        db.Orders.Add(order);
        db.SaveChanges();

        logger.LogInformation(
            "Đơn hàng mới {OrderCode}: {LineCount} món, tổng {Total:N0}đ",
            order.Code, lines.Count, order.Total);

        return ServiceResult<Order>.Ok(order);
    }

    public Order? FindByCode(string code)
    {
        using var db = dbFactory.CreateDbContext();
        var normalized = BookingCode.Normalize(code);

        // Dòng món là kiểu "owned" nên tự được nạp kèm, không cần Include.
        return db.Orders.AsNoTracking().FirstOrDefault(o => o.Code == normalized);
    }

    public ServiceResult<Order> Cancel(string code)
    {
        using var db = dbFactory.CreateDbContext();
        var normalized = BookingCode.Normalize(code);

        var order = db.Orders.FirstOrDefault(o => o.Code == normalized);
        if (order is null)
        {
            return ServiceResult<Order>.NotFound($"Không có đơn hàng nào mang mã '{code}'.");
        }

        if (order.Status == OrderStatus.Cancelled)
        {
            return ServiceResult<Order>.Conflict("Đơn hàng này đã được huỷ trước đó.");
        }

        if (order.Status != OrderStatus.Received)
        {
            return ServiceResult<Order>.Conflict(
                "Bếp đã bắt đầu làm món nên không huỷ trên website được nữa. Vui lòng gọi 1900 6868.");
        }

        order.Status = OrderStatus.Cancelled;
        db.SaveChanges();

        logger.LogInformation("Huỷ đơn hàng {OrderCode}", order.Code);

        return ServiceResult<Order>.Ok(order);
    }

    private int GetPrepMinutes(string dishId) => dishService.FindById(dishId)?.PrepMinutes ?? 15;
}
