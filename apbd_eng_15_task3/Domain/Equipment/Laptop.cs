namespace apbd_eng_15_task3.Domain.Equipment;

public class Laptop : Equipment
{
    public string OperatingSystem { get; set; }
    public int RamGb { get; set; }

    public Laptop(string name, string operatingSystem, int ramGb) : base(name)
    {
        OperatingSystem = operatingSystem;
        RamGb = ramGb;
    }

    public override string GetTypeName()
    {
        return "Laptop";
    }

    public override string GetDetails()
    {
        return "OS: " + OperatingSystem + ", RAM: " + RamGb + " GB";
    }
}