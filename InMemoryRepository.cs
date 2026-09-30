using System.Collections.Generic;

namespace KinoStudia
{
    /// <summary>
    /// Репозиторий для работы с тестовыми данными в оперативной памяти.
    /// </summary>
    public class InMemoryRepository
    {
        private List<Studio> _studios;
        private List<Director> _directors;
        private List<Film> _films;

        /// <summary>
        /// Конструктор репозитория. Наполняет списки начальными согласованными данными.
        /// </summary>
        public InMemoryRepository()
        {
            _studios = new List<Studio>
            {
                new Studio(1, "Warner Bros", "США"),
                new Studio(2, "Paramount", "США"),
                new Studio(3, "Мосфильм", "Россия"),
                new Studio(4, "Studio Ghibli", "Япония"),
                new Studio(5, "Ленфильм", "Россия")
            };

            _directors = new List<Director>
            {
                new Director(1, "К. Нолан", 20, 5),
                new Director(2, "С. Спилберг", 45, 3),
                new Director(3, "Л. Гайдай", 40, 2),
                new Director(4, "Х. Миядзаки", 50, 1),
                new Director(5, "К. Тарантино", 30, 4)
            };

            _films = new List<Film>
            {
                new Film(1, "Начало", 1, 1, 2010, 160000000m),
                new Film(2, "Интерстеллар", 2, 1, 2014, 140000000m),
                new Film(3, "Бриллиантовая рука", 3, 3, 1968, 5000000m),
                new Film(4, "Тёмный рыцарь", 1, 1, 2008, 200000000m),
                new Film(5, "Джанго освобождённый", 2, 5, 2012, 100000000m)
            };
        }

        public List<Studio> GetStudios() => _studios;
        public List<Director> GetDirectors() => _directors;
        public List<Film> GetFilms() => _films;
    }
}
