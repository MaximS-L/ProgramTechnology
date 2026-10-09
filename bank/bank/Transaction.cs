namespace bank;

/// <summary>
/// Представляет неизменяемую запись о банковской операции, хранящую сумму, дату проведения и комментарий.
/// </summary>
public record Transaction(decimal Amount, DateTime Date, string Note);

//internal record Transaction
//{

//    public decimal Amount { get; }
//    public decimal Date { get; }
//    public decimal Note { get; }
//    public Transaction(decimal Amount, decimal Date, decimal Note)
//    {
//        this.Note = Note;
//        this.Amount = Amount;
//        this.Date = Date;
//    }

//}

