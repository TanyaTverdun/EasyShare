using EasyShare.Domain.Enums;
using MediatR;

namespace EasyShare.Application.Features.Companies.Queries.GetBookings;

public class GetCompanyBookingsQuery : IRequest<List<CompanyBookingDto>>
{
    public string? SearchTerm { get; set; } // Змінили init на set
    public BookingStatus? StatusFilter { get; set; } // Змінили назву для безпеки
}
