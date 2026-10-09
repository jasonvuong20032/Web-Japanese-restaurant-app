using Microsoft.EntityFrameworkCore;
using SakuraTei.Api.Models;

namespace SakuraTei.Api.Data;

/// <summary>
/// Database SQLite của Sakura Tei. Cách ánh xạ từng bảng nằm trong <c>Data/Configurations</c>,
/// tạo bảng và nạp dữ liệu mẫu nằm trong <see cref="DatabaseSetup"/>.
/// </summary>
public sealed class SakuraDbContext(DbContextOptions<SakuraDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Dish> Dishes => Set<Dish>();

    public DbSet<BlogPost> Posts => Set<BlogPost>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SakuraDbContext).Assembly);
}
