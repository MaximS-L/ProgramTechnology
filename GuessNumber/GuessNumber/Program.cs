using System;

namespace GuessNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            ISecretGenerator generator = new RandomSecretGenerator();
            IStatisticsStorage storage = new FileStatisticsStorage("stats.txt");

            IOutput output = new CompositeOutput(
                new ConsoleOutput(),
                new FileOutput("game_log.txt"));

            GameUI ui = new GameUI(output);
            Statistics statistics = new Statistics();

            do
            {
                Settings settings = ui.ChooseSettings();

                Game game = new Game(generator, settings);
                ui.Subscribe(game);
                game.Finished += statistics.AddGame;

                game.Start();

                while (!game.IsFinished)
                {
                    int? number = ui.ReadNumber(game.Attempts + 1);

                    if (number.HasValue)
                        game.MakeGuess(number.Value);
                    else
                        game.SkipAttempt();
                }
            } while (ui.AskContinue());

            if (ui.AskSave())
            {
                storage.Save(statistics);
                ui.ShowMessage("Статистика успешно сохранена в файл stats.txt!");
            }
            else
            {
                ui.ShowMessage("Выход без сохранения.");
            }

            Console.WriteLine("Спасибо за игру! Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}