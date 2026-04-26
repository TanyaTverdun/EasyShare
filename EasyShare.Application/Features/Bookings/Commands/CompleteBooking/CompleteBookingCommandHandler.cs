using AutoMapper;
using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Bookings.Commands.CompleteBooking;

public class CompleteBookingCommandHandler 
    : IRequestHandler<CompleteBookingCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;

    public CompleteBookingCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IMapper mapper)
    {
        this._context = context;
        this._userContext = userContext;
        this._mapper = mapper;
    }

    public async Task<Unit> Handle(
        CompleteBookingCommand request,
        CancellationToken cancellationToken)
    {
        var companyId = this._userContext.UserId;

        var booking = await this._context.Bookings
            .Include(b => b.Item)
            .FirstOrDefaultAsync(b =>
                b.Id == request.BookingId &&
                b.Item.CompanyId == companyId,
                cancellationToken);

        if (booking == null)
        {
            throw new NotFoundException(
                "Бронювання не знайдено або у вас немає доступу.");
        }

        try
        {
            booking.Complete();
        }
        catch (InvalidOperationException ex)
        {
            throw new ConflictException(ex.Message);
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
