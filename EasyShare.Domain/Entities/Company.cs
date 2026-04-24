namespace EasyShare.Domain.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public int? LocationId { get; set; }
    public Location? Location { get; set; } = null!;

    public ICollection<Item> Items { get; set; } = new List<Item>();

    public static Company Create(string name, string email, string phone, string passwordHash)
    {
        return new Company
        {
            Name = name,
            Email = email,
            Phone = phone,
            PasswordHash = passwordHash
        };
    }
}
