namespace GuessNumber
{
    public class GameResult
    {
        public string Difficulty { get; }
        public int Attempts { get; }
        public int MaxAttempts { get; }
        public int Secret { get; }
        public bool IsWin { get; }

        public GameResult(string difficulty, int attempts, int maxAttempts, int secret, bool isWin)
        {
            Difficulty = difficulty;
            Attempts = attempts;
            MaxAttempts = maxAttempts;
            Secret = secret;
            IsWin = isWin;
        }
    }
}
