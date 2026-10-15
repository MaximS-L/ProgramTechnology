using System.Globalization;
namespace MedicalEquipment;

public class XRay : MedicalDevice, ISterilizable
{
    private int _sterilizationCount;
    public double RadiationLevel { get; set; }
    public int SterilizationCount => _sterilizationCount;
    public bool IsDangerous => RadiationLevel > 1.0;

    public XRay(string serial, string name, int power, double radiationLevel)
        : base(serial, name, power)
    {
        RadiationLevel = radiationLevel;
    }

    public XRay(string name) : base(name)
    {
        RadiationLevel = 0.1;
    }

    public override void Operate()
    {
        Console.WriteLine($"{Name} takes an X-ray image.");
    }

    public void Sterilize()
    {
        _sterilizationCount++;
        Console.WriteLine($"{Name} sterilized.");
    }

    public override string ToString()
    {
        string radiation = RadiationLevel.ToString("F1", CultureInfo.InvariantCulture);
        return $"[XRay] {Name}, radiation={radiation}, class: {Class}";
    }
}