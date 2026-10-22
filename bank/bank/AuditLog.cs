using System;
using System.Collections.Generic;
using System.Text;

namespace bank;

/// <summary>
/// Тип операции, записываемой в журнал аудита.
/// </summary>
public enum AuditOperationType
{
    /// <summary>
    /// Обычное пополнение счёта.
    /// </summary>
    Deposit,

    /// <summary>
    /// Обычное списание со счёта.
    /// </summary>
    Withdrawal,

    /// <summary>
    /// Начисление или списание процентов.
    /// </summary>
    Interest
}

/// <summary>
/// Неизменяемая запись журнала аудита: номер счёта, тип операции, сумма и дата.
/// </summary>
public record AuditEntry(string AccountNumber, AuditOperationType Type, decimal Amount, DateTime Date);

/// <summary>
/// Отдельный журнал аудита, в который записываются все операции по счетам.
/// </summary>
public static class AuditLog
{
    /// <summary>
    /// Внутренний список всех записей журнала.
    /// </summary>
    private static readonly List<AuditEntry> _entries = new();

    /// <summary>
    /// Все записи журнала, доступные только для чтения.
    /// </summary>
    public static IReadOnlyList<AuditEntry> Entries => _entries;

    /// <summary>
    /// Добавляет в журнал запись об операции.
    /// </summary>
    public static void Record(string accountNumber, AuditOperationType type, decimal amount, DateTime date)
    {
        _entries.Add(new AuditEntry(accountNumber, type, amount, date));
    }

    /// <summary>
    /// Формирует текстовый отчёт по всем записям журнала.
    /// </summary>
    public static string GetReport()
    {
        var report = new StringBuilder();
        report.AppendLine("Account\t\tType\t\tAmount\tDate");
        foreach (var e in _entries)
        {
            report.AppendLine($"{e.AccountNumber}\t{e.Type}\t{e.Amount}\t{e.Date.ToShortDateString()}");
        }
        return report.ToString();
    }
}