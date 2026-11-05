using System.Collections.Generic;

namespace GuessGame
{
    public class Config
    {
        public int Max { get; set; }
        public int Tries { get; set; }

        public Config(int max, int tries)
        {
            Max = max;
            Tries = tries;
        }
    }

    public class Difficulty
    {
        public string Name { get; set; }
        public int Max { get; set; }
        public int Tries { get; set; }

        public Difficulty(string name, int max, int tries)
        {
            Name = name;
            Max = max;
            Tries = tries;
        }
    }

    public static class DifficultySettings
    {
        private static readonly List<Difficulty> _levels =
            new List<Difficulty>
            {
                new Difficulty("Легкий (1-50, 10 попыток)", 50, 10),
                new Difficulty("Нормальный (1-100, 7 попыток)", 100, 7),
                new Difficulty("Сложный (1-100, 5 попыток)", 100, 5)
            };

        public static IReadOnlyList<Difficulty> Levels
        {
            get
            {
                return _levels.AsReadOnly();
            }
        }

        public static void AddLevel(Difficulty level)
        {
            _levels.Add(level);
        }
    }
}