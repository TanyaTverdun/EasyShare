namespace EasyShare.Application.Features.Attributes.Queries.GetAttributesByType;

public class AttributeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> Values { get; set; }
}
