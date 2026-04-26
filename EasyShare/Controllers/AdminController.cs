using EasyShare.Application.Features.Admin.Commands.DeleteItemType;
using EasyShare.Application.Features.Admin.Commands.UpdateItemType;
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

    [HttpDelete("item-types/{id}")]
    public async Task<IActionResult> DeleteItemType(
        int id, 
        CancellationToken cancellationToken)
    {
        var command = new DeleteItemTypeCommand 
        { 
            Id = id 
        };

        await this._mediator.Send(
            command, 
            cancellationToken);

        return NoContent();
    }

    [HttpPut("item-types/{id}")]
    public async Task<IActionResult> UpdateItemType(
        int id, 
        [FromBody] string name, 
        CancellationToken cancellationToken)
    {
        var command = new UpdateItemTypeCommand
        {
            Id = id,
            Name = name
        };

        await this._mediator.Send(
            command, 
            cancellationToken);

        return NoContent();
    }
}
