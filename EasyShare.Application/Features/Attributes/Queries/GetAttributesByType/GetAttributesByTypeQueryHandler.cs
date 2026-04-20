using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Attributes.Queries.GetAttributesByType;

public class GetAttributesByTypeQueryHandler : IRequestHandler<GetAttributesByTypeQuery, List<TypeWithAttributesDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAttributesByTypeQueryHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<List<TypeWithAttributesDto>> Handle(GetAttributesByTypeQuery request, CancellationToken cancellationToken)
    {
        if (request.TypeIds == null || !request.TypeIds.Any())
        {
            return new List<TypeWithAttributesDto>();
        }

        return await this._context.ItemTypes
            .Where(it => request.TypeIds.Contains(it.Id) && !it.IsDeleted)
            .Select(it => new TypeWithAttributesDto
            {
                TypeId = it.Id,
                TypeName = it.Name,
                Attributes = it.Attributes
                    .Where(a => !a.IsDeleted)
                    .Select(a => new AttributeDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Values = a.ItemAttributeValues
                            .Select(iav => iav.Value)
                            .Distinct()
                    })
            })
            .ToListAsync(cancellationToken);
    }
}
