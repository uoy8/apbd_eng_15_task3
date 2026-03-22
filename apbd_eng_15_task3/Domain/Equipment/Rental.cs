namespace apbd_eng_15_task3.Domain.Equipment;

public class Rental
{
    public Guid Id { get; }
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentedAt { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnedAt { get; private set; }
    public decimal? PenaltyFee { get; private set; }

    public Rental(User user, Equipment equipment, DateTime rentedAt, int rentalDays)
    {
        Id = Guid.NewGuid();
        User = user;
        Equipment = equipment;
        RentedAt = rentedAt;
        DueDate = rentedAt.AddDays(rentalDays);
    }

    public bool IsReturned()
    {
        return ReturnedAt != null;
    }

    public bool IsOverdue()
    {
        return !IsReturned() && DateTime.Now > DueDate;
    }

    public void RecordReturn(DateTime returnedAt, decimal penaltyFee)
    {
        ReturnedAt = returnedAt;
        PenaltyFee = penaltyFee;
    }
}