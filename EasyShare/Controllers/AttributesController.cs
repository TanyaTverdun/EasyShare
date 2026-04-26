using EasyShare.Application.Features.Attributes.Commands.CreateAttribute;
using EasyShare.Application.Features.Attributes.Commands.DeleteAttribute;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyShare.Controllers;

[ApiController]
[Route("api/attributes")]
[Authorize]
public class AttributesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AttributesController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAttribute(
        [FromBody] CreateAttributeRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var attributeId = await this._mediator.Send(
            command,
            cancellationToken);

        return Created("", new { Id = attributeId });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttribute(
        int id,
        CancellationToken cancellationToken)
    {
        await this._mediator.Send(
            new DeleteAttributeCommand { Id = id }, 
            cancellationToken);

        return NoContent();
    }
}
