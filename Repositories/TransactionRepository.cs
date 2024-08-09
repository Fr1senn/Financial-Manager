using financial_manager.Entities;
using financial_manager.Models;
using financial_manager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinancialManagerContext _financialManagerContext;

        public TransactionRepository(FinancialManagerContext financialManagerContext)
        {
            _financialManagerContext = financialManagerContext;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(int packSize = 10, int pageNumber = 0)
        {
            return await _financialManagerContext.Transactions
                .Include(t => t.Category)
                .OrderByDescending(t => t.CreatedAt)
                .Skip(pageNumber * packSize)
                .Take(packSize)
                .Select(t => new Transaction
                {
                    Id = t.Id,
                    Title = t.Title,
                    Significance = t.Significance,
                    TransactionType = t.TransactionType,
                    Category = t.Category != null ? new Category
                    {
                        Id = t.Category.Id,
                        Title = t.Category.Title,
                        CreatedAt = t.Category.CreatedAt,
                        Transactions = null
                    } : null,
                    CreatedAt = t.CreatedAt,
                    ExpenseDate = t.ExpenseDate,
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
