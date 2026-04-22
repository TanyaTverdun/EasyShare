using EasyShare.Application.Features.Attributes.Queries.GetAttributesByType;
using EasyShare.Application.Features.Categories.Queries.GetActiveCategories;
using EasyShare.Application.Features.Items.Queries.GetCatalogItems;
using EasyShare.Application.Features.ItemTypes.Queries.GetTypesByCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyShare.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    // GET: api/catalog/categories
    [HttpGet("categories")]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories(
        CancellationToken cancellationToken)
    {
        var query = new GetActiveCategoriesQuery();
        var result = await this._mediator.Send(
            query, 
            cancellationToken);

        return Ok(result);
    }

    // GET: api/catalog/types?categoryIds=1&categoryIds=2
    [HttpGet("types")]
    public async Task<ActionResult<List<CategoryWithTypesDto>>> GetTypesByCotegoryIds(
        [FromQuery] List<int> categoryIds,
        CancellationToken cancellationToken)
    {
        var query = new GetTypesByCategoryQuery
        { 
            CategoryIds = categoryIds
        };

        var result = await this._mediator.Send(
            query, 
            cancellationToken);

        return Ok(result);
    }

    // GET: api/catalog/attributes?typeIds=1&typeIds=2
    [HttpGet("attributes")]
    public async Task<ActionResult<List<TypeWithAttributesDto>>> GetAttributesByType(
        [FromQuery] List<int> typeIds,
        CancellationToken cancellationToken)
    {
        var query = new GetAttributesByTypeQuery
        { 
            TypeIds = typeIds 
        };

        var result = await this._mediator.Send(
            query, 
            cancellationToken);

        return Ok(result);
    }

    // GET: api/catalog/items
    [HttpGet("items")]
    public async Task<ActionResult<List<CatalogItemDto>>> GetCatalogItems(
        [FromQuery] GetCatalogItemsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            query, 
            cancellationToken);

        return Ok(result);
    }
}
