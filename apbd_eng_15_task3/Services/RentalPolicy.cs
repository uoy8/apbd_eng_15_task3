namespace apbd_eng_15_task3.Services;

public class RentalPolicy
{
    public const int PenaltyPerDay = 15;
    public const int DefaultRentalDays = 7;

    public static decimal CalculatePenalty(DateTime dueDate, DateTime returnedAt)
    {
        if (returnedAt <= dueDate)
        {
            return 0;
        }

        int daysLate = (int)(returnedAt - dueDate).TotalDays;
        return daysLate * PenaltyPerDay;
    }
}