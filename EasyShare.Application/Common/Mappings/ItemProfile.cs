using AutoMapper;
using EasyShare.Application.Features.Companies.Queries.GetItems;
using EasyShare.Application.Features.Items.Queries.GetItemById;
using EasyShare.Domain.Entities;

namespace EasyShare.Application.Common.Mappings;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<Review, ItemReviewDto>()
            .ForMember(
                dest => dest.ReviewId,
                opt => opt.MapFrom(
                    src => src.Id))

            .ForMember(
                dest => dest.ReviewerName,
                opt => opt.MapFrom(src =>
                    $"{src.Booking.User.FirstName} {src.Booking.User.LastName}"
                    .Trim()));

        CreateMap<Item, ItemDetailsDto>()
            .ForMember(
                dest => dest.ItemId, 
                opt => opt.MapFrom(
                    src => src.Id))

            .ForMember(
                dest => dest.ImageUrl, 
                opt => opt.NullSubstitute(
                    string.Empty))

            .ForMember(
                dest => dest.BillingPeriod, 
                opt => opt.MapFrom(
                    src => src.BillingPeriod.ToFriendlyString()))

            .ForMember(
                dest => dest.City, 
                opt => opt.MapFrom(
                    src => src.Location.City))

            .ForMember(
                dest => dest.Street, 
                opt => opt.MapFrom(
                    src => src.Location.Street))

            .ForMember(
                dest => dest.Building, 
                opt => opt.MapFrom(
                    src => src.Location.Building))

            .ForMember(
                dest => dest.CategoryName, 
                opt => opt.MapFrom(
                    src => src.ItemType.Category.Name))

            .ForMember(
                dest => dest.ReviewsCount, 
                opt => opt.MapFrom(src =>
                    src.Bookings.SelectMany(b => b.Reviews).Count()))

            .ForMember(
                dest => dest.AverageRating, 
                opt => opt.MapFrom(src =>
                    src.Bookings.SelectMany(b => b.Reviews).Any()
                        ? Math.Round(
                            src.Bookings
                                .SelectMany(b => b.Reviews)
                                .Average(r => r.Rating), 
                            1)
                        : 0.0))

            .ForMember(
                dest => dest.Attributes, 
                opt => opt.MapFrom(src =>
                    src.ItemAttributeValues
                        .ToDictionary(iav => 
                            iav.Attribute.Name, 
                            iav => iav.Value)))

            .ForMember(
                dest => dest.Reviews, 
                opt => opt.MapFrom(src =>
                    src.Bookings
                        .SelectMany(b => b.Reviews)
                        .OrderByDescending(r => r.CreatedAt)));

        CreateMap<Item, CompanyItemDto>()
            .ForMember(
                dest => dest.BookingsCount, 
                opt => opt.MapFrom(src => src.Bookings.Count))

            .ForMember(
                dest => dest.Rating, 
                opt => opt.MapFrom(src =>
                    src.Bookings
                        .SelectMany(b => b.Reviews)
                        .Average(r => (double?)r.Rating) ?? 0));
    }
}
