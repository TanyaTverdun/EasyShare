using AutoMapper;
using EasyShare.Application.Features.Users.Commands.UpdateProfile;
using EasyShare.Application.Features.Users.Queries.GetProfile;
using EasyShare.Domain.Entities;

namespace EasyShare.Application.Common.Mappings;

public class UserCabinetProfile : Profile
{
    public UserCabinetProfile()
    {
        CreateMap<User, UserProfileDto>()
            .ForMember(
                dest => dest.City,
                opt => opt.MapFrom(src => 
                    src.Location != null ? src.Location.City : null))

            .ForMember(
                dest => dest.Street,
                opt => opt.MapFrom(src => 
                    src.Location != null ? src.Location.Street : null))

            .ForMember(
                dest => dest.Building,
                opt => opt.MapFrom(src => 
                    src.Location != null ? src.Location.Building : (int?)null));

        CreateMap<UpdateUserProfileCommand, User>()
            .ForMember(
                dest => dest.FirstName, 
                opt => opt.MapFrom(
                    src => src.FullName.
                        Trim()
                        .Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[0]))

            .ForMember(
                dest => dest.LastName, 
                opt => opt.MapFrom(src => src.FullName
                    .Trim()
                    .Split(' ', 2, StringSplitOptions.RemoveEmptyEntries).Length > 1
                        ? src.FullName
                            .Trim()
                            .Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]
                        : string.Empty))

            .ForPath(
                dest => dest.Location.City, 
                opt => opt.MapFrom(src => src.City ?? string.Empty))

            .ForPath(
                dest => dest.Location.Street, 
                opt => opt.MapFrom(src => src.Street ?? string.Empty))

            .ForPath(
                dest => dest.Location.Building, 
                opt => opt.MapFrom(src => src.Building ?? 0))

            .ForMember(
                dest => dest.Id, 
                opt => opt.Ignore())

            .ForMember(
                dest => dest.PasswordHash, 
                opt => opt.Ignore())

            .ForMember(
                dest => dest.LocationId, 
                opt => opt.Ignore())

            .ForMember(
                dest => dest.IsAdmin, 
                opt => opt.Ignore())

            .ForMember(
                dest => dest.Bookings, 
                opt => opt.Ignore());

        CreateMap<User, UserProfileResponse>()
            .ForMember(
                dest => dest.NewEmail, 
                opt => opt.MapFrom(src => src.Email))

            .ForMember(
                dest => dest.NewName, 
                opt => opt.MapFrom(src => src.FirstName))

            .ForMember(
                dest => dest.Token, 
                opt => opt.Ignore());
    }
}
