using System.Collections.Generic;

namespace GuessNumber
{
    public class Statistics
    {
        public int Games { get; private set; }
        public int Wins { get; private set; }
        public int MinAttempts { get; private set; }
        public int MaxAttempts { get; private set; }
        public int TotalAttempts { get; private set; }

        public Dictionary<string, int> GamesByLevel { get; } = new Dictionary<string, int>();

        public double Average
        {
            get { return Wins > 0 ? (double)TotalAttempts / Wins : 0; }
        }

        public void AddGame(GameResult result)
        {
            Games++;

            if (!GamesByLevel.ContainsKey(result.Difficulty))
            {
                GamesByLevel[result.Difficulty] = 0;
            }
            GamesByLevel[result.Difficulty]++;

            if (!result.IsWin)
                return;

            Wins++;
            TotalAttempts += result.Attempts;

            if (MinAttempts == 0 || result.Attempts < MinAttempts)
            {
                MinAttempts = result.Attempts;
            }
            if (result.Attempts > MaxAttempts)
            {
                MaxAttempts = result.Attempts;
            }
        }
    }
}
