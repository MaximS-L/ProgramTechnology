using System.Globalization;
namespace MedicalEquipment;

public class MRI : MedicalDevice, ISterilizable
{
    private int _sterilizationCount;
    public double TeslaStrength { get; set; }
    public int SterilizationCount => _sterilizationCount;
    public bool IsPowerful => TeslaStrength >= 3.0;

    public MRI(string serial, string name, int power, double teslaStrength)
        : base(serial, name, power)
    {
        TeslaStrength = teslaStrength;
    }

    public MRI(string name) : base(name)
    {
        TeslaStrength = 1.5;
    }

    public override void Operate()
    {
        Console.WriteLine($"{Name} scans patient.");
    }

    public void Sterilize()
    {
        _sterilizationCount++;
        Console.WriteLine($"{Name} sterilized.");
    }

    public override string ToString()
    {
        string tesla = TeslaStrength.ToString("F1", CultureInfo.InvariantCulture);
        return $"[MRI] {Name}, tesla={tesla}, class: {Class}";
    }
}