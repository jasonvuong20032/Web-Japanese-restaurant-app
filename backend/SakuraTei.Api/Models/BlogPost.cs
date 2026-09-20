namespace SakuraTei.Api.Models;

/// <summary>Bài viết trong chuyên mục "Câu chuyện ẩm thực".</summary>
public sealed record BlogPost
{
    public required string Slug { get; init; }

    public required string Title { get; init; }

    public required string Excerpt { get; init; }

    /// <summary>Nội dung đầy đủ dạng Markdown rút gọn: <c>##</c> cho tiểu mục, dòng trống ngăn đoạn.</summary>
    public required string Content { get; init; }

    public required string Author { get; init; }

    public required string ImageUrl { get; init; }

    /// <summary>Chuyên mục hiển thị, ví dụ "Văn hoá", "Công thức".</summary>
    public required string Topic { get; init; }

    public required DateOnly PublishedAt { get; init; }

    /// <summary>Thời gian đọc ước tính, tính bằng phút.</summary>
    public int ReadMinutes { get; init; }
}
