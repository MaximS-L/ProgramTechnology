using System;

namespace GuessGame
{
    public class Match
    {
        private readonly ISecret _engine;
        private readonly Config _cfg;

        private int _secret;
        private int _count;
        private bool _isWin;

        public event Action<int, int> OnStart;
        public event Action<int, State> OnStep;
        public event Action<int> OnWin;
        public event Action<int> OnLose;

        public Match(ISecret gameEngine, Config config)
        {
            this._engine = gameEngine;
            this._cfg = config;
        }

        public void Run()
        {
            _secret = _engine.Setup(1, _cfg.Max);
            _count = 0;
            _isWin = false;

            OnStart?.Invoke(_cfg.Max, _cfg.Tries);
        }

        public bool Check(int num)
        {
            _count++;

            if (num == _secret)
            {
                _isWin = true;
                OnWin?.Invoke(_count);
                return true;
            }

            State state = num < _secret ? State.Low : State.High;

            OnStep?.Invoke(_count, state);

            if (_count >= _cfg.Tries)
            {
                OnLose?.Invoke(_secret);
                return true;
            }

            return false;
        }

        public int GetCount()
        {
            return _count;
        }

        public bool IsWin()
        {
            return _isWin;
        }
    }
}