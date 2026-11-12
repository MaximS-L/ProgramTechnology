using System;

namespace GuessNumber;

public class GameUI
{
    private readonly IOutput _output;
    private readonly IDifficultyProvider _levels;                
    public GameUI(IOutput output, IDifficultyProvider levels)    
    {
        _output = output;
        _levels = levels;                                       
    }

    public void Subscribe(Game game)
    {
        game.Started += settings =>
            _output.WriteLine($"Уровень: {settings.Name}. Загадано число от 1 до {settings.MaxNumber}. У вас {settings.MaxAttempts} попыток.");

        game.HintGiven += hint =>
        {
            if (hint == Hint.Higher)
            {
                _output.WriteLine("Мое число больше твоего.");
            }
            else
            {
                _output.WriteLine("Мое число меньше твоего.");
            }
        };

        game.InvalidInput += () =>
            _output.WriteLine("Некорректный ввод. Попытка засчитана.");

        game.Won += result =>
        {
            _output.WriteLine($"Поздравляю! Вы угадали число за {result.Attempts} попыток.");

            if (result.Attempts == 1) _output.WriteLine("А вы неплох!");
            else if (result.Attempts <= result.MaxAttempts / 3) _output.WriteLine("Красавчик!");
            else if (result.Attempts <= result.MaxAttempts * 2 / 3) _output.WriteLine("Хорошо постарались!");
            else _output.WriteLine("Вы были на грани!");
        };

        game.Lost += result =>
            _output.WriteLine($"Попытки закончились. Было загадано число {result.Secret}.");
    }

    public Settings ChooseSettings()
    {
        var levels = _levels.GetAll();                          

        Console.WriteLine("Выберите уровень сложности:");
        for (int i = 0; i < levels.Count; i++)
        {
            Console.WriteLine((i + 1) + " — " + levels[i].Name);
        }
        Console.WriteLine($"{levels.Count + 1} — Пользовательские настройки");
        Console.Write("Ваш выбор: ");

        int.TryParse(Console.ReadLine(), out int choice);

        if (choice >= 1 && choice <= levels.Count)
        {
            return levels[choice - 1];
        }
        if (choice == levels.Count + 1)
        {
            int max = ReadPositive("Введите максимальное число диапазона: ");
            int tries = ReadPositive("Введите количество попыток: ");
            return new Settings("Пользовательский", max, tries);
        }
        return levels[1]; 
    }

    public int? ReadNumber(int attemptNumber)
    {
        Console.Write($"Попытка {attemptNumber}. Введите число: ");

        if (int.TryParse(Console.ReadLine(), out int number))
        {
            return number;
        }
        return null;
    }

    public bool AskContinue()
    {
        Console.Write("Хотите продолжить? (yes/no): ");
        string answer = Console.ReadLine().Trim().ToLower();
        return answer == "yes" || answer == "y";
    }

    public bool AskSave()
    {
        Console.WriteLine("Записать данные в файл?");
        Console.WriteLine("1 — Да");
        Console.WriteLine("2 — Нет");
        Console.Write("Ваш выбор: ");
        return Console.ReadLine() == "1";
    }

    public void ShowMessage(string text)
    {
        _output.WriteLine(text);
    }

    private int ReadPositive(string prompt)
    {
        int value;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out value) || value < 1)
        {
            Console.Write("Нужно целое число больше 0. Повторите: ");
        }
        return value;
    }
}