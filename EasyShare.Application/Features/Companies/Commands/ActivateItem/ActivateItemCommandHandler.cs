using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Companies.Commands.ActivateItem;

public class ActivateItemCommandHandler 
    : IRequestHandler<ActivateItemCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public ActivateItemCommandHandler(
        IApplicationDbContext context, 
        IUserContext userContext)
    {
        this._context = context;
        this._userContext = userContext;
    }

    public async Task<Unit> Handle(
        ActivateItemCommand request, 
        CancellationToken cancellationToken)
    {
        var companyId = this._userContext.UserId;

        var item = await _context.Items
            .FirstOrDefaultAsync(i => 
                i.Id == request.Id && 
                i.CompanyId == companyId, 
                cancellationToken);

        if (item == null)
        {
            throw new NotFoundException("Товар не знайдено");
        }

        item.Activate();

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
