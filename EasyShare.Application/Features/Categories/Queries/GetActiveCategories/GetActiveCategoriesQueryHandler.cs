using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Categories.Queries.GetActiveCategories
{
    public class GetActiveCategoriesQueryHandler : IRequestHandler<GetActiveCategoriesQuery, List<CategoryDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetActiveCategoriesQueryHandler(IApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<List<CategoryDto>> Handle(
            GetActiveCategoriesQuery request, 
            CancellationToken cancellationToken)
        {
            return await this._context.Categories
                .Where(c => !c.IsDeleted)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync(cancellationToken);
        }
    }
}
