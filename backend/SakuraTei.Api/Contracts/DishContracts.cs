using SakuraTei.Api.Models;

namespace SakuraTei.Api.Contracts;

/// <summary>Dạng rút gọn của món ăn, dùng cho lưới thực đơn và kết quả tìm kiếm.</summary>
public sealed record DishSummary(
    string Id,
    string Slug,
    string Name,
    string NameJp,
    string CategorySlug,
    string Description,
    decimal Price,
    decimal? OriginalPrice,
    string ImageUrl,
    IReadOnlyList<string> Tags,
    int SpicyLevel,
    int Calories,
    int PrepMinutes,
    double Rating,
    int ReviewCount,
    bool IsFeatured,
    bool IsAvailable)
{
    public static DishSummary From(Dish d) => new(
        d.Id, d.Slug, d.Name, d.NameJp, d.CategorySlug, d.Description, d.Price, d.OriginalPrice,
        d.ImageUrl, d.Tags, d.SpicyLevel, d.Calories, d.PrepMinutes, d.Rating, d.ReviewCount,
        d.IsFeatured, d.IsAvailable);
}

/// <summary>Dạng đầy đủ cho trang chi tiết món, kèm gợi ý món cùng nhóm.</summary>
public sealed record DishDetail(
    string Id,
    string Slug,
    string Name,
    string NameJp,
    string NameRomaji,
    string CategorySlug,
    string CategoryName,
    string Description,
    string LongDescription,
    decimal Price,
    decimal? OriginalPrice,
    string ImageUrl,
    IReadOnlyList<string> Tags,
    IReadOnlyList<string> Ingredients,
    int SpicyLevel,
    int Calories,
    int PrepMinutes,
    double Rating,
    int ReviewCount,
    bool IsFeatured,
    bool IsAvailable,
    IReadOnlyList<DishSummary> Related);

/// <summary>Danh mục kèm số món đang có, để frontend hiện badge đếm.</summary>
public sealed record CategoryResponse(
    string Slug,
    string Name,
    string NameJp,
    string Kanji,
    string Description,
    string ImageUrl,
    string AccentColor,
    int DisplayOrder,
    int DishCount);

/// <summary>Tham số lọc thực đơn, ASP.NET Core tự bind từ query string.</summary>
public sealed record DishQuery
{
    /// <summary>Lọc theo slug danh mục; bỏ trống nghĩa là lấy tất cả.</summary>
    public string? Category { get; init; }

    /// <summary>Từ khoá tìm trong tên tiếng Việt, tên romaji, mô tả và nguyên liệu.</summary>
    public string? Q { get; init; }

    public decimal? MinPrice { get; init; }

    public decimal? MaxPrice { get; init; }

    /// <summary>Độ cay tối đa chấp nhận được, từ 0 đến 3.</summary>
    public int? MaxSpicy { get; init; }

    /// <summary>Lọc theo một thẻ, ví dụ <c>Thuần chay</c>.</summary>
    public string? Tag { get; init; }

    /// <summary><c>price_asc</c>, <c>price_desc</c>, <c>rating</c>, <c>popular</c>, mặc định <c>default</c>.</summary>
    public string? Sort { get; init; }

    /// <summary>
    /// Số trang, bắt đầu từ 1. Để nullable vì <c>[AsParameters]</c> coi tham số giá trị không
    /// nullable là bắt buộc và bỏ qua giá trị mặc định ở đây; mặc định thật nằm trong DishService.
    /// </summary>
    public int? Page { get; init; }

    /// <summary>Số món mỗi trang; bỏ trống thì DishService dùng 12.</summary>
    public int? PageSize { get; init; }
}
