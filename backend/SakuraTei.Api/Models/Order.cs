namespace SakuraTei.Api.Models;

/// <summary>Trạng thái vòng đời của một đơn hàng.</summary>
public enum OrderStatus
{
    /// <summary>Vừa nhận, chờ bếp xác nhận. Chỉ ở trạng thái này khách mới tự huỷ được.</summary>
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

/// <summary>
/// Một dòng món trong đơn hàng, đã chốt giá tại thời điểm đặt. Lưu ở bảng <c>OrderLines</c>
/// và luôn được nạp kèm đơn hàng chứa nó.
/// </summary>
/// <remarks>
/// Là class chứ không phải record: EF Core theo dõi thực thể theo tham chiếu, còn record so sánh
/// theo giá trị nên hai dòng giống hệt nhau sẽ bị coi là một.
/// </remarks>
public sealed class OrderLine
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

/// <summary>Một đơn đặt món, lưu ở bảng <c>Orders</c> trong SQLite.</summary>
public sealed class Order
{
    /// <summary>Mã đơn dạng <c>ST-XXXXXX</c>, khách dùng để tra cứu. Là khoá chính.</summary>
    public required string Code { get; init; }

    public required string CustomerName { get; init; }

    public required string Phone { get; init; }

    public string? Email { get; init; }

    public required string Address { get; init; }

    public string? Note { get; init; }

    public required List<OrderLine> Lines { get; init; }

    /// <summary>Tổng tiền hàng trước phí giao.</summary>
    public required decimal Subtotal { get; init; }

    public required decimal DeliveryFee { get; init; }

    public decimal Total => Subtotal + DeliveryFee;

    /// <summary>Đổi được sau khi tạo — hiện chỉ có thao tác huỷ đơn đổi trạng thái.</summary>
    public OrderStatus Status { get; set; } = OrderStatus.Received;

    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Thời điểm dự kiến giao xong.</summary>
    public required DateTimeOffset EstimatedReadyAt { get; init; }
}
