using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Companies.Commands.DeactivateItem;

public class DeactivateItemCommandHandler
    : IRequestHandler<DeactivateItemCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public DeactivateItemCommandHandler(
        IApplicationDbContext context, 
        IUserContext userContext)
    {
        this._context = context;
        this._userContext = userContext;
    }

    public async Task<Unit> Handle(
        DeactivateItemCommand request, 
        CancellationToken cancellationToken)
    {
        var companyId = this._userContext.UserId;

        var item = await this._context.Items
            .FirstOrDefaultAsync(i => 
                i.Id == request.Id && 
                i.CompanyId == companyId,
                cancellationToken);

        if (item == null)
        {
            throw new NotFoundException(
                "Товар не знайдено або у вас немає прав на його редагування.");
        }

        item.Deactivate();

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
