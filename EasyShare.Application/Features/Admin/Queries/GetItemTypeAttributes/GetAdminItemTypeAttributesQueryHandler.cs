using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Admin.Queries.GetItemTypeAttributes;

public class GetAdminItemTypeAttributesQueryHandler
: IRequestHandler<GetAdminItemTypeAttributesQuery, List<AdminAttributeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAdminItemTypeAttributesQueryHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<List<AdminAttributeDto>> Handle(
        GetAdminItemTypeAttributesQuery request,
        CancellationToken cancellationToken)
    {
        return await this._context.Attributes
            .Where(a => a.TypeId == request.TypeId && !a.IsDeleted)
            .Select(a => new AdminAttributeDto
            {
                Id = a.Id,
                Name = a.Name
            })
            .ToListAsync(cancellationToken);
    }
}
