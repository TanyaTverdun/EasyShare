using EasyShare.Application.Features.Admin.Queries.GetItemTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyShare.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet("item-types")]
    public async Task<ActionResult<List<AdminItemTypeDto>>> GetItemTypes(
        [FromQuery] string? searchTerm,
        CancellationToken cancellationToken)
    {
        var query = new GetAdminItemTypesQuery
        {
            SearchTerm = searchTerm
        };

        var result = await this._mediator.Send(
            query, 
            cancellationToken);

        return Ok(result);
    }
}
