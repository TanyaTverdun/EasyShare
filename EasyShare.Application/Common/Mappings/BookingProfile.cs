using AutoMapper;
using EasyShare.Application.Features.Bookings.Commands.ReturnBooking;
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
                dest => dest.Quantity,
                opt => opt.MapFrom(src => src.RentedQuantity))

            .ForMember(
                dest => dest.Status, 
                opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<ReturnBookingCommand, Review>()

            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating!.Value))

            // Зашиваємо бізнес-правила прямо в мапер
            .ForMember(dest => dest.IsOwner, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))

            // Ігноруємо навігаційні властивості та Id
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Booking, opt => opt.Ignore());
    }
}
