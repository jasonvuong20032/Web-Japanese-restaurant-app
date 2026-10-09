namespace SakuraTei.Api.Models;

/// <summary>
/// Một lượt đặt bàn tại quán, lưu ở bảng <c>Reservations</c> trong SQLite. Khách đổi được
/// thông tin, ngày giờ và số khách; huỷ thì bản ghi bị xoá hẳn và trả lại chỗ cho khung giờ đó.
/// </summary>
public sealed class Reservation
{
    /// <summary>Mã đặt bàn dạng <c>RS-XXXXXX</c>. Là khoá chính.</summary>
    public required string Code { get; init; }

    public required string CustomerName { get; set; }

    public required string Phone { get; set; }

    public string? Email { get; set; }

    /// <summary>Ngày đến, không kèm giờ.</summary>
    public required DateOnly Date { get; set; }

    /// <summary>Khung giờ đã chọn, ví dụ <c>18:30</c>.</summary>
    public required TimeOnly Time { get; set; }

    public required int PartySize { get; set; }

    public string? Note { get; set; }

    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Lần đổi lịch gần nhất; <c>null</c> nếu chưa đổi lần nào.</summary>
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>Tình trạng còn chỗ của một khung giờ trong ngày.</summary>
/// <param name="Time">Khung giờ, định dạng <c>HH:mm</c>.</param>
/// <param name="RemainingSeats">Số chỗ còn nhận.</param>
/// <param name="IsAvailable">Còn nhận đặt hay không.</param>
public sealed record TimeSlot(string Time, int RemainingSeats, bool IsAvailable);
