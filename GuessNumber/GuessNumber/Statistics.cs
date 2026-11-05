namespace GuessGame
{
    public class Statistics
    {
        private int _min = 0;
        private int _max = 0;
        private int _total = 0;
        private int _games = 0;
        private int _wins = 0;

        public int Min => _min;
        public int Max => _max;
        public int Total => _total;
        public int Games => _games;
        public int Wins => _wins;

        public double Average
        {
            get
            {
                if (_wins > 0)
                    return (double)_total / _wins;

                return 0;
            }
        }

        public void AddResult(int count, bool isWin)
        {
            _games++;

            if (isWin)
            {
                _wins++;

                if (_min == 0 || _min > count)
                    _min = count;

                if (_max < count)
                    _max = count;

                _total += count;
            }
        }
    }
}