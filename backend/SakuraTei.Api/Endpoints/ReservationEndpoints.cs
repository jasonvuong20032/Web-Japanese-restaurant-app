using SakuraTei.Api.Common;
using SakuraTei.Api.Contracts;
using SakuraTei.Api.Services;

namespace SakuraTei.Api.Endpoints;

/// <summary>Đặt bàn tại quán: xem chỗ trống, đặt mới, tra cứu, đổi lịch, huỷ.</summary>
public static class ReservationEndpoints
{
    public static RouteGroupBuilder MapReservationEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/reservations/availability", (DateOnly? date, IReservationService reservations) =>
                TypedResults.Ok(reservations.GetAvailability(date ?? VietnamTime.Today)))
            .WithName("GetReservationAvailability")
            .WithSummary("Khung giờ còn chỗ trong một ngày.")
            .WithDescription("Quán nhận khách từ 11:00 đến 21:00, mỗi khung 30 phút, sức chứa 24 chỗ một khung.");

        group.MapPost("/reservations", (ReservationRequest request, IReservationService reservations) =>
                reservations.Create(request).ToHttpResult(reservation =>
                    Results.Created($"/api/reservations/{reservation.Code}", ReservationResponse.From(reservation))))
            .WithName("CreateReservation")
            .WithSummary("Đặt bàn.")
            .Produces<ReservationResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        group.MapGet("/reservations/{code}", (string code, IReservationService reservations) =>
                reservations.FindByCode(code) is { } reservation
                    ? Results.Ok(ReservationResponse.From(reservation))
                    : ApiResults.NotFound($"Không có lượt đặt bàn nào mang mã '{code}'."))
            .WithName("GetReservationByCode")
            .WithSummary("Tra cứu lượt đặt bàn theo mã.")
            .Produces<ReservationResponse>()
            .Produces<ProblemDetailsBody>(StatusCodes.Status404NotFound);

        group.MapPut("/reservations/{code}", (string code, ReservationRequest request, IReservationService reservations) =>
                reservations.Update(code, request).ToHttpResult(reservation =>
                    Results.Ok(ReservationResponse.From(reservation))))
            .WithName("UpdateReservation")
            .WithSummary("Đổi lịch đặt bàn.")
            .WithDescription("Gửi lại toàn bộ thông tin lượt đặt. Chỗ trống được kiểm tra lại, không tính chỗ của chính lượt này.")
            .Produces<ReservationResponse>()
            .ProducesValidationProblem()
            .Produces<ProblemDetailsBody>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetailsBody>(StatusCodes.Status409Conflict);

        group.MapDelete("/reservations/{code}", (string code, IReservationService reservations) =>
                reservations.Delete(code).ToHttpResult(_ => Results.NoContent()))
            .WithName("DeleteReservation")
            .WithSummary("Huỷ đặt bàn.")
            .WithDescription("Xoá lượt đặt và trả lại chỗ cho khung giờ đó. Lượt đặt đã qua giờ hẹn thì không huỷ được.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetailsBody>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetailsBody>(StatusCodes.Status409Conflict);

        return group;
    }
}
