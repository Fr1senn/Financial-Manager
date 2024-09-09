using financial_manager.Models;

namespace financial_manager.Utilities.Interfaces;

public interface IMonthProvider
{
    Dictionary<string, TransactionSummary> GetMonthlyTransactionsSummary();
}