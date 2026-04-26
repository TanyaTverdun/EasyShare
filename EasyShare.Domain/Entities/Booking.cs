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

    public void Confirm()
    {
        if (Status != BookingStatus.PendingConfirmation)
        {
            throw new InvalidOperationException(
                "Можна підтвердити лише бронювання, яке очікує підтвердження.");
        }

        Status = BookingStatus.Confirmed;
    }

    public void Issue()
    {
        if (Status != BookingStatus.Confirmed)
        {
            throw new InvalidOperationException(
                "Видати можна лише підтверджене бронювання.");
        }

        Status = BookingStatus.Active;
    }

    public void Complete()
    {
        if (Status != BookingStatus.PendingReturn && 
            Status != BookingStatus.Active)
        {
            throw new InvalidOperationException(
                "Завершити можна лише бронювання, що знаходиться в оренді або очікує повернення.");
        }

        Status = BookingStatus.Completed;
    }

    public void RequestReturn()
    {
        if (Status != BookingStatus.Active)
        {
            throw new InvalidOperationException(
                "Ви можете повернути тільки активне бронювання.");
        }

        Status = BookingStatus.PendingReturn;
    }
}
