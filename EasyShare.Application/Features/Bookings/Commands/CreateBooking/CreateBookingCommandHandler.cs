using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Common.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler 
    : IRequestHandler<CreateBookingCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public CreateBookingCommandHandler(
        IApplicationDbContext context, 
        IUserContext userContext)
    {
        this._context = context;
        this._userContext = userContext;
    }

    public async Task<Unit> Handle(
        CreateBookingCommand request, 
        CancellationToken cancellationToken)
    {
        var userId = this._userContext.UserId;

        try
        {
            await this._context.ExecuteCreateBookingProcedureAsync(
                userId, 
                request.ItemId, 
                request.Quantity, 
                request.StartDate, 
                request.EndDate, 
                cancellationToken);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Overbooking"))
            {
                throw new ConflictException(
                    "На жаль, на обрані дати немає достатньої кількості вільного товару.");
            }

            if (ex.Message.Contains("ItemNotFound"))
            {
                throw new NotFoundException(
                    "Товар не знайдено або він деактивований компанією.");
            }

            throw;
        }

        return Unit.Value;
    }
}