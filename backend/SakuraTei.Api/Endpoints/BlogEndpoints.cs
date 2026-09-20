using SakuraTei.Api.Common;
using SakuraTei.Api.Models;
using SakuraTei.Api.Services;

namespace SakuraTei.Api.Endpoints;

/// <summary>Chuyên mục "Câu chuyện ẩm thực".</summary>
public static class BlogEndpoints
{
    public static RouteGroupBuilder MapBlogEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/posts", (string? topic, IBlogService blog, int page = 1, int pageSize = 9) =>
                TypedResults.Ok(blog.GetPosts(topic, page, pageSize)))
            .WithName("GetPosts")
            .WithSummary("Danh sách bài viết, lọc được theo chuyên mục.");

        group.MapGet("/posts/topics", (IBlogService blog) => TypedResults.Ok(blog.GetTopics()))
            .WithName("GetPostTopics")
            .WithSummary("Các chuyên mục hiện có.");

        group.MapGet("/posts/latest", (IBlogService blog, int take = 3) =>
                TypedResults.Ok(blog.GetLatest(take)))
            .WithName("GetLatestPosts")
            .WithSummary("Bài mới nhất, dùng cho trang chủ.");

        group.MapGet("/posts/{slug}", (string slug, IBlogService blog) =>
            {
                var post = blog.GetBySlug(slug);
                return post is null
                    ? Results.NotFound(new ProblemDetailsBody("Không tìm thấy", $"Không có bài viết '{slug}'.", 404))
                    : Results.Ok(post);
            })
            .WithName("GetPostBySlug")
            .WithSummary("Nội dung đầy đủ của một bài viết.")
            .Produces<BlogPost>()
            .Produces<ProblemDetailsBody>(StatusCodes.Status404NotFound);

        return group;
    }
}
