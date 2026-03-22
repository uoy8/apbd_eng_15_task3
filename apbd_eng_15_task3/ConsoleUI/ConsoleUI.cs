namespace apbd_eng_15_task3.ConsoleUI;
using apbd_eng_15_task3.Domain.Enums;
using apbd_eng_15_task3.Domain.Equipment;
using apbd_eng_15_task3.Services;

public class ConsoleUI
{
    private EquipmentService _equipmentService;
    private RentalService _rentalService;
    private ReportService _reportService;

    public ConsoleUI(EquipmentService equipmentService, RentalService rentalService, ReportService reportService)
    {
        _equipmentService = equipmentService;
        _rentalService = rentalService;
        _reportService = reportService;
    }

    public void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  OK: " + message);
        Console.ResetColor();
    }

    public void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  ERROR: " + message);
        Console.ResetColor();
    }

    public void PrintHeader(string title)
    {
        Console.WriteLine();
        Console.WriteLine("=== " + title + " ===");
    }

    public void TryRent(User user, Equipment equipment, int days)
    {
        try
        {
            Rental rental = _rentalService.RentEquipment(user, equipment, days);
            PrintSuccess("Rented '" + equipment.Name + "' to " + user.GetFullName() + " for " + days + " days. Due: " + rental.DueDate.ToString("yyyy-MM-dd"));
        }
        catch (InvalidOperationException ex)
        {
            PrintError(ex.Message);
        }
    }

    public void TryReturn(Rental rental, DateTime returnedAt)
    {
        try
        {
            _rentalService.ReturnEquipment(rental, returnedAt);

            if (rental.PenaltyFee > 0)
            {
                PrintError("'" + rental.Equipment.Name + "' returned LATE. Penalty: " + rental.PenaltyFee + " PLN");
            }
            else
            {
                PrintSuccess("'" + rental.Equipment.Name + "' returned on time. No penalty.");
            }
        }
        catch (InvalidOperationException ex)
        {
            PrintError(ex.Message);
        }
    }

    public void TryMarkUnavailable(Equipment equipment)
    {
        try
        {
            _equipmentService.MarkUnavailable(equipment);
            PrintSuccess("'" + equipment.Name + "' marked as unavailable.");
        }
        catch (InvalidOperationException ex)
        {
            PrintError(ex.Message);
        }
    }

    public void ShowAllEquipment()
    {
        PrintHeader("All equipment");
        _reportService.PrintEquipmentList(_equipmentService.GetAll());
    }

    public void ShowAvailableEquipment()
    {
        PrintHeader("Available equipment");
        _reportService.PrintEquipmentList(_equipmentService.GetAvailable());
    }

    public void ShowActiveRentalsForUser(User user)
    {
        _reportService.PrintRentalsForUser(user, _rentalService.GetActiveRentalsForUser(user));
    }

    public void ShowOverdueRentals()
    {
        _reportService.PrintOverdueRentals(_rentalService.GetOverdueRentals());
    }

    public void ShowFullReport()
    {
        _reportService.PrintFullReport();
    }
}