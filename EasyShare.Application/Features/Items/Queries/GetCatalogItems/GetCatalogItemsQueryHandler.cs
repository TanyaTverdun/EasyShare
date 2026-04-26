using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Common.Models;
using EasyShare.Application.Features.Items.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Items.Queries.GetCatalogItems;

public class GetCatalogItemsQueryHandler 
    : IRequestHandler<GetCatalogItemsQuery, PagedResult<CatalogItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCatalogItemsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<PagedResult<CatalogItemDto>> Handle(
        GetCatalogItemsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.ItemCatalog.AsNoTracking().AsQueryable()
            .Where(i => i.IsActive)
            .FilterBySearchTerm(
                request.SearchTerm)
            .FilterByCity(
                request.City)
            .FilterByHierarchy(
                request.CategoryIds, 
                request.TypeIds)
            .FilterByAttributes(
                request.Attributes, 
                this._context)
            .FilterByAvailability(
                request.MinQuantity, 
                request.AvailableFrom, 
                request.AvailableTo, 
                this._context)
            .ApplySorting(
                request.SortBy, 
                request.UserCity, 
                request.UserStreet, 
                request.UserBuilding);

        return await query
            .ProjectTo<CatalogItemDto>(this._mapper.ConfigurationProvider)
            .PaginatedListAsync(
                request.Page, 
                request.PageSize, 
                cancellationToken);
    }
}