using Microsoft.AspNetCore.Mvc;
using SakuraTei.Api.Common;
using SakuraTei.Api.Contracts;
using SakuraTei.Api.Services;

namespace SakuraTei.Api.Endpoints;

/// <summary>Các endpoint đọc thực đơn: danh mục, danh sách món, chi tiết món.</summary>
public static class MenuEndpoints
{
    public static RouteGroupBuilder MapMenuEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/categories", (IDishService dishes) => TypedResults.Ok(dishes.GetCategories()))
            .WithName("GetCategories")
            .WithSummary("Danh sách bảy nhóm món, kèm số món trong từng nhóm.");

        group.MapGet("/dishes", ([AsParameters] DishQuery query, IDishService dishes) =>
                TypedResults.Ok(dishes.Search(query)))
            .WithName("SearchDishes")
            .WithSummary("Tìm và lọc thực đơn, có phân trang.")
            .WithDescription(
                "Hỗ trợ lọc theo category, từ khoá (không dấu cũng khớp), khoảng giá, độ cay tối đa và thẻ. "
                + "Sort nhận: price_asc, price_desc, rating, popular.");

        group.MapGet("/dishes/featured", (IDishService dishes, int take = 8) =>
                TypedResults.Ok(dishes.GetFeatured(take)))
            .WithName("GetFeaturedDishes")
            .WithSummary("Các món nổi bật dùng cho trang chủ.");

        group.MapGet("/dishes/filters", (IDishService dishes) =>
            {
                var (min, max) = dishes.GetPriceRange();
                return TypedResults.Ok(new
                {
                    Tags = dishes.GetAllTags(),
                    MinPrice = min,
                    MaxPrice = max,
                });
            })
            .WithName("GetDishFilters")
            .WithSummary("Dữ liệu dựng bộ lọc: danh sách thẻ và khoảng giá của thực đơn.");

        group.MapGet("/dishes/{slug}", (string slug, IDishService dishes) =>
            {
                var dish = dishes.GetBySlug(slug);
                return dish is null
                    ? Results.NotFound(new ProblemDetailsBody("Không tìm thấy", $"Không có món nào với slug '{slug}'.", 404))
                    : Results.Ok(dish);
            })
            .WithName("GetDishBySlug")
            .WithSummary("Chi tiết một món, kèm tối đa 4 món liên quan cùng nhóm.")
            .Produces<DishDetail>()
            .Produces<ProblemDetailsBody>(StatusCodes.Status404NotFound);

        return group;
    }
}
