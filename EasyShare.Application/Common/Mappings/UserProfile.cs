using AutoMapper;
using EasyShare.Application.Features.Auth.Queries.Login;
using EasyShare.Domain.Entities;
using EasyShare.Domain.Enums;

namespace EasyShare.Application.Common.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, AuthResponseDto>()
            .ForMember(dest => 
                dest.DisplayName, 
                opt => opt.MapFrom(src => 
                    src.IsAdmin ? "Admin" : src.FirstName))

            .ForMember(dest => 
                dest.Role, 
                opt => opt.MapFrom(src => 
                    src.IsAdmin ? AccountType.Admin : AccountType.User))

            .ForMember(dest => 
                dest.Token, 
                opt => opt.Ignore());
    }
}