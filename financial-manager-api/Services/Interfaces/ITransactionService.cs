using financial_manager.Entities;

namespace financial_manager.Services.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<MonthlyTransactionData>> GetMonthlyTransactionsAsync(int year, int userId);
}
