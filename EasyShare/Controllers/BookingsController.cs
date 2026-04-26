using EasyShare.Application.Features.Bookings.Commands.CancelBooking;
using EasyShare.Application.Features.Bookings.Commands.CompleteBooking;
using EasyShare.Application.Features.Bookings.Commands.ConfirmBooking;
using EasyShare.Application.Features.Bookings.Commands.CreateBooking;
using EasyShare.Application.Features.Bookings.Commands.IssueBooking;
using EasyShare.Application.Features.Bookings.Commands.ReturnBooking;
using EasyShare.Application.Features.Bookings.Commands.UpdateBooking;
using EasyShare.Application.Features.Bookings.Queries.GetUserBookings;
using EasyShare.Application.Features.Bookings.Queries.GetUserBookingStats;
using EasyShare.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyShare.API.Controllers;

[Authorize(Roles = nameof(AccountType.User))]
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBooking(
        int id, 
        [FromBody] UpdateBookingCommand command)
    {
        if (id != command.BookingId)
        {
            return BadRequest("ID бронювання в URL не збігається з ID у тілі запиту.");
        }

        await this._mediator.Send(command);

        return NoContent();
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelBooking(
        int id,
        [FromBody] CancelBookingCommand command)
    {
        if (id != command.BookingId)
        {
            return BadRequest(
                "ID бронювання в URL не збігається з ID у тілі запиту.");
        }

        await this._mediator.Send(command);

        return NoContent();
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> ReturnBooking(
    [FromRoute] int id,
    [FromBody] ReturnBookingRequest request,
    CancellationToken cancellationToken)
    {
        var command = new ReturnBookingCommand { 
            BookingId = id,
            Rating = request.Rating,
            Comment = request.Comment
        };

        await this._mediator.Send(command, cancellationToken);

        return Ok(new { message = "Запит на повернення успішно створено." });
    }

    [HttpPut("{id}/confirm")]
    public async Task<IActionResult> ConfirmBooking(
        int id,
        [FromBody] ConfirmBookingCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(
                "ID бронювання в URL не збігається з ID у тілі запиту.");
        }

        await this._mediator.Send(command);

        return NoContent();
    }

    [HttpPut("{id}/issue")]
    public async Task<IActionResult> IssueBooking(
        int id,
        [FromBody] IssueBookingCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(
                "ID бронювання в URL не збігається з ID у тілі запиту.");
        }

        await this._mediator.Send(command);

        return NoContent();
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteBooking(
        int id,
        [FromBody] CompleteBookingCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(
                "ID бронювання в URL не збігається з ID у тілі запиту.");
        }

        await this._mediator.Send(command);

        return NoContent();
    }
}