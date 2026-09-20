using System.Collections.Concurrent;
using System.Security.Cryptography;
using SakuraTei.Api.Contracts;
using SakuraTei.Api.Models;

namespace SakuraTei.Api.Services;

/// <summary>Kết quả tạo đơn: hoặc thành công, hoặc kèm danh sách lỗi theo trường.</summary>
/// <param name="Order">Đơn đã tạo, <c>null</c> khi thất bại.</param>
/// <param name="Errors">Lỗi nghiệp vụ, rỗng khi thành công.</param>
public sealed record CreateOrderResult(Order? Order, IReadOnlyList<(string Field, string Message)> Errors)
{
    public bool Succeeded => Order is not null;
}

public interface IOrderService
{
    CreateOrderResult Create(CreateOrderRequest request);

    Order? FindByCode(string code);
}

/// <summary>
/// Giữ đơn hàng trong bộ nhớ tiến trình. Dữ liệu mất khi khởi động lại API — đúng phạm vi
/// bản này (chưa gắn cơ sở dữ liệu). Khi thay bằng EF Core, chỉ cần thay lớp cài đặt.
/// </summary>
public sealed class OrderService(IDishService dishService, ILogger<OrderService> logger) : IOrderService
{
    /// <summary>Phí giao cố định; miễn phí khi đơn đạt <see cref="FreeDeliveryThreshold"/>.</summary>
    private const decimal DeliveryFee = 25_000m;

    private const decimal FreeDeliveryThreshold = 500_000m;

    private readonly ConcurrentDictionary<string, Order> _orders = new(StringComparer.OrdinalIgnoreCase);

    public CreateOrderResult Create(CreateOrderRequest request)
    {
        var errors = request.Validate();
        if (errors.Count > 0)
        {
            return new CreateOrderResult(null, errors);
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
            return new CreateOrderResult(null, errors);
        }

        var subtotal = lines.Sum(l => l.LineTotal);
        var now = DateTimeOffset.Now;

        var order = new Order
        {
            Code = GenerateCode("ST"),
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

        _orders[order.Code] = order;
        logger.LogInformation(
            "Đơn hàng mới {OrderCode}: {LineCount} món, tổng {Total:N0}đ",
            order.Code, lines.Count, order.Total);

        return new CreateOrderResult(order, []);
    }

    public Order? FindByCode(string code) => _orders.GetValueOrDefault(code);

    private int GetPrepMinutes(string dishId) => dishService.FindById(dishId)?.PrepMinutes ?? 15;

    /// <summary>
    /// Sinh mã dạng <c>ST-A3F9K2</c>. Dùng bảng ký tự đã bỏ 0/O/1/I để khách đọc qua điện thoại không nhầm.
    /// </summary>
    internal static string GenerateCode(string prefix)
    {
        const string alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        var chars = new char[6];

        for (var i = 0; i < chars.Length; i++)
        {
            chars[i] = alphabet[RandomNumberGenerator.GetInt32(alphabet.Length)];
        }

        return $"{prefix}-{new string(chars)}";
    }
}
