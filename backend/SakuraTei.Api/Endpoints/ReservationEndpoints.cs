using SakuraTei.Api.Common;
using SakuraTei.Api.Contracts;
using SakuraTei.Api.Services;

namespace SakuraTei.Api.Endpoints;

/// <summary>Đặt bàn tại quán và tra cứu khung giờ còn chỗ.</summary>
public static class ReservationEndpoints
{
    public static RouteGroupBuilder MapReservationEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/reservations/availability", (DateOnly? date, IReservationService reservations) =>
            {
                var target = date ?? DateOnly.FromDateTime(DateTimeOffset.Now.DateTime);
                return TypedResults.Ok(reservations.GetAvailability(target));
            })
            .WithName("GetReservationAvailability")
            .WithSummary("Khung giờ còn chỗ trong một ngày.")
            .WithDescription("Quán nhận khách từ 11:00 đến 21:00, mỗi khung 30 phút, sức chứa 24 chỗ một khung.");

        group.MapPost("/reservations", (CreateReservationRequest request, IReservationService reservations) =>
            {
                var result = reservations.Create(request);

                return result.Succeeded
                    ? Results.Created(
                        $"/api/reservations/{result.Reservation!.Code}",
                        ReservationResponse.From(result.Reservation))
                    : ApiResults.Validation(result.Errors);
            })
            .WithName("CreateReservation")
            .WithSummary("Đặt bàn.")
            .Produces<ReservationResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        group.MapGet("/reservations/{code}", (string code, IReservationService reservations) =>
            {
                var reservation = reservations.FindByCode(code);
                return reservation is null
                    ? Results.NotFound(new ProblemDetailsBody(
                        "Không tìm thấy",
                        $"Không có lượt đặt bàn nào mang mã '{code}'.",
                        404))
                    : Results.Ok(ReservationResponse.From(reservation));
            })
            .WithName("GetReservationByCode")
            .WithSummary("Tra cứu lượt đặt bàn theo mã.")
            .Produces<ReservationResponse>()
            .Produces<ProblemDetailsBody>(StatusCodes.Status404NotFound);

        return group;
    }
}
