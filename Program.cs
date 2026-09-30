using System;
using System.Collections.Generic;

namespace KinoStudia
{
    public class Program
    {
        /// <summary>
        /// Главный метод программы.
        /// </summary>
        public static void Main(string[] args)
        {

            List<Studio> studios = null;
            List<Director> directors = null;
            List<Film> films = null;

            Console.WriteLine("1 — InMemory, 2 — CSV");
            Console.Write("Ваш выбор: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Неверный выбор");
                return;
            }

            try
            {
                switch (choice)
                {
                    case 1:
                        InMemoryRepository memRepo = new InMemoryRepository();
                        studios = memRepo.GetStudios();
                        directors = memRepo.GetDirectors();
                        films = memRepo.GetFilms();
                        break;

                    case 2:
                        CsvRepository csvRepo = new CsvRepository("C:\\data_baza");
                        studios = csvRepo.GetStudios();
                        directors = csvRepo.GetDirectors();
                        films = csvRepo.GetFilms();
                        break;

                    default:
                        throw new ArgumentException("Неверный выбор");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении данных: {ex.Message}");
                return;
            }

            Console.WriteLine();

            Director? foundDirector = FindDirectorByFilmTitle(films, directors, "Начало");
            Console.WriteLine("1. FindDirector(\"Начало\"): " +
                (foundDirector != null ? foundDirector.GetInfo() : "null"));

            Studio? foundStudio = FindStudioByFilmTitle(films, studios, "Начало");
            Console.WriteLine("2. FindStudio(film \"Начало\"): " +
                (foundStudio != null ? foundStudio.GetInfo() : "null"));

            decimal totalBudget = GetTotalBudget(films);
            Console.WriteLine($"3. GetTotalBudget: {totalBudget.ToString("0")} руб.");

            Dictionary<string, Film> maxBudgetPerStudio = GetMaxBudgetFilmPerStudio(films, studios);
            Console.Write("4. GetMaxBudgetFilmPerStudio: ");
            PrintMaxBudgetPerStudio(maxBudgetPerStudio, studios);

            Console.WriteLine("5. PrintAllFilms:");
            PrintAllFilms(films, directors, studios);

            Console.WriteLine();
            Director? notFound = FindDirectorByFilmTitle(films, directors, "Неизвестный фильм");
            Console.WriteLine("Не найдено: FindDirector(\"Неизвестный фильм\") -> " +
                (notFound != null ? notFound.GetInfo() : "null"));

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }


        /// <summary>
        /// Загружает согласованные данные из InMemoryRepository.
        /// </summary>
        private static (List<Studio>, List<Director>, List<Film>) LoadFromInMemory()
        {
            InMemoryRepository repository = new InMemoryRepository();
            return (repository.GetStudios(), repository.GetDirectors(), repository.GetFilms());
        }

        /// <summary>
        /// Загружает и парсит данные из CsvRepository из указанной папки базы данных.
        /// </summary>
        private static (List<Studio>, List<Director>, List<Film>) LoadFromCsv()
        {
            CsvRepository repository = new CsvRepository("C:\\data_baza");
            return (repository.GetStudios(), repository.GetDirectors(), repository.GetFilms());
        }
        /// <summary>
        /// Находит режиссёра, снявшего фильм с заданным названием.
        /// </summary>
        public static Director? FindDirectorByFilmTitle(List<Film> films, List<Director> directors, string filmTitle)
        {
            if (films == null || directors == null) return null;

            Film? foundFilm = null;
            for (int i = 0; i < films.Count; i++)
            {
                if (films[i].Title == filmTitle)
                {
                    foundFilm = films[i];
                    break;
                }
            }

            if (foundFilm == null) return null;

            for (int i = 0; i < directors.Count; i++)
            {
                if (directors[i].Id == foundFilm.DirectorId)
                {
                    return directors[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Находит киностудию, выпустившую фильм с заданным названием.
        /// </summary>
        public static Studio? FindStudioByFilmTitle(List<Film> films, List<Studio> studios, string filmTitle)
        {
            if (films == null || studios == null) return null;

            Film? foundFilm = null;
            for (int i = 0; i < films.Count; i++)
            {
                if (films[i].Title == filmTitle)
                {
                    foundFilm = films[i];
                    break;
                }
            }

            if (foundFilm == null) return null;

            for (int i = 0; i < studios.Count; i++)
            {
                if (studios[i].Id == foundFilm.StudioId)
                {
                    return studios[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Вычисляет суммарный финансовый бюджет абсолютно всех фильмов, загруженных в систему.
        /// </summary>
        public static decimal GetTotalBudget(List<Film> films)
        {
            if (films == null) return 0;

            decimal totalBudget = 0;
            for (int i = 0; i < films.Count; i++)
            {
                totalBudget += films[i].Budget;
            }
            return totalBudget;
        }

        /// <summary>
        /// Находит по одному фильм с самым максимальным бюджетом для каждой киностудии.
        /// </summary>
        public static Dictionary<string, Film> GetMaxBudgetFilmPerStudio(List<Film> films, List<Studio> studios)
        {
            Dictionary<string, Film> result = new Dictionary<string, Film>();

            if (films == null || studios == null) return result;

            for (int i = 0; i < studios.Count; i++)
            {
                Studio studio = studios[i];
                Film? heaviest = null;

                for (int j = 0; j < films.Count; j++)
                {
                    if (films[j].StudioId == studio.Id)
                    {
                        if (heaviest == null || films[j].Budget > heaviest.Budget)
                        {
                            heaviest = films[j];
                        }
                    }
                }

                if (heaviest != null)
                {
                    string resultKey = $"Студия {studio.Name}";
                    result[resultKey] = heaviest;
                }
            }

            return result;
        }

        /// <summary>
        /// Выводит результаты группировки GetMaxBudgetFilmPerStudio на экран консоли в одну строку через запятую.
        /// </summary>>
        private static void PrintMaxBudgetPerStudio(Dictionary<string, Film> maxBudgetPerStudio, List<Studio> studios)
        {
            if (maxBudgetPerStudio == null || studios == null) return;

            List<string> parts = new List<string>();

            for (int i = 0; i < studios.Count; i++)
            {
                string resultKey = $"Студия {studios[i].Name}";
                if (maxBudgetPerStudio.ContainsKey(resultKey))
                {
                    Film film = maxBudgetPerStudio[resultKey];
                    parts.Add($"{studios[i].Name} — {film.Title} ({film.Budget.ToString("0")})");
                }
            }

            for (int i = 0; i < parts.Count; i++)
            {
                Console.Write(parts[i]);
                if (i < parts.Count - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Печатает сводный информационный отчёт о каждом фильме базы данных, автоматически подтягивая ФИО режиссёра и название студии.
        /// </summary>
        public static void PrintAllFilms(List<Film> films, List<Director> directors, List<Studio> studios)
        {
            if (films == null || directors == null || studios == null) return;

            for (int i = 0; i < films.Count; i++)
            {
                Film film = films[i];

                Director? director = null;
                for (int j = 0; j < directors.Count; j++)
                {
                    if (directors[j].Id == film.DirectorId)
                    {
                        director = directors[j];
                        break;
                    }
                }

                Studio? studio = null;
                for (int j = 0; j < studios.Count; j++)
                {
                    if (studios[j].Id == film.StudioId)
                    {
                        studio = studios[j];
                        break;
                    }
                }

                string directorName = director != null ? director.FullName : "—";
                string studioName = studio != null ? studio.Name : "—";

                Console.WriteLine($"{film.GetInfo()} — режиссёр {directorName}, студия {studioName}");
            }
        }
    }
}
