using SakuraTei.Api.Common;
using SakuraTei.Api.Data;
using SakuraTei.Api.Models;

namespace SakuraTei.Api.Services;

public interface IBlogService
{
    PagedResult<BlogPost> GetPosts(string? topic, int page, int pageSize);

    BlogPost? GetBySlug(string slug);

    /// <summary>Các bài mới nhất, dùng cho dải "Câu chuyện ẩm thực" ở trang chủ.</summary>
    IReadOnlyList<BlogPost> GetLatest(int take);

    IReadOnlyList<string> GetTopics();
}

/// <summary>Đọc bài viết từ dữ liệu tĩnh trong bộ nhớ.</summary>
public sealed class BlogService : IBlogService
{
    private readonly IReadOnlyList<BlogPost> _posts =
        [.. SeedPosts.All.OrderByDescending(p => p.PublishedAt)];

    public PagedResult<BlogPost> GetPosts(string? topic, int page, int pageSize)
    {
        var filtered = string.IsNullOrWhiteSpace(topic)
            ? _posts
            : [.. _posts.Where(p => string.Equals(p.Topic, topic, StringComparison.OrdinalIgnoreCase))];

        var size = Math.Clamp(pageSize, 1, 24);
        var totalPages = filtered.Count == 0 ? 1 : (int)Math.Ceiling(filtered.Count / (double)size);
        var current = Math.Clamp(page, 1, totalPages);

        var items = filtered.Skip((current - 1) * size).Take(size).ToList();

        return new PagedResult<BlogPost>(items, current, size, filtered.Count);
    }

    public BlogPost? GetBySlug(string slug) =>
        _posts.FirstOrDefault(p => string.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<BlogPost> GetLatest(int take) => [.. _posts.Take(Math.Clamp(take, 1, 12))];

    public IReadOnlyList<string> GetTopics() =>
        [.. _posts.Select(p => p.Topic).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(t => t, StringComparer.Ordinal)];
}
