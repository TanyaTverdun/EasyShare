using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Admin.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Unit> Handle(
        UpdateCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var category = await this._context.Categories
            .FirstOrDefaultAsync(
                c => c.Id == request.Id && !c.IsDeleted, 
                cancellationToken);

        if (category == null)
        {
            throw new NotFoundException("Категорію не знайдено.");
        }

        var isNameTaken = await this._context.Categories
            .AnyAsync(c => c.Id != request.Id &&
                           !c.IsDeleted &&
                           c.Name.ToLower() == request.Name.ToLower(),
                           cancellationToken);

        if (isNameTaken)
        {
            throw new InvalidOperationException(
                $"Категорія з назвою '{request.Name}' вже існує.");
        }

        category.UpdateName(request.Name);

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
