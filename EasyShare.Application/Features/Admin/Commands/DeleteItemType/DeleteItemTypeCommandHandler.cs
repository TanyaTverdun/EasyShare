using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Admin.Commands.DeleteItemType;

public class DeleteItemTypeCommandHandler 
    : IRequestHandler<DeleteItemTypeCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteItemTypeCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Unit> Handle(
        DeleteItemTypeCommand request, 
        CancellationToken cancellationToken)
    {
        var itemType = await this._context.ItemTypes
            .FirstOrDefaultAsync(
                t => t.Id == request.Id, 
                cancellationToken);

        if (itemType == null)
        {
            throw new NotFoundException("Тип товару не знайдено.");
        }

        itemType.Delete();

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
