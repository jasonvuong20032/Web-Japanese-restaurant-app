namespace SakuraTei.Api.Common;

/// <summary>Kết cục của một thao tác ghi. Endpoint dựa vào đây để chọn mã HTTP.</summary>
public enum ServiceOutcome
{
    /// <summary>Thành công → 200 / 201 / 204 tuỳ thao tác.</summary>
    Success,

    /// <summary>Không có bản ghi mang mã được hỏi → 404.</summary>
    NotFound,

    /// <summary>Dữ liệu gửi lên sai, kèm lỗi theo từng trường → 400.</summary>
    Invalid,

    /// <summary>Dữ liệu hợp lệ nhưng trạng thái hiện tại không cho phép, ví dụ đơn đã huỷ → 409.</summary>
    Conflict,
}

/// <summary>
/// Kết quả trả về từ service cho các thao tác thêm/sửa/xoá. Service chỉ nói chuyện bằng kiểu này,
/// không biết gì về HTTP; việc đổi sang mã HTTP nằm ở <see cref="ApiResults.ToHttpResult{T}"/>.
/// </summary>
public sealed class ServiceResult<T>
{
    private ServiceResult(ServiceOutcome outcome, T? value, IReadOnlyList<(string Field, string Message)> errors, string? message)
    {
        Outcome = outcome;
        Value = value;
        Errors = errors;
        Message = message;
    }

    public ServiceOutcome Outcome { get; }

    /// <summary>Bản ghi sau thao tác; chỉ có giá trị khi <see cref="Outcome"/> là <see cref="ServiceOutcome.Success"/>.</summary>
    public T? Value { get; }

    /// <summary>Lỗi theo từng trường khi <see cref="Outcome"/> là <see cref="ServiceOutcome.Invalid"/>.</summary>
    public IReadOnlyList<(string Field, string Message)> Errors { get; }

    /// <summary>Thông điệp cho người dùng khi không tìm thấy hoặc bị từ chối.</summary>
    public string? Message { get; }

    public static ServiceResult<T> Ok(T value) => new(ServiceOutcome.Success, value, [], null);

    public static ServiceResult<T> NotFound(string message) => new(ServiceOutcome.NotFound, default, [], message);

    public static ServiceResult<T> Invalid(IReadOnlyList<(string Field, string Message)> errors) =>
        new(ServiceOutcome.Invalid, default, errors, null);

    public static ServiceResult<T> Conflict(string message) => new(ServiceOutcome.Conflict, default, [], message);
}
