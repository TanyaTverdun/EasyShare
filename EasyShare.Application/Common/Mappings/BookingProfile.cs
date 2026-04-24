using AutoMapper;
using EasyShare.Application.Features.Bookings.Queries.GetUserBookings;
using EasyShare.Domain.Entities;

namespace EasyShare.Application.Common.Mappings;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking, BookingItemDto>()
            .ForMember(
                dest => dest.ItemName, 
                opt => opt.MapFrom(src => src.Item.Name))

            .ForMember(
                dest => dest.ItemImageUrl, 
                opt => opt.MapFrom(src => src.Item.ImageUrl))

            .ForMember(
                dest => dest.CategoryName, 
                opt => opt.MapFrom(src => src.Item.ItemType.Name))

            .ForMember(
                dest => dest.StartDate, 
                opt => opt.MapFrom(src => src.StartDatetime))

            .ForMember(
                dest => dest.EndDate, 
                opt => opt.MapFrom(src => src.EndDatetime))

            .ForMember(
                dest => dest.Status, 
                opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
