namespace EasyShare.Domain.Entities;

public class ItemAttributeValue
{
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public int AttributeId { get; set; }
    public Attribute Attribute { get; set; } = null!;

    public string Value { get; set; } = string.Empty;
}
