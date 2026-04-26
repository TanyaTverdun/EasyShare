using MediatR;

namespace EasyShare.Application.Features.Bookings.Queries.GetUserBookingStats;

public record GetUserBookingStatsQuery : IRequest<BookingStatsDto>;
