namespace EasyShare.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public ICollection<ItemType> ItemTypes { get; set; } = new List<ItemType>();
}
