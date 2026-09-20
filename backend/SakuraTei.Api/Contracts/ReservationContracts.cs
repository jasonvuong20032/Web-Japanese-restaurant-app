using SakuraTei.Api.Models;

namespace SakuraTei.Api.Contracts;

/// <summary>Yêu cầu đặt bàn.</summary>
public sealed record CreateReservationRequest(
    string? CustomerName,
    string? Phone,
    string? Email,
    DateOnly? Date,
    string? Time,
    int PartySize,
    string? Note)
{
    /// <summary>
    /// Kiểm tra dữ liệu tự thân. Việc khung giờ còn chỗ hay không do
    /// <c>ReservationService</c> xử lý vì cần đọc các lượt đặt hiện có.
    /// </summary>
    /// <param name="today">Ngày hiện tại theo giờ Việt Nam, truyền vào để hàm thuần tuý và dễ test.</param>
    public List<(string Field, string Message)> Validate(DateOnly today)
    {
        var errors = new List<(string, string)>();

        if (string.IsNullOrWhiteSpace(CustomerName))
        {
            errors.Add((nameof(CustomerName), "Vui lòng nhập họ tên người đặt."));
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

        if (Date is null)
        {
            errors.Add((nameof(Date), "Vui lòng chọn ngày đến."));
        }
        else if (Date.Value < today)
        {
            errors.Add((nameof(Date), "Không thể đặt bàn cho ngày đã qua."));
        }
        else if (Date.Value > today.AddDays(60))
        {
            errors.Add((nameof(Date), "Quán chỉ nhận đặt trước tối đa 60 ngày."));
        }

        if (string.IsNullOrWhiteSpace(Time))
        {
            errors.Add((nameof(Time), "Vui lòng chọn khung giờ."));
        }
        else if (!TimeOnly.TryParseExact(Time, "HH:mm", out _))
        {
            errors.Add((nameof(Time), "Khung giờ phải có dạng HH:mm, ví dụ 18:30."));
        }

        if (PartySize is < 1 or > 12)
        {
            errors.Add((nameof(PartySize), "Số khách phải từ 1 đến 12. Nhóm đông hơn vui lòng gọi hotline."));
        }

        if (Note is { Length: > 500 })
        {
            errors.Add((nameof(Note), "Ghi chú tối đa 500 ký tự."));
        }

        return errors;
    }
}

/// <summary>Lượt đặt bàn trả về cho client.</summary>
public sealed record ReservationResponse(
    string Code,
    string CustomerName,
    string Phone,
    string? Email,
    DateOnly Date,
    string Time,
    int PartySize,
    string? Note,
    DateTimeOffset CreatedAt)
{
    public static ReservationResponse From(Reservation r) => new(
        r.Code,
        r.CustomerName,
        r.Phone,
        r.Email,
        r.Date,
        r.Time.ToString("HH:mm"),
        r.PartySize,
        r.Note,
        r.CreatedAt);
}

/// <summary>Bảng khung giờ của một ngày.</summary>
/// <param name="Date">Ngày được hỏi.</param>
/// <param name="Slots">Danh sách khung giờ kèm số chỗ còn lại.</param>
public sealed record AvailabilityResponse(DateOnly Date, IReadOnlyList<TimeSlot> Slots);
