namespace apbd_eng_15_task3.Domain.Equipment;

public class Camera : Equipment
{
    public int MegaPixels { get; set; }
    public bool HasStabilization { get; set; }

    public Camera(string name, int megaPixels, bool hasStabilization) : base(name)
    {
        MegaPixels = megaPixels;
        HasStabilization = hasStabilization;
    }

    public override string GetTypeName()
    {
        return "Camera";
    }

    public override string GetDetails()
    {
        string stabilization;
        if (HasStabilization)
        {
            stabilization = "Yes";
        }
        else
        {
            stabilization = "No";
        }

        return "MegaPixels: " + MegaPixels + ", Stabilization: " + stabilization;
    }
}