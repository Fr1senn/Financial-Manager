using financial_manager.Models;

namespace financial_manager.Services.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<MonthlyTransactionData>> GetMonthlyTransactionsAsync(int year, int userId);
}