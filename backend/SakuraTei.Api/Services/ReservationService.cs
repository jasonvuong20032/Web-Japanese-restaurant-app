using Microsoft.EntityFrameworkCore;
using SakuraTei.Api.Common;
using SakuraTei.Api.Contracts;
using SakuraTei.Api.Data;
using SakuraTei.Api.Models;

namespace SakuraTei.Api.Services;

public interface IReservationService
{
    /// <summary>Bảng khung giờ của một ngày, kèm số chỗ còn nhận.</summary>
    AvailabilityResponse GetAvailability(DateOnly date);

    Reservation? FindByCode(string code);

    /// <summary>Thêm lượt đặt mới nếu khung giờ còn đủ chỗ.</summary>
    ServiceResult<Reservation> Create(ReservationRequest request);

    /// <summary>Đổi thông tin, ngày giờ hoặc số khách; kiểm tra lại chỗ trống như đặt mới.</summary>
    ServiceResult<Reservation> Update(string code, ReservationRequest request);

    /// <summary>Xoá lượt đặt, trả lại chỗ cho khung giờ đó.</summary>
    ServiceResult<Reservation> Delete(string code);
}

/// <summary>
/// Đặt bàn lưu trong bảng <c>Reservations</c>. Số chỗ còn lại không lưu riêng mà tính từ chính
/// các lượt đặt, nên sửa hay xoá một lượt là chỗ trống tự đúng theo.
/// </summary>
public sealed class ReservationService(
    IDbContextFactory<SakuraDbContext> dbFactory,
    ILogger<ReservationService> logger) : IReservationService
{
    /// <summary>Số chỗ ngồi quán nhận cho mỗi khung giờ 30 phút.</summary>
    private const int SeatsPerSlot = 24;

    /// <summary>Quán mở 11:00 và nhận khách cuối lúc 21:00.</summary>
    private static readonly TimeOnly OpenTime = new(11, 0);

    private static readonly TimeOnly LastSeating = new(21, 0);

    /// <summary>
    /// Khoá ghi, để bước "đếm chỗ còn trống" và bước "lưu" diễn ra liền mạch. Thiếu khoá này thì
    /// hai khách bấm đặt cùng lúc có thể cùng thấy còn chỗ và cùng được nhận, vượt sức chứa.
    /// Chỉ đúng khi API chạy một instance — xem DEPLOY.md.
    /// </summary>
    private readonly Lock _writeLock = new();

    public AvailabilityResponse GetAvailability(DateOnly date)
    {
        using var db = dbFactory.CreateDbContext();
        var booked = BookedSeatsBySlot(db, date);

        var slots = EnumerateSlots()
            .Select(time =>
            {
                var remaining = Math.Max(0, SeatsPerSlot - booked.GetValueOrDefault(time));
                return new TimeSlot(time.ToString("HH:mm"), remaining, remaining > 0);
            })
            .ToList();

        return new AvailabilityResponse(date, slots);
    }

    public Reservation? FindByCode(string code)
    {
        using var db = dbFactory.CreateDbContext();
        var normalized = BookingCode.Normalize(code);

        return db.Reservations.AsNoTracking().FirstOrDefault(r => r.Code == normalized);
    }

    public ServiceResult<Reservation> Create(ReservationRequest request)
    {
        var errors = Validate(request, out var date, out var time);
        if (errors.Count > 0)
        {
            return ServiceResult<Reservation>.Invalid(errors);
        }

        lock (_writeLock)
        {
            using var db = dbFactory.CreateDbContext();

            if (CheckCapacity(db, date, time, request.PartySize, exceptCode: null) is { } full)
            {
                return ServiceResult<Reservation>.Invalid([full]);
            }

            var reservation = new Reservation
            {
                Code = BookingCode.NewUnique("RS", code => db.Reservations.Any(r => r.Code == code)),
                CustomerName = request.CustomerName!.Trim(),
                Phone = request.Phone!.Trim(),
                Email = TrimToNull(request.Email),
                Date = date,
                Time = time,
                PartySize = request.PartySize,
                Note = TrimToNull(request.Note),
                CreatedAt = VietnamTime.Now,
            };

            db.Reservations.Add(reservation);
            db.SaveChanges();

            logger.LogInformation(
                "Đặt bàn mới {ReservationCode}: {PartySize} khách lúc {Date} {Time}",
                reservation.Code, reservation.PartySize, date, time);

            return ServiceResult<Reservation>.Ok(reservation);
        }
    }

    public ServiceResult<Reservation> Update(string code, ReservationRequest request)
    {
        lock (_writeLock)
        {
            using var db = dbFactory.CreateDbContext();
            var normalized = BookingCode.Normalize(code);

            var reservation = db.Reservations.FirstOrDefault(r => r.Code == normalized);
            if (reservation is null)
            {
                return ServiceResult<Reservation>.NotFound(NotFoundMessage(code));
            }

            if (HasPassed(reservation))
            {
                return ServiceResult<Reservation>.Conflict("Lượt đặt này đã qua giờ hẹn nên không đổi được nữa.");
            }

            var errors = Validate(request, out var date, out var time);
            if (errors.Count > 0)
            {
                return ServiceResult<Reservation>.Invalid(errors);
            }

            // Bỏ qua chính lượt đang sửa khi đếm chỗ: giữ nguyên giờ mà tăng từ 4 lên 6 khách
            // thì chỉ cần thêm 2 chỗ, không phải 6.
            if (CheckCapacity(db, date, time, request.PartySize, exceptCode: reservation.Code) is { } full)
            {
                return ServiceResult<Reservation>.Invalid([full]);
            }

            reservation.CustomerName = request.CustomerName!.Trim();
            reservation.Phone = request.Phone!.Trim();
            reservation.Email = TrimToNull(request.Email);
            reservation.Date = date;
            reservation.Time = time;
            reservation.PartySize = request.PartySize;
            reservation.Note = TrimToNull(request.Note);
            reservation.UpdatedAt = VietnamTime.Now;

            db.SaveChanges();

            logger.LogInformation(
                "Đổi lịch {ReservationCode}: {PartySize} khách lúc {Date} {Time}",
                reservation.Code, reservation.PartySize, date, time);

            return ServiceResult<Reservation>.Ok(reservation);
        }
    }

    public ServiceResult<Reservation> Delete(string code)
    {
        lock (_writeLock)
        {
            using var db = dbFactory.CreateDbContext();
            var normalized = BookingCode.Normalize(code);

            var reservation = db.Reservations.FirstOrDefault(r => r.Code == normalized);
            if (reservation is null)
            {
                return ServiceResult<Reservation>.NotFound(NotFoundMessage(code));
            }

            if (HasPassed(reservation))
            {
                return ServiceResult<Reservation>.Conflict("Lượt đặt này đã qua giờ hẹn nên không huỷ được nữa.");
            }

            db.Reservations.Remove(reservation);
            db.SaveChanges();

            logger.LogInformation("Huỷ đặt bàn {ReservationCode}", reservation.Code);

            return ServiceResult<Reservation>.Ok(reservation);
        }
    }

    /// <summary>Kiểm tra dữ liệu tự thân và khung giờ có nằm trong giờ nhận khách không.</summary>
    private static List<(string Field, string Message)> Validate(
        ReservationRequest request,
        out DateOnly date,
        out TimeOnly time)
    {
        date = default;
        time = default;

        var errors = request.Validate(VietnamTime.Today);
        if (errors.Count > 0)
        {
            return errors;
        }

        // Validate đã bảo đảm hai giá trị này hợp lệ.
        date = request.Date!.Value;
        time = TimeOnly.ParseExact(request.Time!, "HH:mm");

        if (!EnumerateSlots().Contains(time))
        {
            errors.Add((nameof(request.Time),
                $"Quán chỉ nhận khách từ {OpenTime:HH\\:mm} đến {LastSeating:HH\\:mm}, mỗi 30 phút một khung."));
        }

        return errors;
    }

    /// <summary>Trả lỗi nếu khung giờ không còn đủ chỗ, <c>null</c> nếu đủ.</summary>
    private static (string Field, string Message)? CheckCapacity(
        SakuraDbContext db,
        DateOnly date,
        TimeOnly time,
        int partySize,
        string? exceptCode)
    {
        var remaining = SeatsPerSlot - BookedSeatsBySlot(db, date, exceptCode).GetValueOrDefault(time);

        if (remaining >= partySize)
        {
            return null;
        }

        return (nameof(ReservationRequest.Time), remaining <= 0
            ? "Khung giờ này đã kín chỗ, vui lòng chọn giờ khác."
            : $"Khung giờ này chỉ còn {remaining} chỗ, không đủ cho {partySize} khách.");
    }

    /// <summary>Tổng số khách đã đặt của từng khung giờ trong một ngày.</summary>
    private static Dictionary<TimeOnly, int> BookedSeatsBySlot(SakuraDbContext db, DateOnly date, string? exceptCode = null)
    {
        var query = db.Reservations.AsNoTracking().Where(r => r.Date == date);

        if (exceptCode is not null)
        {
            query = query.Where(r => r.Code != exceptCode);
        }

        // Một ngày tối đa vài trăm lượt, gom nhóm phía ứng dụng cho chắc chắn dịch được sang SQL.
        return query
            .Select(r => new { r.Time, r.PartySize })
            .AsEnumerable()
            .GroupBy(r => r.Time)
            .ToDictionary(g => g.Key, g => g.Sum(r => r.PartySize));
    }

    private static bool HasPassed(Reservation reservation) =>
        reservation.Date.ToDateTime(reservation.Time) < VietnamTime.Now.DateTime;

    private static string NotFoundMessage(string code) => $"Không có lượt đặt bàn nào mang mã '{code}'.";

    private static string? TrimToNull(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    /// <summary>Các khung giờ nhận khách, cách nhau 30 phút.</summary>
    private static IEnumerable<TimeOnly> EnumerateSlots()
    {
        for (var t = OpenTime; t <= LastSeating; t = t.AddMinutes(30))
        {
            yield return t;
        }
    }
}
