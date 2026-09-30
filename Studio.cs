using System;

namespace KinoStudia
{
    /// <summary>
    /// Представляет киностудию.
    /// </summary>
    public class Studio
    {
        /// <summary>
        /// Уникальный идентификатор киностудии (Первичный ключ).
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Название киностудии.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Страна киностудии.
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Признак зарубежной студии: страна не "Россия".
        /// </summary>
        public bool IsForeign => Country != "Россия";

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Studio()
        {
        }

        /// <summary>
        /// Конструктор с полным набором параметров и проверкой значений.
        /// </summary>
        public Studio(int id, string name, string country)
        {
            Id = ValidateId(id);
            Name = ValidateName(name);
            Country = ValidateCountry(country);
        }

        /// <summary>
        /// Возвращает краткую информацию о студии.
        /// </summary>
        public string GetInfo()
        {
            return $"{Name} ({Country})";
        }

        private static int ValidateId(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Идентификатор не может быть отрицательным.", nameof(Id));
            }
            return value;
        }

        private static string ValidateName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Название не может быть пустым.", nameof(Name));
            }
            return value;
        }

        private static string ValidateCountry(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Страна не может быть пустой.", nameof(Country));
            }
            return value;
        }
    }
}
