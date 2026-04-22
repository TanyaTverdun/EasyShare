using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Features.Items.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Items.Queries.GetCatalogItems;

public class GetCatalogItemsQueryHandler : 
    IRequestHandler<GetCatalogItemsQuery, List<CatalogItemDto>>
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

    public async Task<List<CatalogItemDto>> Handle(
        GetCatalogItemsQuery request, 
        CancellationToken cancellationToken)
    {
        var query = _context.ItemCatalog.AsNoTracking().AsQueryable()
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

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<CatalogItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return items;
    }
}