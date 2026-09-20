namespace SakuraTei.Api.Models;

/// <summary>Trạng thái vòng đời của một đơn hàng.</summary>
public enum OrderStatus
{
    /// <summary>Vừa nhận, chờ bếp xác nhận.</summary>
    Received,

    /// <summary>Bếp đang chế biến.</summary>
    Preparing,

    /// <summary>Đang giao tới khách.</summary>
    Delivering,

    /// <summary>Đã giao xong.</summary>
    Completed,

    /// <summary>Đã huỷ.</summary>
    Cancelled,
}

/// <summary>Một dòng món trong đơn hàng, đã chốt giá tại thời điểm đặt.</summary>
public sealed record OrderLine
{
    public required string DishId { get; init; }

    public required string DishName { get; init; }

    public required string DishSlug { get; init; }

    public required string ImageUrl { get; init; }

    /// <summary>Đơn giá do server tra từ thực đơn, không lấy theo giá client gửi lên.</summary>
    public required decimal UnitPrice { get; init; }

    public required int Quantity { get; init; }

    public decimal LineTotal => UnitPrice * Quantity;
}

/// <summary>Một đơn đặt món. Chỉ tồn tại trong bộ nhớ, mất khi khởi động lại API.</summary>
public sealed record Order
{
    /// <summary>Mã đơn dạng <c>ST-XXXXXX</c>, khách dùng để tra cứu.</summary>
    public required string Code { get; init; }

    public required string CustomerName { get; init; }

    public required string Phone { get; init; }

    public string? Email { get; init; }

    public required string Address { get; init; }

    public string? Note { get; init; }

    public required IReadOnlyList<OrderLine> Lines { get; init; }

    /// <summary>Tổng tiền hàng trước phí giao.</summary>
    public required decimal Subtotal { get; init; }

    public required decimal DeliveryFee { get; init; }

    public decimal Total => Subtotal + DeliveryFee;

    public OrderStatus Status { get; init; } = OrderStatus.Received;

    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Thời điểm dự kiến giao xong.</summary>
    public required DateTimeOffset EstimatedReadyAt { get; init; }
}
