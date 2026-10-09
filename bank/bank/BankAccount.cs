using System;
using System.Collections.Generic;
using System.Text;

namespace bank;

/// <summary>
/// Представляет базовый класс банковского счёта с поддержкой операций пополнения, снятия и ведения истории.
/// </summary>
public class BankAccount
{
    /// <summary>
    /// Минимально допустимый баланс для данного счёта.
    /// </summary>
    private readonly decimal _minimumBalance;

    /// <summary>
    /// Начальное значение для генерации уникальных номеров счетов.
    /// </summary>
    static private int s_accountNumberSeed = 1000000000;

    /// <summary>
    /// Уникальный текстовый номер банковского счёта.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Имя и фамилия владельца банковского счёта.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    /// Текущий баланс счёта, автоматически вычисляемый как сумма всех совершённых транзакций.
    /// </summary>
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var transaction in _allTransactions)
            {
                balance += transaction.Amount;
            }
            return balance;
        }
    }

    /// <summary>
    /// Внутренний список, хранящий всю историю транзакций по данному счёту.
    /// </summary>
    private List<Transaction> _allTransactions = new List<Transaction>();

    /// <summary>
    /// Инициализирует обычный банковский счёт с указанием владельца и начального баланса. Минимальный лимит баланса устанавливается в 0.
    /// </summary>
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0)
    {
    }

    /// <summary>
    /// Инициализирует банковский счёт с указанием владельца, начального баланса и пользовательского лимита минимального баланса.
    /// </summary>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Owner = name;
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
        _minimumBalance = minimumBalance;

        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.UtcNow, "initial balance");
    }

    /// <summary>
    /// Зачисляет на счёт указанную сумму на определённую дату с текстовым комментарием. Сумма должна быть строго положительной.
    /// </summary>
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }

        var deposite = new Transaction(amount, date, note);
        _allTransactions.Add(deposite);
    }

    /// <summary>
    /// Списывает со счёта указанную сумму на определённую дату с комментарием, а также автоматически применяет овердрафт при нехватке лимита.
    /// </summary>
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);

        _allTransactions.Add(withdrawal);

        if (overdraftTransaction is not null)
            _allTransactions.Add(overdraftTransaction);
    }

    /// <summary>
    /// Проверяет, уходит ли счёт в минус ниже лимита. Возвращает транзакцию штрафа для кредитного счёта или генерирует ошибку для обычного.
    /// </summary>
    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("Not sufficient rubls for this widthdrawal");
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// Формирует и возвращает форматированную текстовую строку со всей историей изменений баланса и транзакций по счёту.
    /// </summary>
    public string GetAccountHistory()
    {
        var report = new StringBuilder();

        decimal balance = 0;
        report.AppendLine("Data\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}");
        }
        return report.ToString();
    }

    /// <summary>
    /// Выполняет регламентные расчёты и транзакции в конце месяца. Переопределяется в классах-наследниках.
    /// </summary>
    public virtual void PerformMonthEndTransactions()
    {
    }

    /// <summary>
    /// Возвращает текстовое представление счёта, включающее его тип, имя владельца, номер и текущий баланс.
    /// </summary>
    public override string ToString()
    {
        return $"Type: {GetType().Name}\tOwner: {Owner}\tNumber of account: {Number}\tBalance: {Balance}";
    }
}
