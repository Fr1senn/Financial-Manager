using System.Globalization;
using financial_manager.Entities;

namespace financial_manager.Utilities;

public static class MonthProvider
{
    public static Dictionary<string, TransactionSummary> GetMonthlyTransactionsSummary()
    {
        return Enumerable
            .Range(1, 12)
            .ToDictionary(
                month => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                month => new TransactionSummary { Income = 0, Expense = 0 }
            );
    }
}
