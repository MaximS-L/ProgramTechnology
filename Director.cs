using System;

namespace KinoStudia
{
    /// <summary>
    /// Представляет режиссёра фильма.
    /// </summary>
    public class Director
    {
        private int _experience;
        private int _awards;

        /// <summary>
        /// Уникальный идентификатор режиссёра (Первичный ключ).
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Полное имя режиссёра.
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// Стаж работы в годах. Не может быть отрицательным.
        /// </summary>
        public int Experience
        {
            get => _experience;
            private set => _experience = ValidateExperience(value);
        }

        /// <summary>
        /// Количество наград. Не может быть отрицательным.
        /// </summary>
        public int Awards
        {
            get => _awards;
            private set => _awards = ValidateAwards(value);
        }

        /// <summary>
        /// Признак опытного режиссёра: стаж больше 10 лет.
        /// </summary>
        public bool IsExperienced => Experience > 10;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Director()
        {
        }

        /// <summary>
        /// Конструктор с полным набором параметров и проверкой значений.
        /// </summary>
        public Director(int id, string fullName, int experience, int awards)
        {
            Id = ValidateId(id);
            FullName = ValidateFullName(fullName);
            Experience = experience;
            Awards = awards;
        }

        /// <summary>
        /// Возвращает краткую информацию о режиссёре с правильным склонением лет.
        /// </summary>
        public string GetInfo()
        {
            return $"{FullName} ({Experience} {GetYearWord(Experience)} опыта, {Awards} {GetAwardsWord(Awards)})";
        }

        private static int ValidateId(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Идентификатор не может быть отрицательным.", nameof(Id));
            }
            return value;
        }

        private static string ValidateFullName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Имя не может быть пустым.", nameof(FullName));
            }
            return value;
        }

        private static int ValidateExperience(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Стаж не может быть отрицательным.", nameof(Experience));
            }
            return value;
        }

        private static int ValidateAwards(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Количество наград не может быть отрицательным.", nameof(Awards));
            }
            return value;
        }

        private static string GetYearWord(int years)
        {
            int rem100 = years % 100;
            int rem10 = years % 10;

            if (rem100 >= 11 && rem100 <= 14) return "лет";
            if (rem10 == 1) return "год";
            if (rem10 >= 2 && rem10 <= 4) return "года";
            return "лет";
        }

        private static string GetAwardsWord(int awards)
        {
            int rem100 = awards % 100;
            int rem10 = awards % 10;

            if (rem100 >= 11 && rem100 <= 14) return "наград";
            if (rem10 == 1) return "награда";
            if (rem10 >= 2 && rem10 <= 4) return "награды";
            return "наград";
        }
    }
}
