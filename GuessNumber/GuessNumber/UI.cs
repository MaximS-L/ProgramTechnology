using System;

namespace GuessGame
{
    public class UI
    {
        public Config GetConfig()
        {
            Console.WriteLine("Выберите уровень сложности:");

            for (int i = 0; i < DifficultySettings.Levels.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1} — {DifficultySettings.Levels[i].Name}");
            }

            Console.WriteLine(
                $"{DifficultySettings.Levels.Count + 1} — Свои настройки");

            Console.Write("Ваш выбор: ");

            int.TryParse(Console.ReadLine(), out int choice);

            if (choice >= 1 && choice <= DifficultySettings.Levels.Count)
            {
                Difficulty level = DifficultySettings.Levels[choice - 1];

                return new Config(level.Max, level.Tries);
            }

            if (choice == DifficultySettings.Levels.Count + 1)
            {
                Console.Write("Введите максимальное число: ");
                int.TryParse(Console.ReadLine(), out int max);

                Console.Write("Введите количество попыток: ");
                int.TryParse(Console.ReadLine(), out int tries);

                return new Config(max, tries);
            }

            return new Config(100, 7);
        }

        public void Bind(Match match)
        {
            match.OnStart += (max, tries) =>
            {
                Console.WriteLine($"Загадано число от 1 до {max}.");
                Console.WriteLine($"У вас {tries} попыток.");
            };

            match.OnStep += (count, state) =>
            {
                if (state == State.Low)
                    Console.WriteLine("Мое число больше твоего.");
                else
                    Console.WriteLine("Мое число меньше твоего.");
            };

            match.OnWin += (count) =>
            {
                Console.WriteLine(
                    $"Поздравляю! Вы угадали число за {count} попыток.");
            };

            match.OnLose += (secret) =>
            {
                Console.WriteLine(
                    $"Попытки закончились. Было загадано число {secret}.");
            };
        }

        public int ReadNum(int step)
        {
            Console.Write($"Попытка {step}. Введите число: ");

            int.TryParse(Console.ReadLine(), out int num);

            return num;
        }

        public bool Loop()
        {
            Console.Write("Хотите продолжить? (yes/no): ");

            string ans = Console.ReadLine().Trim().ToLower();

            return ans == "yes" || ans == "y";
        }

        public bool AskSave()
        {
            Console.WriteLine("Записать данные в файл?");
            Console.WriteLine("1 — Да");
            Console.WriteLine("2 — Нет");
            Console.Write("Ваш выбор: ");

            return Console.ReadLine() == "1";
        }
    }
}