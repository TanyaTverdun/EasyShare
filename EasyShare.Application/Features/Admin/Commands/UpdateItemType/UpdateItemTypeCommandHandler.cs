using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Admin.Commands.UpdateItemType;

public class UpdateItemTypeCommandHandler 
    : IRequestHandler<UpdateItemTypeCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateItemTypeCommandHandler(
        IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Unit> Handle(
        UpdateItemTypeCommand request, 
        CancellationToken cancellationToken)
    {
        var isNameTaken = await this._context.ItemTypes
            .AnyAsync(t => t.Id != request.Id &&
                       !t.IsDeleted &&
                       t.Name.ToLower() == request.Name.ToLower(),
                       cancellationToken);

        if (isNameTaken)
        {
            throw new ConflictException(
                $"Тип товару з назвою '{request.Name}' вже існує.");
        }
        var itemType = await this._context.ItemTypes
            .FirstOrDefaultAsync(
                t => t.Id == request.Id && !t.IsDeleted, 
                cancellationToken);

        if (itemType == null)
        {
            throw new NotFoundException("Тип товару не знайдено.");
        }

        itemType.UpdateName(request.Name);

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
