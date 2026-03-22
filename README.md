# Equipment Rental System — APBD Tutorial 2

A C# console application for managing university equipment rentals.

## How to run

1. Clone the repository
2. Open the project in Rider or Visual Studio
3. Run the project with `dotnet run`

Requires .NET 8 or later.

## Project structure
```
apbd_eng_15_task3/
├── Domain/
│   ├── Enums/
│   │   ├── EquipmentStatus.cs
│   │   └── UserType.cs
│   └── Equipment/
│       ├── Equipment.cs
│       ├── Laptop.cs
│       ├── Projector.cs
│       ├── Camera.cs
│       ├── User.cs
│       ├── Student.cs
│       ├── Employee.cs
│       └── Rental.cs
├── Services/
│   ├── RentalPolicy.cs
│   ├── RentalService.cs
│   ├── EquipmentService.cs
│   ├── UserService.cs
│   └── ReportService.cs
├── ConsoleUI/
│   └── ConsoleUI.cs
└── Program.cs
```

## Design decisions

### Separation of responsibilities
I split the code into three layers. The Domain folder contains only the data models
with no logic. The Services folder contains all the business logic. The ConsoleUI folder
contains only the console output. This way each class has one clear job and changing
one part does not break the others.

### Abstract classes
Equipment and User are abstract classes because they represent a general concept,
not a real object. You cannot create a plain Equipment — you have to create a Laptop,
Projector or Camera. The abstract class holds the shared fields like Name and Status,
and forces each subclass to implement GetTypeName() and GetDetails().

The same applies to User. Student and Employee both extend User, but each returns
a different rental limit from GetMaxActiveRentals(). This means the limit lives
in the class itself and not in a bunch of if statements scattered across the code.

### RentalPolicy
All business rules like the penalty rate and default rental days are in one place —
RentalPolicy.cs. If the penalty changes from 15 PLN to 20 PLN, I only change it
in one file.

### Cohesion and coupling
Each service only does one thing. RentalService handles renting and returning.
EquipmentService manages the equipment list. ReportService only prints reports.
They do not depend on each other except where necessary, and all the wiring
happens in Program.cs.

## Branch strategy

- `feature-domain-equipment` — domain models (Equipment, User, Rental, Enums)
- `feature/services` — service layer (RentalService, EquipmentService, etc.)
- Both branches were merged into `master`
