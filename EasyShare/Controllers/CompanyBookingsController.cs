using EasyShare.Application.Features.Bookings.Commands.CancelBooking;
using EasyShare.Application.Features.Bookings.Commands.CompleteBooking;
using EasyShare.Application.Features.Bookings.Commands.ConfirmBooking;
using EasyShare.Application.Features.Bookings.Commands.IssueBooking;
using EasyShare.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyShare.Controllers;


[Authorize(Roles = nameof(AccountType.Company))]
[ApiController]
[Route("api/[controller]")]
public class CompanyBookingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyBookingsController(IMediator mediator)
    {
        this._mediator = mediator;
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
     [FromRoute] int id,
     [FromBody] CompleteBookingRequest request,
     CancellationToken cancellationToken)
    {
        var command = new CompleteBookingCommand
        {
            BookingId = id,
            Rating = request.Rating,
            Comment = request.Comment
        };

        await this._mediator.Send(
            command,
            cancellationToken);

        return NoContent();
    }
}
