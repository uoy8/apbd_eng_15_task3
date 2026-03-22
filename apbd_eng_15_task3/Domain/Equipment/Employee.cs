namespace apbd_eng_15_task3.Domain.Equipment;

public class Employee : User
{
    public string Department { get; set; }

    public Employee(string firstName, string lastName, string department)
        : base(firstName, lastName, Enums.UserType.Employee)
    {
        Department = department;
    }

    public override int GetMaxActiveRentals()
    {
        return 5;
    }
}