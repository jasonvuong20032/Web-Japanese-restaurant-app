using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SakuraTei.Api.Data.Configurations;

/// <summary>
/// Lưu một danh sách nhỏ (thẻ món, nguyên liệu, bộ ảnh) thành một cột JSON thay vì tách bảng riêng.
/// Các danh sách này luôn được đọc trọn cùng món ăn và không bao giờ bị truy vấn riêng.
/// </summary>
internal static class JsonColumn
{
    /// <summary>Giữ nguyên tiếng Việt có dấu trong cột, mở database ra đọc được ngay.</summary>
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    public static PropertyBuilder<IReadOnlyList<T>> HasJsonConversion<T>(this PropertyBuilder<IReadOnlyList<T>> property) =>
        property.HasConversion(
            value => JsonSerializer.Serialize(value, Options),
            json => JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>(),
            // EF cần biết cách so sánh danh sách để phát hiện thay đổi; mặc định nó chỉ so tham chiếu.
            new ValueComparer<IReadOnlyList<T>>(
                (left, right) => ReferenceEquals(left, right) || (left != null && right != null && left.SequenceEqual(right)),
                value => value.Aggregate(0, (hash, item) => HashCode.Combine(hash, item)),
                value => value.ToList()));
}
