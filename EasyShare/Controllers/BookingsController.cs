using EasyShare.Application.Features.Bookings.Commands.CreateBooking;
using EasyShare.Application.Features.Bookings.Queries.GetUserBookings;
using EasyShare.Application.Features.Bookings.Queries.GetUserBookingStats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyShare.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookingsController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking(
        [FromBody] CreateBookingCommand command)
    {
        await this._mediator.Send(command);

        return NoContent();
    }

    [HttpGet("user-stats")]
    public async Task<IActionResult> GetMyStats()
    {
        var stats = await this._mediator.Send(new GetMyBookingStatsQuery());

        return Ok(stats);
    }

    [HttpGet("user-bookings")]
    public async Task<IActionResult> GetMyBookings(
        [FromQuery] GetMyBookingsQuery query)
    {
        var bookings = await this._mediator.Send(query);

        return Ok(bookings);
    }
}