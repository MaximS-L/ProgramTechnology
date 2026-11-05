using System;

namespace GuessGame
{
    public interface ISecret
    {
        int Setup(int min, int max);
    }

    public class Engine : ISecret
    {
        private readonly Random _rnd = new Random();

        public int Setup(int min, int max)
        {
            return _rnd.Next(min, max + 1);
        }
    }

    public class FixedSecret : ISecret
    {
        private readonly int _number;

        public FixedSecret(int number)
        {
            _number = number;
        }

        public int Setup(int min, int max)
        {
            if (min > max)
                throw new ArgumentException("Неверный диапазон.");

            if (_number < min)
                return min;

            if (_number > max)
                return max;

            return _number;
        }
    }
}