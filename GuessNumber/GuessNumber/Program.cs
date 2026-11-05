using System;

namespace GuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            ISecret engine = new Engine();

            UI ui = new UI();
            Store store = new Store();
            Statistics statistics = new Statistics();

            bool play = true;

            do
            {
                Config cfg = ui.GetConfig();

                Match match = new Match(engine, cfg);

                ui.Bind(match);
                match.Run();

                int maxTries = cfg.Tries;
                bool done = false;

                for (int i = 1; i <= maxTries; i++)
                {
                    int num = ui.ReadNum(i);

                    done = match.Check(num);

                    if (done)
                        break;
                }

                statistics.AddResult(
                    match.GetCount(),
                    match.IsWin()
                );

                play = ui.Loop();

            } while (play);

            if (ui.AskSave())
            {
                store.Save(statistics);

                Console.WriteLine(
                    "Статистика успешно сохранена в файл stats.txt!");
            }
            else
            {
                Console.WriteLine("Выход без сохранения.");
            }

            Console.WriteLine(
                "Спасибо за игру! Нажмите любую клавишу для выхода...");

            Console.ReadKey();
        }
    }
}