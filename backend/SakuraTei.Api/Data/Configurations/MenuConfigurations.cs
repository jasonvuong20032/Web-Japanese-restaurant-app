using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Slug);
    }
}

internal sealed class DishConfiguration : IEntityTypeConfiguration<Dish>
{
    /// <summary>
    /// Cột ẩn giữ thứ tự món như trong dữ liệu mẫu. Không có cột này thì thứ tự đọc ra phụ thuộc
    /// vào cách EF sắp xếp lệnh INSERT, và các món cùng điểm đánh giá sẽ đổi chỗ cho nhau.
    /// </summary>
    public const string DisplayOrder = "DisplayOrder";

    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.ToTable("Dishes");
        builder.HasKey(d => d.Id);
        builder.HasIndex(d => d.Slug).IsUnique();

        builder.Property<int>(DisplayOrder);

        // Mỗi món thuộc đúng một nhóm có thật — SQLite từ chối món trỏ tới nhóm không tồn tại.
        // Restrict: còn món thì không xoá được nhóm, tránh lỡ tay xoá dây chuyền cả chục món.
        builder.HasOne<Category>().WithMany().HasForeignKey(d => d.CategorySlug).OnDelete(DeleteBehavior.Restrict);

        builder.Property(d => d.Tags).HasJsonConversion();
        builder.Property(d => d.Ingredients).HasJsonConversion();
        builder.Property(d => d.Photos).HasJsonConversion();
    }
}

internal sealed class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(p => p.Slug);
    }
}
