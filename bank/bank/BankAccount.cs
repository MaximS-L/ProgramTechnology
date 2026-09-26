using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace bank;

internal class BankAccount
{
    private List<Transaction> _allTransactions = new List<Transaction>();
    public string Owner { get; private set; }
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
    public BankAccount(string name, decimal initialBalance)
    {
        
        Owner = name; //this.Owner = name;
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

        if (Balance < amount)
        {
            throw new InvalidOperationException("Not sofficient rubls for this withdrawal");
        }

        var withdrawal = new Transaction(-amount, date, note);
        _allTransactions.Add(withdrawal);
    }

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
}
