namespace SakuraTei.Api.Common;

/// <summary>
/// Giờ Việt Nam (UTC+7, không có giờ mùa hè). Máy chủ deploy thường chạy theo UTC, nên dùng
/// <c>DateTimeOffset.Now</c> thì từ 0:00 tới 7:00 sáng giờ Việt Nam máy chủ vẫn tưởng là hôm qua —
/// khách sẽ đặt được bàn cho một ngày đã qua.
/// </summary>
public static class VietnamTime
{
    private static readonly TimeSpan Offset = TimeSpan.FromHours(7);

    public static DateTimeOffset Now => DateTimeOffset.UtcNow.ToOffset(Offset);

    public static DateOnly Today => DateOnly.FromDateTime(Now.DateTime);
}
