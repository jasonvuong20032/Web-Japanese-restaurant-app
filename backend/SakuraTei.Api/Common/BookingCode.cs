using System.Security.Cryptography;

namespace SakuraTei.Api.Common;

/// <summary>Mã đơn hàng (<c>ST-</c>) và mã đặt bàn (<c>RS-</c>) mà khách dùng để tra cứu.</summary>
public static class BookingCode
{
    /// <summary>Bỏ 0/O/1/I để khách đọc mã qua điện thoại không nhầm.</summary>
    private const string Alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    private const int Length = 6;

    /// <summary>
    /// Sinh mã dạng <c>ST-A3F9K2</c> chưa có trong database. Khả năng trùng là 1 trên một tỉ,
    /// nhưng mã là khoá chính nên vẫn kiểm tra cho chắc thay vì để lỗi ràng buộc nổ ra lúc lưu.
    /// </summary>
    /// <param name="prefix">Tiền tố, ví dụ <c>ST</c>.</param>
    /// <param name="isTaken">Hỏi database xem mã đã có người dùng chưa.</param>
    public static string NewUnique(string prefix, Func<string, bool> isTaken)
    {
        string code;
        do
        {
            code = New(prefix);
        }
        while (isTaken(code));

        return code;
    }

    /// <summary>
    /// Đưa mã khách gõ về dạng lưu trong database. SQLite so sánh chuỗi phân biệt hoa thường,
    /// nên khách gõ <c>st-a3f9k2</c> vẫn phải tìm ra <c>ST-A3F9K2</c>.
    /// </summary>
    public static string Normalize(string code) => code.Trim().ToUpperInvariant();

    private static string New(string prefix)
    {
        Span<char> chars = stackalloc char[Length];

        for (var i = 0; i < chars.Length; i++)
        {
            chars[i] = Alphabet[RandomNumberGenerator.GetInt32(Alphabet.Length)];
        }

        return $"{prefix}-{new string(chars)}";
    }
}
