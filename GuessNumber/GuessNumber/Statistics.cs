using System.Collections.Generic;
using System.IO;

namespace GuessNumber
{
    public class Statistics
    {
        public int Games { get; private set; }
        public int Wins { get; private set; }
        public int MinAttempts { get; private set; }
        public int MaxAttempts { get; private set; }
        public int TotalAttempts { get; private set; }

        public Dictionary<string, int> GamesByLevel { get; } = new Dictionary<string, int>();

        public double Average
        {
            get { return Wins > 0 ? (double)TotalAttempts / Wins : 0; }
        }

        public void AddGame(GameResult result)
        {
            Games++;

            if (!GamesByLevel.ContainsKey(result.Difficulty))
            {
                GamesByLevel[result.Difficulty] = 0;
            }
            GamesByLevel[result.Difficulty]++;

            if (!result.IsWin)
                return;

            Wins++;
            TotalAttempts += result.Attempts;

            if (MinAttempts == 0 || result.Attempts < MinAttempts)
            {
                MinAttempts = result.Attempts;
            }
            if (result.Attempts > MaxAttempts)
            {
                MaxAttempts = result.Attempts;
            }
        }
    }

    public interface IStatisticsStorage
    {
        void Save(Statistics statistics);
    }

    public class FileStatisticsStorage : IStatisticsStorage
    {
        private readonly string _path;

        public FileStatisticsStorage(string path)
        {
            _path = path;
        }

        public void Save(Statistics statistics)
        {
            List<string> lines = new List<string>
            {
                $"Всего игр: {statistics.Games}",
                $"Побед: {statistics.Wins}",
                $"Минимум попыток: {statistics.MinAttempts}",
                $"Максимум попыток: {statistics.MaxAttempts}",
                $"Среднее количество попыток: {statistics.Average:F2}",
                "Сыграно по уровням сложности:"
            };

            foreach (var pair in statistics.GamesByLevel)
            {
                lines.Add("  " + pair.Key + ": " + pair.Value);
            }
            File.WriteAllLines(_path, lines);
        }
    }
}