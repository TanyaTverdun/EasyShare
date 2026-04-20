namespace EasyShare.Application.Features.Attributes.Queries.GetAttributesByType;

public class TypeWithAttributesDto
{
    public int TypeId { get; set; }
    public string TypeName { get; set; }
    public IEnumerable<AttributeDto> Attributes { get; set; }
}
