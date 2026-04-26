using EasyShare.Application.Features.ItemTypes.Commands.CreateItemType;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyShare.Controllers;

[ApiController]
[Route("api/item-types")]
[Authorize]
public class ItemTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemTypesController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateItemType(
        [FromBody] CreateItemTypeRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var typeId = await this._mediator.Send(
            command, 
            cancellationToken);

        return Created("", new { Id = typeId });
    }
}
