using System;

namespace KinoStudia
{
    /// <summary>
    /// Представляет кинофильм.
    /// </summary>
    public class Film
    {
        private int _year;
        private decimal _budget;

        /// <summary>
        /// Уникальный идентификатор фильма (Первичный ключ).
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Название фильма.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Внешний ключ: идентификатор киностудии.
        /// </summary>
        public int StudioId { get; private set; }

        /// <summary>
        /// Внешний ключ: идентификатор режиссёра.
        /// </summary>
        public int DirectorId { get; private set; }

        /// <summary>
        /// Год выпуска фильма.
        /// </summary>
        public int Year
        {
            get => _year;
            private set => _year = ValidateYear(value);
        }

        /// <summary>
        /// Бюджет фильма. Не может быть отрицательным.
        /// </summary>
        public decimal Budget
        {
            get => _budget;
            private set => _budget = ValidateBudget(value);
        }

        /// <summary>
        /// Признак дорогого фильма: бюджет больше 100 000 000 руб.
        /// </summary>
        public bool IsExpensive => Budget > 100000000m;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Film()
        {
        }

        /// <summary>
        /// Конструктор с полным набором параметров и проверкой значений.
        /// </summary>
        public Film(int id, string title, int studioId, int directorId, int year, decimal budget)
        {
            Id = ValidateId(id, nameof(Id));
            Title = ValidateTitle(title);
            StudioId = ValidateId(studioId, nameof(StudioId));
            DirectorId = ValidateId(directorId, nameof(DirectorId));
            Year = year;
            Budget = budget;
        }

        /// <summary>
        /// Возвращает краткую информацию о фильме.
        /// </summary>
        public string GetInfo()
        {
            return $"{Title} ({Year}, {Budget.ToString("0")} руб.)";
        }

        private static int ValidateId(int value, string paramName)
        {
            if (value < 0)
            {
                throw new ArgumentException($"Идентификатор {paramName} не может быть отрицательным.", paramName);
            }
            return value;
        }

        private static string ValidateTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Название фильма не может быть пустым.", nameof(Title));
            }
            return value;
        }

        private static int ValidateYear(int value)
        {
            if (value < 1888 || value > DateTime.Now.Year + 5)
            {
                throw new ArgumentException("Некорректный год выпуска фильма.", nameof(Year));
            }
            return value;
        }

        private static decimal ValidateBudget(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Бюджет не может быть отрицательным.", nameof(Budget));
            }
            return value;
        }
    }
}
