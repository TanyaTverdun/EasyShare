namespace EasyShare.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public bool IsOwner { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;
}
