using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace bank;

// BankAccount - потомок класса object => 
public class BankAccount
{
    private List<Transaction> _allTransactions = new List<Transaction>();
    public string Owner { get; private set; }

    // Поле для хранения лимита (для обычного счета это 0)
    private readonly decimal _minimumBalance;
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
    public string Number {  get; }
    private static int s_accountNumberSeed = 1000000000;

    // Старый конструктор для обычных счетов (минимум равен 0)
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0)
    {
    }
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        
        Owner = name; //this.Owner = name;
        _minimumBalance = minimumBalance;
        MakeDeposit(initialBalance, DateTime.UtcNow, "initial balance");
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
    }
    public void MakeDeposit(decimal amount, DateTime date, string note) 
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException
                (nameof(amount), "Amount of deposit must be positive");
        }

        var deposite =  new Transaction(amount, date, note);
        _allTransactions.Add(deposite);

    }
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException
                (nameof(amount), "Amount of withdrawal must be positive");
        }
        // Вычисляем, выходим ли мы за рамки установленного лимита счёта
        bool isOverdrawn = Balance - amount < _minimumBalance;

        // Вызываем виртуальный метод проверки.
        var overdraftTransaction = CheckWithdrawalLimit(isOverdrawn);

        // Если за рамки вышли, а комиссии нет 
        if (isOverdrawn && overdraftTransaction == null)
        {
            throw new InvalidOperationException("Not sofficient rubls for this withdrawal");
        }

        var withdrawal = new Transaction(-amount, date, note);
        _allTransactions.Add(withdrawal);

        // Если есть комиссия за овердрафт — добавляем её в историю
        if (overdraftTransaction != null)
        {
            _allTransactions.Add(overdraftTransaction);
        }
    }

    // Метод, который переопределяет кредитный счет для начисления 20 единиц комиссии
    private protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn) => default;
    



    public string GetAccountHistory()
    {
        var report = new StringBuilder();

        decimal balance = 0;
        report.AppendLine("Data\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"" +
                $"{item.Date.ToShortDateString()}\t" +
                $"{item.Amount}\t{balance}\t{item.Note}");
        }
        return report.ToString();
    }


    //Ключевое слово virtual позволяет в дочернем классе
    // предоставить другую реализацию
    // метода PerformMonthAndTransactions
    public virtual void PerformMonthAndTransactions()
    {

    }

    public override string ToString()
    {
        return $"Type: {GetType().Name}\t" + $"Owner: { Owner}\t" + $"Number of account: { Number}\t" + $"Balance: { Balance}";
    }

  
}

