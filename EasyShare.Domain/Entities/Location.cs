namespace EasyShare.Domain.Entities;

public class Location
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int Building { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Company> Companies { get; set; } = new List<Company>();
    public ICollection<Item> Items { get; set; } = new List<Item>();
}
