using apbd_eng_15_task3.Domain.Enums;

namespace apbd_eng_15_task3.Domain.Equipment;

public abstract class Equipment {  
    public Guid Id { get; }
    public string Name { get; set; }
    public EquipmentStatus Status { get; set; }

    protected Equipment(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Status = EquipmentStatus.Available;
    }

    public bool IsAvailable()
    {
        return Status == EquipmentStatus.Available;
    }

    public abstract string GetTypeName();
    public abstract string GetDetails();
}