using System.Security.Cryptography.X509Certificates;

namespace bank;

public class InterestEarningAcoount: BankAccount
{
    public  InterestEarningAcoount(string name,  decimal initialBalance)
        : base(name, initialBalance)
    
    { }

    public override void PerformMonthAndTransactions()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "Apply month interest");
        }
    }
}
