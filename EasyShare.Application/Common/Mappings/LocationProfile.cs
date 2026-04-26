using AutoMapper;
using EasyShare.Application.Features.Locations.Queries;
using EasyShare.Domain.Entities;

namespace EasyShare.Application.Common.Mappings;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDto>();
    }
}
