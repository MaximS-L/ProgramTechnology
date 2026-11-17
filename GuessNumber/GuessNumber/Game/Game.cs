using System;

namespace GuessNumber;

public enum Hint
{
    Higher,
    Lower
}

public class Game
{
    private readonly ISecretGenerator _generator;
    private readonly Settings _settings;
    private int _secret;
    public int Attempts { get; private set; }
    public bool IsFinished { get; private set; }

    public event Action<Settings> Started;
    public event Action<Hint> HintGiven;
    public event Action InvalidInput;
    public event Action<GameResult> Won;
    public event Action<GameResult> Lost;
    public event Action<GameResult> Finished;

    public Game(ISecretGenerator generator, Settings settings)
    {
        _generator = generator;
        _settings = settings;
    }

    public void Start()
    {
        _secret = _generator.Generate(1, _settings.MaxNumber);
        Attempts = 0;
        IsFinished = false;

        Started?.Invoke(_settings);
    }

    public void MakeGuess(int number)
    {
        Attempts++;

        if (number == _secret)
        {
            Finish(true);
            return;
        }

        if (number > _secret)
        {
            HintGiven?.Invoke(Hint.Lower);
        }
        else
        {
            HintGiven?.Invoke(Hint.Higher);
        }
        CheckAttemptsLeft();
    }

    public void SkipAttempt()
    {
        Attempts++;
        InvalidInput?.Invoke();
        CheckAttemptsLeft();
    }

    private void CheckAttemptsLeft()
    {
        if (Attempts >= _settings.MaxAttempts)
        {
            Finish(false);
        }
    }

    private void Finish(bool isWin)
    {
        IsFinished = true;

        GameResult result = new GameResult(
            _settings.Name, Attempts, _settings.MaxAttempts, _secret, isWin);

        if (isWin)
        {
            Won?.Invoke(result);
        }
        else
        {
            Lost?.Invoke(result);
        }
        Finished?.Invoke(result);
    }
}
