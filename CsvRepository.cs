using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace KinoStudia
{
    /// <summary>
    /// Репозиторий для считывания данных из CSV-файлов.
    /// </summary>
    public class CsvRepository
    {
        private string _basePath;

        /// <summary>
        /// Конструктор репозитория. Принимает путь к папке с файлами.
        /// </summary>
        public CsvRepository(string basePath)
        {
            _basePath = basePath;
        }

        /// <summary>
        /// Считывает киностудии из файла studios.csv
        /// </summary>
        public List<Studio> GetStudios()
        {
            List<Studio> result = new List<Studio>();
            string path = Path.Combine(_basePath, "studios.csv");

            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);

            if (lines.Length < 2)
            {
                Console.WriteLine($"Файл {path} пуст или содержит только заголовок.");
                return result;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                string[] parts = lines[i].Split(',');
                if (parts.Length < 3) continue;

                int id = int.Parse(parts[0], CultureInfo.InvariantCulture);
                string name = parts[1];
                string country = parts[2];

                Studio studio = new Studio(id, name, country);
                result.Add(studio);
            }
            return result;
        }

        /// <summary>
        /// Считывает режиссёров из файла directors.csv
        /// </summary>
        public List<Director> GetDirectors()
        {
            List<Director> result = new List<Director>();
            string path = Path.Combine(_basePath, "directors.csv");

            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);

            if (lines.Length < 2)
            {
                Console.WriteLine($"Файл {path} пуст или содержит только заголовок.");
                return result;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                string[] parts = lines[i].Split(',');
                if (parts.Length < 4) continue;

                int id = int.Parse(parts[0], CultureInfo.InvariantCulture);
                string fullName = parts[1];
                int experience = int.Parse(parts[2], CultureInfo.InvariantCulture);
                int awards = int.Parse(parts[3], CultureInfo.InvariantCulture);

                Director director = new Director(id, fullName, experience, awards);
                result.Add(director);
            }
            return result;
        }

        /// <summary>
        /// Считывает фильмы из файла films.csv
        /// </summary>
        public List<Film> GetFilms()
        {
            List<Film> result = new List<Film>();
            string path = Path.Combine(_basePath, "films.csv");

            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);

            if (lines.Length < 2)
            {
                Console.WriteLine($"Файл {path} пуст или содержит только заголовок.");
                return result;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                string[] parts = lines[i].Split(',');
                if (parts.Length < 6) continue;

                int id = int.Parse(parts[0], CultureInfo.InvariantCulture);
                string title = parts[1];
                int studioId = int.Parse(parts[2], CultureInfo.InvariantCulture);
                int directorId = int.Parse(parts[3], CultureInfo.InvariantCulture);
                int year = int.Parse(parts[4], CultureInfo.InvariantCulture);
                decimal budget = decimal.Parse(parts[5], CultureInfo.InvariantCulture);

                Film film = new Film(id, title, studioId, directorId, year, budget);
                result.Add(film);
            }
            return result;
        }
    }
}
