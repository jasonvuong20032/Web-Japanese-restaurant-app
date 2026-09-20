using System.Collections.Concurrent;
using SakuraTei.Api.Contracts;
using SakuraTei.Api.Models;

namespace SakuraTei.Api.Services;

/// <summary>Kết quả đặt bàn: hoặc thành công, hoặc kèm lỗi theo trường.</summary>
public sealed record CreateReservationResult(
    Reservation? Reservation,
    IReadOnlyList<(string Field, string Message)> Errors)
{
    public bool Succeeded => Reservation is not null;
}

public interface IReservationService
{
    /// <summary>Bảng khung giờ của một ngày, kèm số chỗ còn nhận.</summary>
    AvailabilityResponse GetAvailability(DateOnly date);

    CreateReservationResult Create(CreateReservationRequest request);

    Reservation? FindByCode(string code);
}

/// <summary>
/// Quản lý đặt bàn trong bộ nhớ. Sức chứa mỗi khung giờ là cố định; khi hết chỗ thì
/// khung giờ đó bị đánh dấu không khả dụng.
/// </summary>
public sealed class ReservationService(ILogger<ReservationService> logger) : IReservationService
{
    /// <summary>Số chỗ ngồi quán nhận cho mỗi khung giờ 30 phút.</summary>
    private const int SeatsPerSlot = 24;

    /// <summary>Quán mở 11:00 và nhận khách cuối lúc 21:00.</summary>
    private static readonly TimeOnly OpenTime = new(11, 0);

    private static readonly TimeOnly LastSeating = new(21, 0);

    private readonly ConcurrentDictionary<string, Reservation> _byCode = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>Số chỗ đã đặt, khoá là "yyyy-MM-dd HH:mm".</summary>
    private readonly ConcurrentDictionary<string, int> _bookedSeats = new(StringComparer.Ordinal);

    /// <summary>Khoá ghi, để kiểm tra chỗ trống và trừ chỗ diễn ra liền mạch.</summary>
    private readonly Lock _writeLock = new();

    public AvailabilityResponse GetAvailability(DateOnly date)
    {
        var slots = new List<TimeSlot>();

        foreach (var time in EnumerateSlots())
        {
            var booked = _bookedSeats.GetValueOrDefault(SlotKey(date, time));
            var remaining = Math.Max(0, SeatsPerSlot - booked);
            slots.Add(new TimeSlot(time.ToString("HH:mm"), remaining, remaining > 0));
        }

        return new AvailabilityResponse(date, slots);
    }

    public CreateReservationResult Create(CreateReservationRequest request)
    {
        var today = DateOnly.FromDateTime(DateTimeOffset.Now.DateTime);
        var errors = request.Validate(today);
        if (errors.Count > 0)
        {
            return new CreateReservationResult(null, errors);
        }

        // Validate đã bảo đảm hai giá trị này hợp lệ.
        var date = request.Date!.Value;
        var time = TimeOnly.ParseExact(request.Time!, "HH:mm");

        if (!EnumerateSlots().Contains(time))
        {
            errors.Add((nameof(request.Time),
                $"Quán chỉ nhận khách từ {OpenTime:HH\\:mm} đến {LastSeating:HH\\:mm}, mỗi 30 phút một khung."));
            return new CreateReservationResult(null, errors);
        }

        var key = SlotKey(date, time);
        Reservation reservation;

        lock (_writeLock)
        {
            var booked = _bookedSeats.GetValueOrDefault(key);
            var remaining = SeatsPerSlot - booked;

            if (remaining < request.PartySize)
            {
                errors.Add((nameof(request.Time), remaining <= 0
                    ? "Khung giờ này đã kín chỗ, vui lòng chọn giờ khác."
                    : $"Khung giờ này chỉ còn {remaining} chỗ, không đủ cho {request.PartySize} khách."));
                return new CreateReservationResult(null, errors);
            }

            reservation = new Reservation
            {
                Code = OrderService.GenerateCode("RS"),
                CustomerName = request.CustomerName!.Trim(),
                Phone = request.Phone!.Trim(),
                Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim(),
                Date = date,
                Time = time,
                PartySize = request.PartySize,
                Note = string.IsNullOrWhiteSpace(request.Note) ? null : request.Note.Trim(),
                CreatedAt = DateTimeOffset.Now,
            };

            _bookedSeats[key] = booked + request.PartySize;
            _byCode[reservation.Code] = reservation;
        }

        logger.LogInformation(
            "Đặt bàn mới {ReservationCode}: {PartySize} khách lúc {Date} {Time}",
            reservation.Code, reservation.PartySize, date, request.Time);

        return new CreateReservationResult(reservation, []);
    }

    public Reservation? FindByCode(string code) => _byCode.GetValueOrDefault(code);

    /// <summary>Các khung giờ nhận khách, cách nhau 30 phút.</summary>
    private static IEnumerable<TimeOnly> EnumerateSlots()
    {
        for (var t = OpenTime; t <= LastSeating; t = t.AddMinutes(30))
        {
            yield return t;
        }
    }

    private static string SlotKey(DateOnly date, TimeOnly time) => $"{date:yyyy-MM-dd} {time:HH\\:mm}";
}
