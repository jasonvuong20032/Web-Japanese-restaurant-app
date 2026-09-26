namespace SakuraTei.Api.Models;

/// <summary>
/// Một ảnh của món ăn. Ảnh lấy từ Wikimedia Commons theo giấy phép Creative Commons nên phải
/// giữ tên tác giả, giấy phép và đường dẫn trang gốc để hiển thị ghi công.
/// </summary>
/// <param name="Url">Bản rộng 960px cho ảnh lớn ở trang chi tiết.</param>
/// <param name="ThumbUrl">Bản rộng 500px cho thẻ món và dải ảnh nhỏ — nhẹ hơn khoảng ba lần.</param>
/// <param name="Credit">Tên tác giả kèm giấy phép, ví dụ "Nguyễn Văn A · CC BY-SA 4.0".</param>
/// <param name="SourceUrl">Trang mô tả ảnh trên Wikimedia Commons.</param>
public sealed record DishPhoto(string Url, string ThumbUrl, string Credit, string SourceUrl);
