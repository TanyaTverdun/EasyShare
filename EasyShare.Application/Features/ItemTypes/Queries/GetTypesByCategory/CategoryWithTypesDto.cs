namespace EasyShare.Application.Features.ItemTypes.Queries.GetTypesByCategory;

public record CategoryWithTypesDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public IEnumerable<ItemTypeDto> Types { get; set; }
}
