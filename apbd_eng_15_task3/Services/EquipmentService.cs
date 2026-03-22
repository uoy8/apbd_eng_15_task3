namespace apbd_eng_15_task3.Services;
using apbd_eng_15_task3.Domain.Enums;
using apbd_eng_15_task3.Domain.Equipment;

public class EquipmentService
{
    private List<Equipment> _equipment = new List<Equipment>();

    public void AddEquipment(Equipment equipment)
    {
        _equipment.Add(equipment);
    }

    public List<Equipment> GetAll()
    {
        return _equipment;
    }

    public List<Equipment> GetAvailable()
    {
        List<Equipment> result = new List<Equipment>();

        foreach (Equipment equipment in _equipment)
        {
            if (equipment.IsAvailable())
            {
                result.Add(equipment);
            }
        }

        return result;
    }

    public void MarkUnavailable(Equipment equipment)
    {
        if (equipment.Status == EquipmentStatus.Rented)
        {
            throw new InvalidOperationException(equipment.Name + " is currently rented and cannot be marked unavailable.");
        }

        equipment.Status = EquipmentStatus.Unavailable;
    }
}