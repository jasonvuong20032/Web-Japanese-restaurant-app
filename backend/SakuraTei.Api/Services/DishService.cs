using System.Globalization;
using System.Text;
using SakuraTei.Api.Common;
using SakuraTei.Api.Contracts;
using SakuraTei.Api.Data;
using SakuraTei.Api.Models;

namespace SakuraTei.Api.Services;

public interface IDishService
{
    IReadOnlyList<CategoryResponse> GetCategories();

    PagedResult<DishSummary> Search(DishQuery query);

    IReadOnlyList<DishSummary> GetFeatured(int take);

    DishDetail? GetBySlug(string slug);

    /// <summary>Tra món theo Id để tính tiền đơn hàng; <c>null</c> nếu không có món đó.</summary>
    Dish? FindById(string id);

    /// <summary>Toàn bộ thẻ lọc đang được dùng, đã sắp xếp — frontend dựng bộ lọc từ đây.</summary>
    IReadOnlyList<string> GetAllTags();

    /// <summary>Khoảng giá thấp nhất và cao nhất của thực đơn, dùng cho thanh lọc giá.</summary>
    (decimal Min, decimal Max) GetPriceRange();
}

/// <summary>
/// Đọc thực đơn từ dữ liệu tĩnh trong bộ nhớ. Đăng ký singleton vì dữ liệu không đổi
/// trong suốt vòng đời ứng dụng.
/// </summary>
public sealed class DishService : IDishService
{
    private const int DefaultPageSize = 12;

    private const int MaxPageSize = 60;

    private readonly IReadOnlyList<Dish> _dishes = SeedDishes.All;
    private readonly IReadOnlyList<Category> _categories = SeedCategories.All;
    private readonly Dictionary<string, Dish> _byId;
    private readonly Dictionary<string, Dish> _bySlug;

    /// <summary>Chuỗi tìm kiếm đã bỏ dấu của từng món, tính sẵn một lần để lọc cho nhanh.</summary>
    private readonly Dictionary<string, string> _searchIndex;

    public DishService()
    {
        _byId = _dishes.ToDictionary(d => d.Id, StringComparer.OrdinalIgnoreCase);
        _bySlug = _dishes.ToDictionary(d => d.Slug, StringComparer.OrdinalIgnoreCase);
        _searchIndex = _dishes.ToDictionary(
            d => d.Id,
            d => RemoveDiacritics(
                $"{d.Name} {d.NameRomaji} {d.NameJp} {d.Description} {string.Join(' ', d.Tags)} {string.Join(' ', d.Ingredients)}"),
            StringComparer.OrdinalIgnoreCase);
    }

    public IReadOnlyList<CategoryResponse> GetCategories() =>
    [
        .. _categories
            .OrderBy(c => c.DisplayOrder)
            .Select(c => new CategoryResponse(
                c.Slug, c.Name, c.NameJp, c.Kanji, c.Description, c.ImageUrl, c.AccentColor,
                c.DisplayOrder, _dishes.Count(d => d.CategorySlug == c.Slug))),
    ];

    public PagedResult<DishSummary> Search(DishQuery query)
    {
        IEnumerable<Dish> result = _dishes;

        if (!string.IsNullOrWhiteSpace(query.Category))
        {
            result = result.Where(d => string.Equals(d.CategorySlug, query.Category, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Q))
        {
            // So khớp không dấu để "ca hoi" cũng tìm ra "cá hồi".
            var needle = RemoveDiacritics(query.Q.Trim());
            result = result.Where(d => _searchIndex[d.Id].Contains(needle, StringComparison.OrdinalIgnoreCase));
        }

        if (query.MinPrice is { } min)
        {
            result = result.Where(d => d.Price >= min);
        }

        if (query.MaxPrice is { } max)
        {
            result = result.Where(d => d.Price <= max);
        }

        if (query.MaxSpicy is { } spicy)
        {
            result = result.Where(d => d.SpicyLevel <= spicy);
        }

        if (!string.IsNullOrWhiteSpace(query.Tag))
        {
            result = result.Where(d => d.Tags.Contains(query.Tag, StringComparer.OrdinalIgnoreCase));
        }

        result = query.Sort switch
        {
            "price_asc" => result.OrderBy(d => d.Price).ThenBy(d => d.Name, StringComparer.Ordinal),
            "price_desc" => result.OrderByDescending(d => d.Price).ThenBy(d => d.Name, StringComparer.Ordinal),
            "rating" => result.OrderByDescending(d => d.Rating).ThenByDescending(d => d.ReviewCount),
            "popular" => result.OrderByDescending(d => d.ReviewCount).ThenByDescending(d => d.Rating),
            // Mặc định: món nổi bật lên trước, rồi tới điểm đánh giá.
            _ => result.OrderByDescending(d => d.IsFeatured).ThenByDescending(d => d.Rating),
        };

        var matched = result.ToList();

        var pageSize = Math.Clamp(query.PageSize ?? DefaultPageSize, 1, MaxPageSize);
        var totalPages = matched.Count == 0 ? 1 : (int)Math.Ceiling(matched.Count / (double)pageSize);
        var page = Math.Clamp(query.Page ?? 1, 1, totalPages);

        var items = matched
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(DishSummary.From)
            .ToList();

        return new PagedResult<DishSummary>(items, page, pageSize, matched.Count);
    }

    public IReadOnlyList<DishSummary> GetFeatured(int take) =>
    [
        .. _dishes
            .Where(d => d.IsFeatured)
            .OrderByDescending(d => d.Rating)
            .Take(Math.Clamp(take, 1, 24))
            .Select(DishSummary.From),
    ];

    public DishDetail? GetBySlug(string slug)
    {
        if (!_bySlug.TryGetValue(slug, out var dish))
        {
            return null;
        }

        var categoryName = _categories.FirstOrDefault(c => c.Slug == dish.CategorySlug)?.Name ?? dish.CategorySlug;

        var related = _dishes
            .Where(d => d.CategorySlug == dish.CategorySlug && d.Id != dish.Id)
            .OrderByDescending(d => d.Rating)
            .Take(4)
            .Select(DishSummary.From)
            .ToList();

        return new DishDetail(
            dish.Id, dish.Slug, dish.Name, dish.NameJp, dish.NameRomaji, dish.CategorySlug, categoryName,
            dish.Description, dish.LongDescription, dish.Price, dish.OriginalPrice, dish.ImageUrl,
            dish.Tags, dish.Ingredients, dish.SpicyLevel, dish.Calories, dish.PrepMinutes,
            dish.Rating, dish.ReviewCount, dish.IsFeatured, dish.IsAvailable, related);
    }

    public Dish? FindById(string id) => _byId.GetValueOrDefault(id);

    public IReadOnlyList<string> GetAllTags() =>
    [
        .. _dishes
            .SelectMany(d => d.Tags)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(t => t, StringComparer.Ordinal),
    ];

    public (decimal Min, decimal Max) GetPriceRange() => (_dishes.Min(d => d.Price), _dishes.Max(d => d.Price));

    /// <summary>
    /// Bỏ dấu tiếng Việt bằng cách tách ký tự tổ hợp rồi loại các dấu thanh.
    /// Riêng đ/Đ phải thay tay vì nó không phải là "d + dấu".
    /// </summary>
    private static string RemoveDiacritics(string text)
    {
        var normalized = text.Replace('đ', 'd').Replace('Đ', 'D').Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(normalized.Length);

        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        return builder.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();
    }
}
