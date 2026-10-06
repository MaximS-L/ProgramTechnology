using System;

namespace bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount("Maxon", 230046);
            BankAccount account2 = new BankAccount("Egor", 12288);
            Console.WriteLine($"account: {account1.Owner} {account1.Balance} {account1.Number}");
            Console.WriteLine($"account: {account2.Owner} {account2.Balance} {account2.Number}");

            account1.MakeDeposit(1000m, DateTime.UtcNow, ":)");
            Console.WriteLine(account1.Balance);

            account1.MakeWithdrawal(100m, DateTime.UtcNow, ": )");
            Console.WriteLine(account1.Balance);

            Console.WriteLine(account1.GetAccountHistory());

            try
            {
                account2.MakeWithdrawal(10000000, DateTime.UtcNow, "; )");
                Console.WriteLine(account2.Balance);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            InterestEarningAcoount interestEarning = new("Maxim", 1000m);
            interestEarning.MakeDeposit(1000m, DateTime.UtcNow, ";)");

            interestEarning.MakeWithdrawal(10m, DateTime.UtcNow, ";(");
            interestEarning.PerformMonthAndTransactions();

            Console.WriteLine(interestEarning);
            Console.WriteLine(interestEarning.GetAccountHistory());

            Console.WriteLine("\n Credit Balance");
            LineOfCreditAccount credit = new("Max_Credit", 0m, 2000m);

            credit.MakeWithdrawal(1500m, DateTime.UtcNow, "Покупка 1"); 
            credit.MakeWithdrawal(1000m, DateTime.UtcNow, "Покупка 2"); 
            credit.PerformMonthAndTransactions();                       
            Console.WriteLine(credit.GetAccountHistory());
        }
    }
}
