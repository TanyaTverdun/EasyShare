using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Admin.Queries.GetCategories;

public class GetAdminCategoriesQueryHandler 
    : IRequestHandler<GetAdminCategoriesQuery, List<AdminCategoryDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAdminCategoriesQueryHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<List<AdminCategoryDto>> Handle(
        GetAdminCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        var query = this._context.Categories
            .Where(c => !c.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim().ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(search));
        }

        return await query
            .OrderBy(c => c.Name)
            .Select(c => new AdminCategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync(cancellationToken);
    }
}
