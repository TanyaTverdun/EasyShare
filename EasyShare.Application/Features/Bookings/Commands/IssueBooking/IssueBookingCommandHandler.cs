using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Bookings.Commands.IssueBooking;

public class IssueBookingCommandHandler 
    : IRequestHandler<IssueBookingCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public IssueBookingCommandHandler(
        IApplicationDbContext context, 
        IUserContext userContext)
    {
        this._context = context;
        this._userContext = userContext;
    }

    public async Task Handle(
        IssueBookingCommand request, 
        CancellationToken cancellationToken)
    {
        var companyId = this._userContext.UserId;

        var booking = await _context.Bookings
            .Include(b => b.Item)
            .FirstOrDefaultAsync(b =>
                b.Id == request.Id &&
                b.Item.CompanyId == companyId,
                cancellationToken);

        if (booking == null)
        {
            throw new NotFoundException(
                "Бронювання не знайдено або у вас немає доступу.");
        }

        try
        {
            booking.Issue();
        }
        catch (InvalidOperationException ex)
        {
            throw new ConflictException(ex.Message);
        }

        await this._context.SaveChangesAsync(cancellationToken);
    }
}
