using EasyShare.Application.Features.Items.Commands.UpdateItem;
using EasyShare.Application.Features.Items.Queries.GetItemById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyShare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: api/items/{Id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<ItemDetailsDto>> GetItemById(
            [FromRoute] GetItemByIdQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(
        [FromRoute] int id,
        [FromForm] UpdateItemRequest request,
        CancellationToken cancellationToken)
        {
            var command = request.ToCommand(id);

            await this._mediator.Send(
                command, 
                cancellationToken);

            return NoContent();
        }
    }
}
