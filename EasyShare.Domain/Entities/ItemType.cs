namespace EasyShare.Domain.Entities;

public class ItemType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();
    public ICollection<Item> Items { get; set; } = new List<Item>();

    public static ItemType Create(string name, int categoryId)
    {
        return new ItemType 
        { 
            Name = name, 
            CategoryId = categoryId, 
            IsDeleted = false 
        };
    }

    public void Delete()
    {
        if (IsDeleted) return;

        IsDeleted = true;

        foreach (var attribute in Attributes)
        {
            attribute.Delete();
        }
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException(
                "Назва не може бути порожньою.");
        }

        Name = newName.Trim();
    }
}
