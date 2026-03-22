using apbd_eng_15_task3.Domain.Enums;

namespace apbd_eng_15_task3.Domain.Equipment;

public class Student : User
{
    public string StudentId { get; set; }

    public Student(string firstName, string lastName, string studentId)
        : base(firstName, lastName, UserType.Student)
    {
        StudentId = studentId;
    }

    public override int GetMaxActiveRentals()
    {
        return 2;
    }
}