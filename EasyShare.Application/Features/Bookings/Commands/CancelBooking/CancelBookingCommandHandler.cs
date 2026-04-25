using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Bookings.Commands.CancelBooking;

public class CancelBookingCommandHandler 
    : IRequestHandler<CancelBookingCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public CancelBookingCommandHandler(
        IApplicationDbContext context, 
        IUserContext userContext)
    {
        this._context = context;
        this._userContext = userContext;
    }

    public async Task<Unit> Handle(
        CancelBookingCommand request, 
        CancellationToken cancellationToken)
    {
        var userId = this._userContext.UserId;

        var booking = await this._context.Bookings
            .FirstOrDefaultAsync(b => 
                b.Id == request.BookingId && 
                b.UserId == userId, 
                cancellationToken);

        if (booking == null)
        {
            throw new UnauthorizedException(
                "Бронювання не знайдено або у вас немає доступу.");
        }

        try
        {
            booking.Cancel();
        }
        catch (InvalidOperationException ex)
        {
            throw new ConflictException(ex.Message);
        }

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
