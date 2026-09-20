namespace SakuraTei.Api.Models;

/// <summary>
/// Một món ăn / thức uống trong thực đơn của Sakura Tei.
/// </summary>
public sealed record Dish
{
    /// <summary>Định danh ổn định, dùng trong giỏ hàng và đơn hàng.</summary>
    public required string Id { get; init; }

    /// <summary>Slug thân thiện URL, ví dụ <c>tonkotsu-ramen</c>.</summary>
    public required string Slug { get; init; }

    /// <summary>Tên hiển thị tiếng Việt.</summary>
    public required string Name { get; init; }

    /// <summary>Tên tiếng Nhật (kanji/kana) dùng làm dòng phụ trang trí.</summary>
    public required string NameJp { get; init; }

    /// <summary>Tên romaji, phục vụ tìm kiếm không dấu.</summary>
    public required string NameRomaji { get; init; }

    /// <summary>Slug danh mục — khớp với <see cref="Category.Slug"/>.</summary>
    public required string CategorySlug { get; init; }

    /// <summary>Mô tả ngắn hiển thị trên thẻ món.</summary>
    public required string Description { get; init; }

    /// <summary>Mô tả dài hiển thị ở trang chi tiết.</summary>
    public required string LongDescription { get; init; }

    /// <summary>Giá bán, đơn vị VND.</summary>
    public required decimal Price { get; init; }

    /// <summary>Giá gốc trước khuyến mãi; <c>null</c> nghĩa là không giảm giá.</summary>
    public decimal? OriginalPrice { get; init; }

    public required string ImageUrl { get; init; }

    public IReadOnlyList<string> Tags { get; init; } = [];

    /// <summary>Nguyên liệu chính, hiển thị ở trang chi tiết.</summary>
    public IReadOnlyList<string> Ingredients { get; init; } = [];

    /// <summary>Độ cay từ 0 (không cay) đến 3 (rất cay).</summary>
    public int SpicyLevel { get; init; }

    public int Calories { get; init; }

    /// <summary>Thời gian chế biến ước tính, tính bằng phút.</summary>
    public int PrepMinutes { get; init; }

    /// <summary>Điểm đánh giá trung bình, thang 0–5.</summary>
    public double Rating { get; init; }

    public int ReviewCount { get; init; }

    /// <summary>Đánh dấu món nổi bật để đưa lên trang chủ.</summary>
    public bool IsFeatured { get; init; }

    /// <summary>Món tạm hết hàng vẫn hiển thị nhưng không cho đặt.</summary>
    public bool IsAvailable { get; init; } = true;
}
