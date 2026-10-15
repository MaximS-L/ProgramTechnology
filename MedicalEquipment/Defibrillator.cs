namespace MedicalEquipment;

public class Defibrillator : MedicalDevice
{
    private int _shockCount;
    public int MaxEnergy { get; set; }
    public int ShockCount => _shockCount;
    public bool IsPortable => MaxEnergy <= 360;

    public Defibrillator(string serial, string name, int power, int maxEnergy)
        : base(serial, name, power)
    {
        MaxEnergy = maxEnergy;
    }

    public Defibrillator(string name) : base(name)
    {
        MaxEnergy = 200;
    }

    public override void Operate()
    {
        _shockCount++;
        Console.WriteLine($"{Name} defibrillates.");
    }

    public override string ToString()
    {
        return $"[Defibrillator] {Name}, max energy={MaxEnergy} J, class: {Class}";
    }
}