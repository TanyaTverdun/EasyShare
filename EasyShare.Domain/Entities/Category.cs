namespace EasyShare.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<ItemType> ItemTypes { get; set; } = new List<ItemType>();
}
