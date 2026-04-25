using AutoMapper;
using EasyShare.Application.Features.Items.Queries.GetCatalogItems;
using EasyShare.Domain.Entities;

namespace EasyShare.Application.Common.Mappings;

public class CatalogProfile : Profile
{
    public CatalogProfile()
    {
        CreateMap<ItemCatalogView, CatalogItemDto>();
    }
}
