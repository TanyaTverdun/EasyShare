using EasyShare.Domain.Enums;

namespace EasyShare.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public short RentedQuantity { get; set; }
    public DateTimeOffset StartDatetime { get; set; }
    public DateTimeOffset EndDatetime { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public void Cancel()
    {
        if (Status != BookingStatus.PendingConfirmation &&
            Status != BookingStatus.Confirmed)
        {
            throw new InvalidOperationException(
                "Це бронювання вже не можна скасувати.");
        }

        Status = BookingStatus.Cancelled;
    }
}
