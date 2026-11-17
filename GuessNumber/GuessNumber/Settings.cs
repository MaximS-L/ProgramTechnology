using System.Collections.Generic;

namespace GuessNumber;

public class Settings
{
    public string Name { get; }
    public int MaxNumber { get; }
    public int MaxAttempts { get; }

    public Settings(string name, int maxNumber, int maxAttempts)
    {
        Name = name;
        MaxNumber = maxNumber;
        MaxAttempts = maxAttempts;
    }
}

public interface IDifficultyProvider
{
    List<Settings> GetAll();
}
