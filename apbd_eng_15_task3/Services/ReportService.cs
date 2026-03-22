namespace apbd_eng_15_task3.Services;
using apbd_eng_15_task3.Domain.Enums;
using apbd_eng_15_task3.Domain.Equipment;
public class ReportService
{
    private EquipmentService _equipmentService;
    private UserService _userService;
    private RentalService _rentalService;

    public ReportService(EquipmentService equipmentService, UserService userService, RentalService rentalService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _rentalService = rentalService;
    }

    public void PrintFullReport()
    {
        List<Equipment> allEquipment = _equipmentService.GetAll();
        List<User> allUsers = _userService.GetAll();
        List<Rental> allRentals = _rentalService.GetAllRentals();
        List<Rental> activeRentals = _rentalService.GetAllActiveRentals();
        List<Rental> overdueRentals = _rentalService.GetOverdueRentals();

        int availableCount = 0;
        int rentedCount = 0;
        int unavailableCount = 0;

        foreach (Equipment equipment in allEquipment)
        {
            if (equipment.Status == EquipmentStatus.Available)
                availableCount++;
            else if (equipment.Status == EquipmentStatus.Rented)
                rentedCount++;
            else
                unavailableCount++;
        }

        int studentCount = 0;
        int employeeCount = 0;

        foreach (User user in allUsers)
        {
            if (user is Student)
                studentCount++;
            else
                employeeCount++;
        }

        decimal totalPenalties = 0;
        foreach (Rental rental in allRentals)
        {
            if (rental.PenaltyFee != null)
            {
                totalPenalties += rental.PenaltyFee.Value;
            }
        }

        Console.WriteLine("==================== SYSTEM REPORT ====================");
        Console.WriteLine("Equipment total  : " + allEquipment.Count);
        Console.WriteLine("  Available      : " + availableCount);
        Console.WriteLine("  Rented         : " + rentedCount);
        Console.WriteLine("  Unavailable    : " + unavailableCount);
        Console.WriteLine("Users total      : " + allUsers.Count);
        Console.WriteLine("  Students       : " + studentCount);
        Console.WriteLine("  Employees      : " + employeeCount);
        Console.WriteLine("Rentals total    : " + allRentals.Count);
        Console.WriteLine("  Active         : " + activeRentals.Count);
        Console.WriteLine("  Overdue        : " + overdueRentals.Count);
        Console.WriteLine("Total penalties  : " + totalPenalties + " PLN");

        if (overdueRentals.Count > 0)
        {
            Console.WriteLine("---------------- OVERDUE RENTALS ----------------");
            foreach (Rental rental in overdueRentals)
            {
                Console.WriteLine("  " + rental.User.GetFullName() + " — " + rental.Equipment.Name + " (due: " + rental.DueDate.ToString("yyyy-MM-dd") + ")");
            }
        }

        Console.WriteLine("========================================================");
    }

    public void PrintEquipmentList(List<Equipment> items)
    {
        foreach (Equipment equipment in items)
        {
            Console.WriteLine("  [" + equipment.GetTypeName() + "] " + equipment.Name + " — " + equipment.Status);
            Console.WriteLine("    " + equipment.GetDetails());
        }
    }

    public void PrintRentalsForUser(User user, List<Rental> rentals)
    {
        Console.WriteLine("--- Active rentals for " + user.GetFullName() + " ---");

        if (rentals.Count == 0)
        {
            Console.WriteLine("  No active rentals.");
            return;
        }

        foreach (Rental rental in rentals)
        {
            Console.WriteLine("  " + rental.Equipment.Name + " — due: " + rental.DueDate.ToString("yyyy-MM-dd"));
        }
    }

    public void PrintOverdueRentals(List<Rental> rentals)
    {
        Console.WriteLine("--- Overdue rentals ---");

        if (rentals.Count == 0)
        {
            Console.WriteLine("  None.");
            return;
        }

        foreach (Rental rental in rentals)
        {
            Console.WriteLine("  " + rental.User.GetFullName() + " — " + rental.Equipment.Name + " (due: " + rental.DueDate.ToString("yyyy-MM-dd") + ")");
        }
    }
}