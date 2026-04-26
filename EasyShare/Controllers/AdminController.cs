using EasyShare.Application.Features.Admin.Commands.DeleteCategory;
using EasyShare.Application.Features.Admin.Commands.DeleteItemType;
using EasyShare.Application.Features.Admin.Commands.UpdateCategory;
using EasyShare.Application.Features.Admin.Commands.UpdateItemType;
using EasyShare.Application.Features.Admin.Queries.GetCategories;
using EasyShare.Application.Features.Admin.Queries.GetItemTypeAttributes;
using EasyShare.Application.Features.Admin.Queries.GetItemTypes;
using EasyShare.Application.Features.Attributes.Commands.DeleteAttribute;
using EasyShare.Application.Features.Attributes.Commands.UpdateAttribute;
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

    [HttpPut("attributes/{id}")]
    public async Task<IActionResult> UpdateAttribute(
        int id, 
        [FromBody] string name, 
        CancellationToken cancellationToken)
    {
        var command = new UpdateAttributeCommand
        {
            Id = id,
            Name = name
        };

        await this._mediator.Send(
            command, 
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("attributes/{id}")]
    public async Task<IActionResult> DeleteAttribute(
        int id, 
        CancellationToken cancellationToken)
    {
        var command = new DeleteAttributeCommand 
        { 
            Id = id 
        };

        await this._mediator.Send(
            command, 
            cancellationToken);

        return NoContent();
    }

    [HttpGet("item-types/{typeId}/attributes")]
    public async Task<ActionResult<List<AdminAttributeDto>>> GetItemTypeAttributes(
        int typeId,
        CancellationToken cancellationToken)
    {
        var query = new GetAdminItemTypeAttributesQuery
        {
            TypeId = typeId
        };

        var result = await this._mediator.Send(
            query, 
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("categories")]
    public async Task<ActionResult<List<AdminCategoryDto>>> GetCategories(
        [FromQuery] string? searchTerm,
        CancellationToken cancellationToken)
    {
        var query = new GetAdminCategoriesQuery
        {
            SearchTerm = searchTerm
        };

        var result = await this._mediator.Send(
            query, 
            cancellationToken);

        return Ok(result);
    }

    [HttpPut("categories/{id}")]
    public async Task<IActionResult> UpdateCategory(
        int id, 
        [FromBody] string name, 
        CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryCommand
        {
            Id = id,
            Name = name
        };

        await this._mediator.Send(
            command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("categories/{id}")]
    public async Task<IActionResult> DeleteCategory(
        int id, 
        CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand 
        { 
            Id = id 
        };

        await this._mediator.Send(
            command, 
            cancellationToken);

        return NoContent();
    }
}
