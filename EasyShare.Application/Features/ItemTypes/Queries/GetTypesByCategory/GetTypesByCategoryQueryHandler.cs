using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.ItemTypes.Queries.GetTypesByCategory;

public class GetTypesByCategoryQueryHandler : IRequestHandler<GetTypesByCategoryQuery, List<CategoryWithTypesDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTypesByCategoryQueryHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<List<CategoryWithTypesDto>> Handle(GetTypesByCategoryQuery request, CancellationToken cancellationToken)
    {
        if (request.CategoryIds == null || !request.CategoryIds.Any())
        {
            return new List<CategoryWithTypesDto>();
        }

        return await this._context.Categories
            .Where(c => request.CategoryIds.Contains(c.Id) && !c.IsDeleted)
            .Select(c => new CategoryWithTypesDto
            {
                CategoryId = c.Id,
                CategoryName = c.Name,
                Types = c.ItemTypes
                    .Where(t => !t.IsDeleted)
                    .Select(t => new ItemTypeDto
                    {
                        Id = t.Id,
                        Name = t.Name
                    })
            })
            .ToListAsync(cancellationToken);
    }
}
