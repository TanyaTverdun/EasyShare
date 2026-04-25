using AutoMapper;
using EasyShare.Application.Features.Companies.Commands.UpdateProfile;
using EasyShare.Application.Features.Companies.Queries.GetProfile;
using EasyShare.Domain.Entities;

namespace EasyShare.Application.Common.Mappings;

public class CompanyCabinetProfile : Profile
{
    public CompanyCabinetProfile()
    {
        CreateMap<UpdateCompanyProfileCommand, Company>()
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
                dest => dest.Items, 
                opt => opt.Ignore());

        CreateMap<Company, CompanyProfileResponse>()
            .ForMember(
                dest => dest.NewEmail, 
                opt => opt.MapFrom(src => src.Email))

            .ForMember(
                dest => dest.NewName, 
                opt => opt.MapFrom(src => src.Name))

            .ForMember(
                dest => dest.Token, 
                opt => opt.Ignore());

        CreateMap<Company, CompanyProfileDto>()
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
                    src.Location != null ? (int?)src.Location.Building : null));
    }
}
