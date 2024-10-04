using financial_manager.Entities;
using financial_manager.Entities.DTOs;
using financial_manager.Entities.Models;
﻿using financial_manager.Entities.DTOs;
using financial_manager.Entities.Requests;

namespace financial_manager.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionDTO>> GetTransactionsAsync(PageRequest request);
        Task<int> GetUserTransactionQuantityAsync();
        Task<Dictionary<string, TransactionSummary>> GetMonthlyTransactionsAsync(int year);
        Task<(IEnumerable<TransactionDTO>, int)> GetTransactionsAsync(PageRequest request);
        Task DeleteTransactionAsync(int transactionId);
        Task CreateTransactionAsync(TransactionRequest request);
        Task UpdateTransactionAsync(TransactionRequest request);
    }
}
