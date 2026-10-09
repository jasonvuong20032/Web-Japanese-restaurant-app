using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SakuraTei.Api.Data.Configurations;
using SakuraTei.Api.Data.Seed;

namespace SakuraTei.Api.Data;

/// <summary>Đăng ký, tạo bảng và nạp dữ liệu mẫu cho database SQLite.</summary>
public static class DatabaseSetup
{
    private const string DefaultConnectionString = "Data Source=App_Data/sakuratei.db";

    /// <summary>
    /// Đăng ký <see cref="IDbContextFactory{TContext}"/>. Dùng factory chứ không tiêm thẳng
    /// <see cref="SakuraDbContext"/> vì các service là singleton, còn DbContext chỉ nên sống
    /// trong một thao tác: mỗi lần đọc/ghi tạo một context mới rồi bỏ.
    /// </summary>
    public static IServiceCollection AddSakuraDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var connectionString = ResolveConnectionString(
            configuration.GetConnectionString("Sakura") ?? DefaultConnectionString,
            environment.ContentRootPath);

        return services.AddDbContextFactory<SakuraDbContext>(options => options.UseSqlite(connectionString));
    }

    /// <summary>
    /// Tạo file database và các bảng nếu chưa có, rồi nạp thực đơn và bài viết ở lần chạy đầu.
    /// Phải gọi trước <c>app.Run()</c> vì <c>DishService</c> đọc thực đơn ngay khi được tạo.
    /// </summary>
    /// <remarks>
    /// Dùng <c>EnsureCreated</c> thay cho migration: database chỉ có một phiên bản lược đồ và có thể
    /// xoá đi tạo lại bất cứ lúc nào. Đổi lược đồ thì xoá file <c>App_Data/sakuratei.db</c> rồi chạy lại.
    /// </remarks>
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        var factory = app.Services.GetRequiredService<IDbContextFactory<SakuraDbContext>>();
        var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(DatabaseSetup));

        await using var db = await factory.CreateDbContextAsync();

        var path = db.Database.GetDbConnection().DataSource;
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

        await db.Database.EnsureCreatedAsync();

        if (await db.Categories.AnyAsync())
        {
            logger.LogInformation("Dùng database có sẵn tại {Path}", path);
            return;
        }

        db.Categories.AddRange(SeedCategories.All);
        db.Posts.AddRange(SeedPosts.All);

        var dishes = SeedDishes.All;
        for (var i = 0; i < dishes.Count; i++)
        {
            db.Dishes.Add(dishes[i]).Property(DishConfiguration.DisplayOrder).CurrentValue = i;
        }

        // Một lần SaveChanges là một transaction: hoặc nạp đủ cả ba bảng, hoặc không nạp gì.
        await db.SaveChangesAsync();

        logger.LogInformation(
            "Đã tạo database tại {Path}: {CategoryCount} nhóm, {DishCount} món, {PostCount} bài viết",
            path, SeedCategories.All.Count, dishes.Count, SeedPosts.All.Count);
    }

    /// <summary>
    /// Đường dẫn tương đối được tính từ thư mục gốc của ứng dụng chứ không phải thư mục đang đứng,
    /// nên chạy <c>dotnet run</c> từ đâu thì database cũng nằm cùng một chỗ.
    /// </summary>
    private static string ResolveConnectionString(string connectionString, string contentRoot)
    {
        var builder = new SqliteConnectionStringBuilder(connectionString);

        if (!Path.IsPathRooted(builder.DataSource))
        {
            builder.DataSource = Path.GetFullPath(Path.Combine(contentRoot, builder.DataSource));
        }

        return builder.ToString();
    }
}
