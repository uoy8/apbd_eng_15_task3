namespace apbd_eng_15_task3.Services;

using apbd_eng_15_task3.Domain.Enums;
using apbd_eng_15_task3.Domain.Equipment;

public class RentalService
{
    private List<Rental> _rentals = new List<Rental>();

    public Rental RentEquipment(User user, Equipment equipment, int rentalDays)
    {
        if (!equipment.IsAvailable())
        {
            throw new InvalidOperationException(equipment.Name + " is not available for rental.");
        }

        List<Rental> activeRentals = GetActiveRentalsForUser(user);
        if (activeRentals.Count >= user.GetMaxActiveRentals())
        {
            throw new InvalidOperationException(user.GetFullName() + " has reached the rental limit of " + user.GetMaxActiveRentals() + ".");
        }

        Rental rental = new Rental(user, equipment, DateTime.Now, rentalDays);
        equipment.Status = EquipmentStatus.Rented;
        _rentals.Add(rental);

        return rental;
    }

    public Rental ReturnEquipment(Rental rental, DateTime returnedAt)
    {
        if (rental.IsReturned())
        {
            throw new InvalidOperationException("This rental has already been returned.");
        }

        decimal penalty = RentalPolicy.CalculatePenalty(rental.DueDate, returnedAt);
        rental.RecordReturn(returnedAt, penalty);
        rental.Equipment.Status = EquipmentStatus.Available;

        return rental;
    }

    public List<Rental> GetActiveRentalsForUser(User user)
    {
        List<Rental> result = new List<Rental>();

        foreach (Rental rental in _rentals)
        {
            if (rental.User.Id == user.Id && !rental.IsReturned())
            {
                result.Add(rental);
            }
        }

        return result;
    }

    public List<Rental> GetAllActiveRentals()
    {
        List<Rental> result = new List<Rental>();

        foreach (Rental rental in _rentals)
        {
            if (!rental.IsReturned())
            {
                result.Add(rental);
            }
        }

        return result;
    }

    public List<Rental> GetOverdueRentals()
    {
        List<Rental> result = new List<Rental>();

        foreach (Rental rental in _rentals)
        {
            if (rental.IsOverdue())
            {
                result.Add(rental);
            }
        }

        return result;
    }

    public List<Rental> GetAllRentals()
    {
        return _rentals;
    }
}