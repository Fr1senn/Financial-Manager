using System.Globalization;
using financial_manager.Models;
using financial_manager.Utilities.Interfaces;

namespace financial_manager.Utilities;

public class MonthProvider : IMonthProvider
{
    public Dictionary<string, TransactionSummary> GetMonthlyTransactionsSummary()
    {
        return Enumerable.Range(1, 12)
            .ToDictionary(
                month => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                month => new TransactionSummary
                {
                    Income = 0,
                    Expense = 0
                }
            );
    }
}