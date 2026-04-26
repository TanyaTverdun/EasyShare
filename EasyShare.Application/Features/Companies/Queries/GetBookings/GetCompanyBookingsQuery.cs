using EasyShare.Application.Common.Models;
using EasyShare.Domain.Enums;
using MediatR;

namespace EasyShare.Application.Features.Companies.Queries.GetBookings;

public class GetCompanyBookingsQuery 
    : IRequest<PagedResult<CompanyBookingDto>>
{
    public string? SearchTerm { get; set; }
    public BookingStatus? StatusFilter { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
