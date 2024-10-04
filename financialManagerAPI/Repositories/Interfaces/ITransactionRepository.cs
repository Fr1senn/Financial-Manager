using financial_manager.Entities.DTOs;
using financial_manager.Entities.Requests;

namespace financial_manager.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<(IEnumerable<TransactionDTO>, int)> GetTransactionsAsync(PageRequest request);
        Task DeleteTransactionAsync(int transactionId);
        Task CreateTransactionAsync(TransactionRequest request);
        Task UpdateTransactionAsync(TransactionRequest request);
    }
}
