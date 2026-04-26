using EasyShare.Application.Common.Models;
using EasyShare.Application.Features.Bookings.Enums;
using EasyShare.Domain.Enums;
using MediatR;

namespace EasyShare.Application.Features.Bookings.Queries.GetUserBookings
{
    public class GetUserBookingsQuery : IRequest<PagedResult<BookingItemDto>>
    {
        public string? SearchTerm { get; set; }
        public BookingStatus? StatusFilter { get; set; }
        public BookingSortOption SortBy { get; set; } = BookingSortOption.DateDesc;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
