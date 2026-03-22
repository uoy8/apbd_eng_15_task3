using apbd_eng_15_task3.Domain.Equipment;
using apbd_eng_15_task3.Services;
using apbd_eng_15_task3.ConsoleUI;

EquipmentService equipmentService = new EquipmentService();
UserService userService = new UserService();
RentalService rentalService = new RentalService();
ReportService reportService = new ReportService(equipmentService, userService, rentalService);
ConsoleUI ui = new ConsoleUI(equipmentService, rentalService, reportService);

ui.PrintHeader("Step 1 - Adding equipment");

Laptop laptop1 = new Laptop("Dell XPS 15", "Windows 11", 16);
Laptop laptop2 = new Laptop("MacBook Pro", "macOS", 32);
Projector projector = new Projector("Epson EB-X51", 1024, 768);
Camera camera1 = new Camera("Canon EOS R50", 24, true);
Camera camera2 = new Camera("Sony A7 III", 33, true);

equipmentService.AddEquipment(laptop1);
equipmentService.AddEquipment(laptop2);
equipmentService.AddEquipment(projector);
equipmentService.AddEquipment(camera1);
equipmentService.AddEquipment(camera2);

ui.ShowAllEquipment();

ui.PrintHeader("Step 2 - Adding users");

Student student1 = new Student("Anna", "Kowalska", "s12345");
Student student2 = new Student("Marek", "Nowak", "s67890");
Employee employee1 = new Employee("Jan", "Wisniewski", "Computer Science");

userService.AddUser(student1);
userService.AddUser(student2);
userService.AddUser(employee1);

Console.WriteLine("  Added: " + student1.GetFullName() + ", " + student2.GetFullName() + ", " + employee1.GetFullName());

ui.PrintHeader("Step 3 - Valid rentals");

ui.TryRent(student1, laptop1, 7);
ui.TryRent(student1, camera1, 5);
ui.TryRent(employee1, projector, 3);
ui.TryRent(employee1, camera2, 10);

ui.PrintHeader("Step 4 - Invalid operations");

Console.WriteLine("  Trying to rent a 3rd item to student (limit is 2)...");
ui.TryRent(student1, laptop2, 7);

Console.WriteLine("  Trying to mark a rented projector as unavailable...");
ui.TryMarkUnavailable(projector);

Console.WriteLine("  Marking laptop2 unavailable then trying to rent it...");
ui.TryMarkUnavailable(laptop2);
ui.TryRent(student2, laptop2, 7);

ui.PrintHeader("Step 5 - On time return");

List<Rental> student1Rentals = rentalService.GetActiveRentalsForUser(student1);
Rental cameraRental = null;

foreach (Rental r in student1Rentals)
{
    if (r.Equipment.Id == camera1.Id)
    {
        cameraRental = r;
    }
}

ui.TryReturn(cameraRental, cameraRental.DueDate.AddDays(-1));

ui.PrintHeader("Step 6 - Late return with penalty");

List<Rental> student1RentalsAfter = rentalService.GetActiveRentalsForUser(student1);
Rental laptopRental = null;

foreach (Rental r in student1RentalsAfter)
{
    if (r.Equipment.Id == laptop1.Id)
    {
        laptopRental = r;
    }
}

ui.TryReturn(laptopRental, laptopRental.DueDate.AddDays(5));

ui.PrintHeader("Step 7 - Active rentals per user");

ui.ShowActiveRentalsForUser(student1);
ui.ShowActiveRentalsForUser(employee1);

ui.PrintHeader("Step 8 - Overdue rentals");
ui.ShowOverdueRentals();

ui.PrintHeader("Step 9 - Available equipment");
ui.ShowAvailableEquipment();

ui.PrintHeader("Step 10 - Final report");
ui.ShowFullReport();