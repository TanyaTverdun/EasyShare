using AutoMapper;
using EasyShare.Application.Features.Auth.Queries.Login;
using EasyShare.Domain.Entities;
using EasyShare.Domain.Enums;

namespace EasyShare.Application.Common.Mappings;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, AuthResponseDto>()
            .ForMember(
                dest => dest.DisplayName, 
                opt => opt.MapFrom(src => src.Name))

            .ForMember(
                dest => dest.Role, 
                opt => opt.MapFrom(src => AccountType.Company))

            .ForMember(
                dest => dest.Token, 
                opt => opt.Ignore());
    }
}