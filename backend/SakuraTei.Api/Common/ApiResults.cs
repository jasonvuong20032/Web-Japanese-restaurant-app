using Microsoft.AspNetCore.Http.HttpResults;

namespace SakuraTei.Api.Common;

/// <summary>Kết quả phân trang dùng chung cho mọi danh sách dài.</summary>
/// <param name="Items">Dữ liệu của trang hiện tại.</param>
/// <param name="Page">Số trang, bắt đầu từ 1.</param>
/// <param name="PageSize">Số phần tử mỗi trang.</param>
/// <param name="TotalItems">Tổng số phần tử khớp điều kiện lọc.</param>
public sealed record PagedResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalItems)
{
    /// <summary>Tổng số trang, tối thiểu là 1 để frontend không phải xử lý trường hợp 0.</summary>
    public int TotalPages => TotalItems == 0 ? 1 : (int)Math.Ceiling(TotalItems / (double)PageSize);

    public bool HasPrevious => Page > 1;

    public bool HasNext => Page < TotalPages;
}

/// <summary>Các helper dựng phản hồi lỗi theo chuẩn RFC 7807 (ProblemDetails).</summary>
public static class ApiResults
{
    /// <summary>Trả 400 kèm danh sách lỗi theo từng trường.</summary>
    public static ValidationProblem Validation(IEnumerable<(string Field, string Message)> errors)
    {
        var dictionary = errors
            .GroupBy(e => e.Field, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.Message).ToArray(),
                StringComparer.OrdinalIgnoreCase);

        return TypedResults.ValidationProblem(dictionary, title: "Dữ liệu gửi lên không hợp lệ");
    }

    /// <summary>Trả 404 với thông điệp tiếng Việt thống nhất.</summary>
    public static NotFound<ProblemDetailsBody> NotFound(string message) =>
        TypedResults.NotFound(new ProblemDetailsBody("Không tìm thấy", message, 404));

    /// <summary>Trả 409 khi yêu cầu hợp lệ nhưng trạng thái hiện tại không cho làm, ví dụ huỷ đơn đã huỷ.</summary>
    public static Conflict<ProblemDetailsBody> Conflict(string message) =>
        TypedResults.Conflict(new ProblemDetailsBody("Không thực hiện được", message, 409));

    /// <summary>
    /// Đổi kết quả của service sang phản hồi HTTP. Mọi endpoint ghi dữ liệu đi qua đây nên
    /// 400 / 404 / 409 luôn cùng một hình dạng, và frontend chỉ cần một cách đọc lỗi.
    /// </summary>
    /// <param name="result">Kết quả từ service.</param>
    /// <param name="onSuccess">Dựng phản hồi khi thành công, ví dụ 201 kèm <c>Location</c>.</param>
    public static IResult ToHttpResult<T>(this ServiceResult<T> result, Func<T, IResult> onSuccess) =>
        result.Outcome switch
        {
            ServiceOutcome.Success => onSuccess(result.Value!),
            ServiceOutcome.NotFound => NotFound(result.Message!),
            ServiceOutcome.Invalid => Validation(result.Errors),
            ServiceOutcome.Conflict => Conflict(result.Message!),
            _ => throw new ArgumentOutOfRangeException(nameof(result), result.Outcome, "Kết cục không xác định."),
        };
}

/// <summary>Thân phản hồi lỗi rút gọn, đủ cho frontend hiển thị.</summary>
/// <param name="Title">Tiêu đề ngắn.</param>
/// <param name="Detail">Mô tả chi tiết cho người dùng cuối.</param>
/// <param name="Status">Mã HTTP tương ứng.</param>
public sealed record ProblemDetailsBody(string Title, string Detail, int Status);
