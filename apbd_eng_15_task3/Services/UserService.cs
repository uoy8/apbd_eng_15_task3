namespace apbd_eng_15_task3.Services;
using apbd_eng_15_task3.Domain.Enums;
using apbd_eng_15_task3.Domain.Equipment;
public class UserService
{
    private List<User> _users = new List<User>();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public List<User> GetAll()
    {
        return _users;
    }
}