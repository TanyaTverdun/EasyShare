using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Bookings.Commands.UpdateBooking;

public class UpdateBookingCommandHandler 
    : IRequestHandler<UpdateBookingCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public UpdateBookingCommandHandler(
        IApplicationDbContext context, 
        IUserContext userContext)
    {
        this._context = context;
        this._userContext = userContext;
    }

    public async Task<Unit> Handle(
        UpdateBookingCommand request, 
        CancellationToken cancellationToken)
    {
        var userId = this._userContext.UserId;

        var ownsBooking = await this._context.Bookings
            .AnyAsync(b => 
                b.Id == request.BookingId && 
                b.UserId == userId, 
                cancellationToken);

        if (!ownsBooking)
        {
            throw new UnauthorizedException(
                "Бронювання не знайдено або у вас немає доступу.");
        }

        try
        {
            await this._context.ExecuteUpdateBookingProcedureAsync(
                request.BookingId,
                request.StartDate.UtcDateTime,
                request.EndDate.UtcDateTime,
                request.Quantity,
                cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ConflictException(ex.Message);
        }

        return Unit.Value;
    }
}
