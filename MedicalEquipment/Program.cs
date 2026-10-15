namespace MedicalEquipment;
public class Program
{
    public static void Main(string[] args)
    {
        List<MedicalDevice> devices = new List<MedicalDevice>
        {
            new MRI("SN-001", "Siemens", 1500, 3.0),
            new XRay("Philips"),
            new Defibrillator("SN-003", "Zoll", 50, 360)
        };

        foreach (MedicalDevice device in devices)
        {
            Console.WriteLine(device);
            Console.WriteLine($"  power hungry: {device.IsPowerHungry}");

            device.Operate();

            if (device is ISterilizable sterilizable)
            {
                sterilizable.Sterilize();
            }
            else
            {
                Console.WriteLine($"{device.Name} cannot be sterilized.");
            }

            Console.WriteLine(new string('-', 40));
        }
    }
}