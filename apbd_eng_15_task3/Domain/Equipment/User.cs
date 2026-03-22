using apbd_eng_15_task3.Domain.Enums;

namespace apbd_eng_15_task3.Domain.Equipment;

public abstract class User
{
    public Guid Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public static UserType UserType { get; set; }

    protected User(string firstName, string lastName, UserType userType)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
    }

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }

    public abstract int GetMaxActiveRentals();
}