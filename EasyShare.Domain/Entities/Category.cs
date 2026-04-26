namespace EasyShare.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public ICollection<ItemType> ItemTypes { get; set; } = new List<ItemType>();

    public static Category Create(string name) {
        return new Category 
        { 
            Name = name, 
            IsDeleted = false 
        };
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException(
                "Назва категорії не може бути порожньою.");
        }

        Name = newName.Trim();
    }

    public void Delete()
    {
        if (IsDeleted)
        {
            return;
        }

        IsDeleted = true;

        foreach (var itemType in ItemTypes)
        {
            itemType.Delete();
        }
    }
}
