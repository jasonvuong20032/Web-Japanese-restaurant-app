using SakuraTei.Api.Common;
using SakuraTei.Api.Contracts;
using SakuraTei.Api.Services;

namespace SakuraTei.Api.Endpoints;

/// <summary>Đặt món, tra cứu và huỷ đơn hàng.</summary>
public static class OrderEndpoints
{
    public static RouteGroupBuilder MapOrderEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/orders", (CreateOrderRequest request, IOrderService orders) =>
                orders.Create(request).ToHttpResult(order =>
                    Results.Created($"/api/orders/{order.Code}", OrderResponse.From(order))))
            .WithName("CreateOrder")
            .WithSummary("Tạo đơn hàng mới.")
            .WithDescription(
                "Server tự tra giá từ thực đơn nên client chỉ cần gửi dishId và quantity. "
                + "Đơn từ 500.000đ được miễn phí giao hàng.")
            .Produces<OrderResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        group.MapGet("/orders/{code}", (string code, IOrderService orders) =>
                orders.FindByCode(code) is { } order
                    ? Results.Ok(OrderResponse.From(order))
                    : ApiResults.NotFound($"Không có đơn hàng nào mang mã '{code}'."))
            .WithName("GetOrderByCode")
            .WithSummary("Tra cứu đơn hàng theo mã.")
            .Produces<OrderResponse>()
            .Produces<ProblemDetailsBody>(StatusCodes.Status404NotFound);

        group.MapPatch("/orders/{code}/cancel", (string code, IOrderService orders) =>
                orders.Cancel(code).ToHttpResult(order => Results.Ok(OrderResponse.From(order))))
            .WithName("CancelOrder")
            .WithSummary("Huỷ đơn hàng.")
            .WithDescription("Chỉ huỷ được khi đơn còn ở trạng thái Received (bếp chưa bắt đầu làm).")
            .Produces<OrderResponse>()
            .Produces<ProblemDetailsBody>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetailsBody>(StatusCodes.Status409Conflict);

        return group;
    }
}
