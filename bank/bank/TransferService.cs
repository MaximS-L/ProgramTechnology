using System;

namespace bank;

/// <summary>
/// Выполняет переводы денег между банковскими счетами.
/// </summary>
public static class TransferService
{
    /// <summary>
    /// Переводит сумму со счёта-источника на счёт-получатель, а при ошибке зачисления возвращает списанную сумму обратно.
    /// </summary>
    public static void Transfer(BankAccount from, BankAccount to, decimal amount, DateTime date)
    {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        if (ReferenceEquals(from, to))
        {
            throw new InvalidOperationException("Нельзя переводить деньги на тот же счёт.");
        }

        // Общий идентификатор, по которому можно найти обе половины перевода в истории
        string id = Guid.NewGuid().ToString("N")[..8];

        // Если денег не хватает, исключение вылетит здесь, до любых изменений
        from.MakeWithdrawal(amount, date, $"Transfer to {to.Number} #{id}");

        try
        {
            to.MakeDeposit(amount, date, $"Transfer from {from.Number} #{id}");
        }
        catch
        {
            // Списание уже прошло, а зачисление упало: возвращаем деньги новой операцией
            from.MakeDeposit(amount, date, $"Rollback transfer #{id}");
            throw;
        }
    }
}