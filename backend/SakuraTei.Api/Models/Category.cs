namespace SakuraTei.Api.Models;

/// <summary>
/// Nhóm món trong thực đơn: sushi, ramen, pudding, mochi, matcha.
/// </summary>
public sealed record Category
{
    public required string Slug { get; init; }

    public required string Name { get; init; }

    public required string NameJp { get; init; }

    /// <summary>Một ký tự kanji đại diện, frontend dùng làm ảnh dự phòng khi ảnh lỗi.</summary>
    public required string Kanji { get; init; }

    public required string Description { get; init; }

    public required string ImageUrl { get; init; }

    /// <summary>Mã màu accent (hex) để frontend tô badge/nền cho danh mục.</summary>
    public required string AccentColor { get; init; }

    /// <summary>Thứ tự hiển thị, nhỏ hơn thì đứng trước.</summary>
    public int DisplayOrder { get; init; }
}
