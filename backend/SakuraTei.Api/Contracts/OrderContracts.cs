using SakuraTei.Api.Models;

namespace SakuraTei.Api.Contracts;

/// <summary>Một dòng giỏ hàng client gửi lên. Giá không nằm ở đây — server tự tra.</summary>
/// <param name="DishId">Id món, khớp <see cref="Dish.Id"/>.</param>
/// <param name="Quantity">Số lượng, từ 1 đến 50.</param>
public sealed record CartLineRequest(string? DishId, int Quantity);

/// <summary>Yêu cầu tạo đơn hàng.</summary>
public sealed record CreateOrderRequest(
    string? CustomerName,
    string? Phone,
    string? Email,
    string? Address,
    string? Note,
    IReadOnlyList<CartLineRequest>? Items)
{
    /// <summary>
    /// Kiểm tra các ràng buộc không cần tới thực đơn. Việc đối chiếu món có tồn tại hay không
    /// do <c>OrderService</c> làm, vì chỉ nó mới biết thực đơn.
    /// </summary>
    public List<(string Field, string Message)> Validate()
    {
        var errors = new List<(string, string)>();

        if (string.IsNullOrWhiteSpace(CustomerName))
        {
            errors.Add((nameof(CustomerName), "Vui lòng nhập họ tên người nhận."));
        }
        else if (CustomerName.Trim().Length < 2)
        {
            errors.Add((nameof(CustomerName), "Họ tên phải có ít nhất 2 ký tự."));
        }

        if (string.IsNullOrWhiteSpace(Phone))
        {
            errors.Add((nameof(Phone), "Vui lòng nhập số điện thoại."));
        }
        else if (!Validation.IsVietnamesePhone(Phone))
        {
            errors.Add((nameof(Phone), "Số điện thoại không hợp lệ (cần 10 chữ số, bắt đầu bằng 0)."));
        }

        if (!string.IsNullOrWhiteSpace(Email) && !Validation.IsEmail(Email))
        {
            errors.Add((nameof(Email), "Email không hợp lệ."));
        }

        if (string.IsNullOrWhiteSpace(Address))
        {
            errors.Add((nameof(Address), "Vui lòng nhập địa chỉ giao hàng."));
        }
        else if (Address.Trim().Length < 8)
        {
            errors.Add((nameof(Address), "Địa chỉ quá ngắn, vui lòng ghi rõ số nhà và đường."));
        }

        if (Note is { Length: > 500 })
        {
            errors.Add((nameof(Note), "Ghi chú tối đa 500 ký tự."));
        }

        if (Items is null || Items.Count == 0)
        {
            errors.Add((nameof(Items), "Giỏ hàng đang trống."));
        }
        else
        {
            if (Items.Count > 50)
            {
                errors.Add((nameof(Items), "Mỗi đơn chỉ nhận tối đa 50 loại món."));
            }

            for (var i = 0; i < Items.Count; i++)
            {
                var line = Items[i];
                if (string.IsNullOrWhiteSpace(line.DishId))
                {
                    errors.Add(($"Items[{i}].DishId", "Thiếu mã món."));
                }

                if (line.Quantity is < 1 or > 50)
                {
                    errors.Add(($"Items[{i}].Quantity", "Số lượng phải từ 1 đến 50."));
                }
            }
        }

        return errors;
    }
}

/// <summary>Một dòng món trong phản hồi đơn hàng.</summary>
public sealed record OrderLineResponse(
    string DishId,
    string DishName,
    string DishSlug,
    string ImageUrl,
    decimal UnitPrice,
    int Quantity,
    decimal LineTotal);

/// <summary>Đơn hàng trả về cho client sau khi tạo hoặc tra cứu.</summary>
public sealed record OrderResponse(
    string Code,
    string CustomerName,
    string Phone,
    string? Email,
    string Address,
    string? Note,
    IReadOnlyList<OrderLineResponse> Items,
    decimal Subtotal,
    decimal DeliveryFee,
    decimal Total,
    string Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset EstimatedReadyAt)
{
    public static OrderResponse From(Order o) => new(
        o.Code,
        o.CustomerName,
        o.Phone,
        o.Email,
        o.Address,
        o.Note,
        [.. o.Lines.Select(l => new OrderLineResponse(
            l.DishId, l.DishName, l.DishSlug, l.ImageUrl, l.UnitPrice, l.Quantity, l.LineTotal))],
        o.Subtotal,
        o.DeliveryFee,
        o.Total,
        o.Status.ToString(),
        o.CreatedAt,
        o.EstimatedReadyAt);
}
