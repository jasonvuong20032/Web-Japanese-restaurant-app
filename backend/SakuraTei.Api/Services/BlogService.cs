using Microsoft.EntityFrameworkCore;
using SakuraTei.Api.Common;
using SakuraTei.Api.Data;
using SakuraTei.Api.Models;

namespace SakuraTei.Api.Services;

public interface IBlogService
{
    PagedResult<BlogPost> GetPosts(string? topic, int page, int pageSize);

    BlogPost? GetBySlug(string slug);

    IReadOnlyList<BlogPost> GetLatest(int take);

    IReadOnlyList<string> GetTopics();
}

/// <summary>
/// Bài viết đọc từ bảng <c>Posts</c> một lần khi service được tạo — chỉ vài bài và ít khi đổi,
/// nên giữ trong bộ nhớ thay vì hỏi database mỗi lần.
/// </summary>
public sealed class BlogService : IBlogService
{
    private readonly IReadOnlyList<BlogPost> _posts;

    public BlogService(IDbContextFactory<SakuraDbContext> dbFactory)
    {
        using var db = dbFactory.CreateDbContext();

        // Sắp xếp phía ứng dụng: SQLite lưu ngày dạng chuỗi, không nên phó mặc thứ tự cho nó.
        _posts = [.. db.Posts.AsNoTracking().AsEnumerable().OrderByDescending(p => p.PublishedAt)];
    }

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
