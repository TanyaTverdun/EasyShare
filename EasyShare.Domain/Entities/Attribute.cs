namespace EasyShare.Domain.Entities;

public class Attribute
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public int TypeId { get; set; }
    public ItemType ItemType { get; set; } = null!;

    public ICollection<ItemAttributeValue> ItemAttributeValues { get; set; } = new List<ItemAttributeValue>();
}
