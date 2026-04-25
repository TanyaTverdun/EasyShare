using AutoMapper;
using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Bookings.Commands.ReturnBooking;

public class ReturnBookingCommandHandler : IRequestHandler<ReturnBookingCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;

    public ReturnBookingCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IMapper mapper)
    {
        this._context = context;
        this._userContext = userContext;
        this._mapper = mapper;
    }

    public async Task<Unit> Handle(
        ReturnBookingCommand request, 
        CancellationToken cancellationToken)
    {
        var booking = await this._context.Bookings
            .FirstOrDefaultAsync(b => 
                b.Id == request.BookingId, 
                cancellationToken);

        if (booking == null || booking.UserId != this._userContext.UserId)
        {
            throw new NotFoundException("Бронювання не знайдено.");
        }

        try
        {
            booking.RequestReturn();
        }
        catch (InvalidOperationException ex)
        {
            throw new BadRequestException(ex.Message);
        }

        if (request.Rating.HasValue)
        {
            var review = this._mapper.Map<Review>(request);
            this._context.Reviews.Add(review);
        }

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
