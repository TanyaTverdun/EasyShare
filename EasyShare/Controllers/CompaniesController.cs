using EasyShare.Application.Features.Companies.Commands.ActivateItem;
using EasyShare.Application.Features.Companies.Commands.DeactivateItem;
using EasyShare.Application.Features.Companies.Commands.UpdateProfile;
using EasyShare.Application.Features.Companies.Queries.GetBookings;
using EasyShare.Application.Features.Companies.Queries.GetItems;
using EasyShare.Application.Features.Companies.Queries.GetProfile;
using EasyShare.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyShare.Controllers;

[Authorize(Roles = nameof(AccountType.Company))]
[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPut("profile")]
    public async Task<ActionResult<CompanyProfileResponse>> UpdateMyProfile(
        [FromBody] UpdateCompanyProfileCommand command,
        CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(
            command, 
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("profile")]
    public async Task<ActionResult<CompanyProfileDto>> GetMyProfile(
        CancellationToken cancellationToken)
    {
        var query = new GetCompanyProfileQuery();
        var result = await this._mediator.Send(
            query, 
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("items")]
    public async Task<ActionResult<List<CompanyItemDto>>> GetMyItems(
        [FromQuery] GetCompanyItemsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(
            query, 
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("items/{Id}/deactivate")]
    public async Task<IActionResult> DeactivateItem(
    [FromRoute] DeactivateItemCommand command,
    CancellationToken cancellationToken)
    {
        await this._mediator.Send(
            command, 
            cancellationToken);

        return NoContent();
    }

    [HttpPatch("items/{Id}/activate")]
    public async Task<IActionResult> ActivateItem(
        [FromRoute] ActivateItemCommand command,
        CancellationToken cancellationToken)
    {
        await this._mediator.Send(
            command, 
            cancellationToken);

        return NoContent();
    }

    [HttpGet("bookings")]
    public async Task<ActionResult<List<CompanyBookingDto>>> GetMyBookings(
        [FromQuery] GetCompanyBookingsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await this._mediator.Send(
            query, 
            cancellationToken);

        return Ok(result);
    }
}
