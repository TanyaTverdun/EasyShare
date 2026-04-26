using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Admin.Queries.GetItemTypes;

public class GetAdminItemTypesQueryHandler 
    : IRequestHandler<GetAdminItemTypesQuery, List<AdminItemTypeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAdminItemTypesQueryHandler(
        IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<List<AdminItemTypeDto>> Handle(
        GetAdminItemTypesQuery request, 
        CancellationToken cancellationToken)
    {
        var query = this._context.ItemTypes
            .Where(t => !t.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim().ToLower();
            query = query.Where(t => t.Name.ToLower().Contains(search));
        }

        return await query
            .OrderBy(t => t.Name)
            .Select(t => new AdminItemTypeDto
            {
                Id = t.Id,
                Name = t.Name,
                AttributesCount = t.Attributes.Count(a => !a.IsDeleted)
            })
            .ToListAsync(cancellationToken);
    }
}
