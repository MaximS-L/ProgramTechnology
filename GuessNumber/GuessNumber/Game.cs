using System;

namespace GuessNumber
{
    public interface ISecretGenerator
    {
        int Generate(int min, int max);
    }

    public class RandomSecretGenerator : ISecretGenerator
    {
        private readonly Random _random = new Random();

        public int Generate(int min, int max)
        {
            return _random.Next(min, max + 1);
        }
    }
