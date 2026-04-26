using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Attributes.Commands.DeleteAttribute;

public class DeleteAttributeCommandHandler 
    : IRequestHandler<DeleteAttributeCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteAttributeCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Unit> Handle(
        DeleteAttributeCommand request, 
        CancellationToken cancellationToken)
    {
        var attribute = await this._context.Attributes
            .FirstOrDefaultAsync(a => 
                a.Id == request.Id, 
                cancellationToken);

        if (attribute == null)
        {
            throw new NotFoundException("Характеристику не знайдено.");
        }

        attribute.Delete();

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
