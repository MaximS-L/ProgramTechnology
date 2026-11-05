using System;
using System.IO;

namespace GuessGame
{
    public class Store
    {
        public void Save(Statistics statistics)
        {
            string[] lines =
            {
                $"Всего игр: {statistics.Games}",
                $"Количество побед: {statistics.Wins}",
                $"Максимум попыток: {statistics.Max}",
                $"Минимум попыток: {statistics.Min}",
                $"Среднее количество попыток: {statistics.Average:F2}"
            };

            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "stats.txt"
            );

            File.WriteAllLines(path, lines);
        }
    }
}