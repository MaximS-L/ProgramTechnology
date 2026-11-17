using System.Collections.Generic;
using System.IO;

namespace GuessNumber
{
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
