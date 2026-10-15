namespace MedicalEquipment;
public abstract class MedicalDevice
{
    private readonly string _serial;
    public string Name { get; set; }
    public int PowerConsumption { get; set; }
    public DeviceClass Class { get; set; } = DeviceClass.Diagnostic;
    public string Serial => _serial;
    public bool IsPowerHungry => PowerConsumption > 1000;

    protected MedicalDevice(string serial, string name, int power)
    {
        _serial = serial;
        Name = name;
        PowerConsumption = power;
    }

    protected MedicalDevice(string name) : this("N/A", name, 0)
    {
    }

    public abstract void Operate();
}