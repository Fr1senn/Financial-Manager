using financial_manager.Models;
using System.Collections;

namespace financial_manager.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync(int packSize = 10, int pageNumber = 0);
        Task<int> GetUserTransactionQuantityAsync();
        Task<Dictionary<string, TransactionSummary>> GetMonthlyTransactionsAsync(int year);
        Task DeleteTransactionAsync(int transactionId);
        Task CreateTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
    }
}