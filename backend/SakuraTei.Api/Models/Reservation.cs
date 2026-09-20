namespace SakuraTei.Api.Models;

/// <summary>Một lượt đặt bàn tại quán. Chỉ tồn tại trong bộ nhớ.</summary>
public sealed record Reservation
{
    /// <summary>Mã đặt bàn dạng <c>RS-XXXXXX</c>.</summary>
    public required string Code { get; init; }

    public required string CustomerName { get; init; }

    public required string Phone { get; init; }

    public string? Email { get; init; }

    /// <summary>Ngày đến, không kèm giờ.</summary>
    public required DateOnly Date { get; init; }

    /// <summary>Khung giờ đã chọn, ví dụ <c>18:30</c>.</summary>
    public required TimeOnly Time { get; init; }

    public required int PartySize { get; init; }

    public string? Note { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
}

/// <summary>Tình trạng còn chỗ của một khung giờ trong ngày.</summary>
/// <param name="Time">Khung giờ, định dạng <c>HH:mm</c>.</param>
/// <param name="RemainingSeats">Số chỗ còn nhận.</param>
/// <param name="IsAvailable">Còn nhận đặt hay không.</param>
public sealed record TimeSlot(string Time, int RemainingSeats, bool IsAvailable);
