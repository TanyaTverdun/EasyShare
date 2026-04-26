using EasyShare.Application.Common.Interfaces;
using EasyShare.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler 
    : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<int> Handle(
        CreateCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var existing = await this._context.Categories
            .FirstOrDefaultAsync(c => 
                c.Name.ToLower() == request.Name.ToLower(), 
                cancellationToken);

        if (existing != null)
        {
            return existing.Id;
        }

        var category = Category.Create(request.Name);

        this._context.Categories.Add(category);
        await this._context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
