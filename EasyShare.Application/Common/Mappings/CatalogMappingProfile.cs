using AutoMapper;
using EasyShare.Application.Features.Items.Queries.GetCatalogItems;
using EasyShare.Domain.Entities;

namespace EasyShare.Application.Common.Mappings;

public class CatalogMappingProfile : Profile
{
    public CatalogMappingProfile()
    {
        CreateMap<ItemCatalogView, CatalogItemDto>();
    }
}
