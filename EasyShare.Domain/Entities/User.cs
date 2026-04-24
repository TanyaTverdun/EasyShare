namespace EasyShare.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public string PasswordHash { get; set; } = string.Empty;

    public int? LocationId { get; set; }
    public Location? Location { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string phone,
        string passwordHash)
    {
        return new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            PasswordHash = passwordHash,
            IsAdmin = false
        };
    }
}
