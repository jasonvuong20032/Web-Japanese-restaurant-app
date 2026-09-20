using SakuraTei.Api.Common;
using SakuraTei.Api.Contracts;
using SakuraTei.Api.Services;

namespace SakuraTei.Api.Endpoints;

/// <summary>Đặt món và tra cứu đơn hàng.</summary>
public static class OrderEndpoints
{
    public static RouteGroupBuilder MapOrderEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/orders", (CreateOrderRequest request, IOrderService orders) =>
            {
                var result = orders.Create(request);

                return result.Succeeded
                    ? Results.Created($"/api/orders/{result.Order!.Code}", OrderResponse.From(result.Order))
                    : ApiResults.Validation(result.Errors);
            })
            .WithName("CreateOrder")
            .WithSummary("Tạo đơn hàng mới.")
            .WithDescription(
                "Server tự tra giá từ thực đơn nên client chỉ cần gửi dishId và quantity. "
                + "Đơn từ 500.000đ được miễn phí giao hàng.")
            .Produces<OrderResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        group.MapGet("/orders/{code}", (string code, IOrderService orders) =>
            {
                var order = orders.FindByCode(code);
                return order is null
                    ? Results.NotFound(new ProblemDetailsBody(
                        "Không tìm thấy",
                        $"Không có đơn hàng nào mang mã '{code}'. Lưu ý đơn hàng chỉ lưu trong bộ nhớ và sẽ mất khi máy chủ khởi động lại.",
                        404))
                    : Results.Ok(OrderResponse.From(order));
            })
            .WithName("GetOrderByCode")
            .WithSummary("Tra cứu đơn hàng theo mã.")
            .Produces<OrderResponse>()
            .Produces<ProblemDetailsBody>(StatusCodes.Status404NotFound);

        return group;
    }
}
