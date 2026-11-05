using System;
using System.Collections.Generic;

namespace bank;

public class Program
{
    static void Main(string[] args)
    {
        BankAccount account1 = new BankAccount("Maxon", 230046);
        BankAccount account2 = new BankAccount("Egor", 12288);
        Console.WriteLine($"account: {account1.Owner} {account1.Balance} {account1.Number}");
        Console.WriteLine($"account: {account2.Owner} {account2.Balance} {account2.Number}");

        account1.MakeDeposit(1000m, DateTime.UtcNow, ":)");
        Console.WriteLine(account1.Balance);

        account1.MakeWithdrawal(100m, DateTime.UtcNow, ": ");
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

        InterestEarningAccount interest = new InterestEarningAccount("Maxim", 1000);
        interest.PerformMonthEndTransactions();

        Console.WriteLine(interest.GetAccountHistory());

        LineOfCreditAccount lineOfCredit = new LineOfCreditAccount("Maxim", 0, 1000m);
        lineOfCredit.MakeWithdrawal(500m, DateTime.UtcNow, "credit");

        GiftCardAccount giftcart = new GiftCardAccount("Maxim", 1000m, 5000m);

        List<BankAccount> accounts = new List<BankAccount>();
        accounts.Add(account1);
        accounts.Add(interest);
        accounts.Add(lineOfCredit);
        accounts.Add(giftcart);

        foreach (BankAccount account in accounts)
        {
            Console.WriteLine(account);
            account.PerformMonthEndTransactions();
            Console.WriteLine(account.GetAccountHistory());
        }

        Console.WriteLine("\n Credit Balance");
        LineOfCreditAccount credit = new("Max_Credit", 0m, 2000m);

        credit.MakeWithdrawal(1500m, DateTime.UtcNow, "Покупка 1");
        credit.MakeWithdrawal(1000m, DateTime.UtcNow, "Покупка 2");

        credit.PerformMonthEndTransactions();
        Console.WriteLine(credit.GetAccountHistory());
    }
}
