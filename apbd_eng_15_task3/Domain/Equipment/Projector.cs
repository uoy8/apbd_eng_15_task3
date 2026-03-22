namespace apbd_eng_15_task3.Domain.Equipment;

public class Projector : Equipment
{
    public int ResolutionWidth { get; set; }
    public int ResolutionHeight { get; set; }

    public Projector(string name, int resolutionWidth, int resolutionHeight) : base(name)
    {
        ResolutionWidth = resolutionWidth;
        ResolutionHeight = resolutionHeight;
    }

    public override string GetTypeName()
    {
        return "Projector";
    }

    public override string GetDetails()
    {
        return "Resolution: " + ResolutionWidth + "x" + ResolutionHeight;
    }
}