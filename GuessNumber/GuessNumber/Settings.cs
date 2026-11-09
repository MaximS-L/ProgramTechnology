using System.Collections.Generic;

namespace GuessNumber
{
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

    public static class DifficultyLevels
    {
        private static readonly List<Settings> _levels = new List<Settings>
        {
            new Settings("Легкий (1-50, 10 попыток)", 50, 10),
            new Settings("Нормальный (1-100, 7 попыток)", 100, 7),
            new Settings("Сложный (1-100, 5 попыток)", 100, 5)
        };

        public static List<Settings> GetAll()
        {
            return new List<Settings>(_levels);
        }
    }
}