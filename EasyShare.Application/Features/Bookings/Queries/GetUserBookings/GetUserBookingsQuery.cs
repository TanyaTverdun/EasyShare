using EasyShare.Application.Features.Bookings.Enums;
using EasyShare.Domain.Enums;
using MediatR;

namespace EasyShare.Application.Features.Bookings.Queries.GetUserBookings
{
    public class GetMyBookingsQuery : IRequest<List<BookingItemDto>>
    {
        public string? SearchTerm { get; set; }
        public BookingStatus? StatusFilter { get; set; }
        public BookingSortOption SortBy { get; set; } = BookingSortOption.DateDesc;
    }
}
