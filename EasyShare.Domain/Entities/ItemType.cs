namespace EasyShare.Domain.Entities;

public class ItemType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();
    public ICollection<Item> Items { get; set; } = new List<Item>();
}
