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
}

/// <summary>Thân phản hồi lỗi rút gọn, đủ cho frontend hiển thị.</summary>
/// <param name="Title">Tiêu đề ngắn.</param>
/// <param name="Detail">Mô tả chi tiết cho người dùng cuối.</param>
/// <param name="Status">Mã HTTP tương ứng.</param>
public sealed record ProblemDetailsBody(string Title, string Detail, int Status);
