using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.ItemTypes.Commands.CreateItemType;

public class CreateItemTypeCommandHandler 
    : IRequestHandler<CreateItemTypeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateItemTypeCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<int> Handle(
        CreateItemTypeCommand request, 
        CancellationToken cancellationToken)
    {
        var categoryExists = await this._context.Categories
            .AnyAsync(c => 
                c.Id == request.CategoryId, 
                cancellationToken);

        if (!categoryExists)
        {
            throw new NotFoundException("Категорію не знайдено.");
        }

        var existing = await this._context.ItemTypes
            .FirstOrDefaultAsync(t =>
                t.CategoryId == request.CategoryId &&
                t.Name.ToLower() == request.Name.ToLower(), cancellationToken);

        if (existing != null)
        {
            return existing.Id;
        }

        var type = ItemType.Create(request.Name, request.CategoryId);

        this._context.ItemTypes.Add(type);
        await this._context.SaveChangesAsync(cancellationToken);

        return type.Id;
    }
}
