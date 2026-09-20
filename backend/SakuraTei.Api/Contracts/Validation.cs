using System.Text.RegularExpressions;

namespace SakuraTei.Api.Contracts;

/// <summary>Các phép kiểm tra dùng chung cho nhiều DTO.</summary>
public static partial class Validation
{
    /// <summary>Số điện thoại Việt Nam: 10 chữ số, bắt đầu bằng 0, cho phép dấu cách và gạch nối.</summary>
    public static bool IsVietnamesePhone(string value)
    {
        var digits = new string([.. value.Where(char.IsDigit)]);
        return digits.Length == 10 && digits[0] == '0';
    }

    /// <summary>Kiểm tra email ở mức đủ dùng: có phần tên, một <c>@</c> và tên miền có dấu chấm.</summary>
    public static bool IsEmail(string value) => EmailPattern().IsMatch(value.Trim());

    [GeneratedRegex(@"^[^@\s]+@[^@\s.]+(\.[^@\s.]+)+$", RegexOptions.CultureInvariant)]
    private static partial Regex EmailPattern();
}
