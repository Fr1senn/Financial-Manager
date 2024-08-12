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
                    CreatedAt = t.CreatedAt,
                    ExpenseDate = t.ExpenseDate,
                    Category = t.Category != null ? new Category
                    {
                        Id = t.Category.Id,
                        Title = t.Category.Title,
                        CreatedAt = t.Category.CreatedAt,
                        Transactions = null
                    } : null,
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteTransactionAsync(int transactionId)
        {
            TransactionEntity? transaction = await _financialManagerContext.Transactions.Where(t => t.Id == transactionId).FirstOrDefaultAsync();

            if (transaction is null)
            {
                throw new NullReferenceException("The specified transaction does not exist");
            }

            _financialManagerContext.Transactions.Remove(transaction);
            await _financialManagerContext.SaveChangesAsync();
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            if (transaction is null)
            {
                throw new NullReferenceException(nameof(Transaction));
            }

            Category? category = await GetTransactionCategoryAsync(transaction.Category!.Title);

            _financialManagerContext.Transactions.Add(new TransactionEntity
            {
                UserId = 1,
                CategoryId = category.Id,
                Title = transaction.Title,
                Significance = transaction.Significance,
                TransactionType = transaction.TransactionType,
                CreatedAt = transaction.CreatedAt,
                ExpenseDate = transaction.ExpenseDate,
            });

            await _financialManagerContext.SaveChangesAsync();
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            TransactionEntity? existingTransaction = await _financialManagerContext.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == transaction.Id);

            if (existingTransaction is null)
            {
                throw new NullReferenceException("Transaction does not exist");
            }

            Category? category = await GetTransactionCategoryAsync(transaction.Category!.Title);

            existingTransaction.Title = transaction.Title;
            existingTransaction.Significance = transaction.Significance;
            existingTransaction.TransactionType = transaction.TransactionType;
            existingTransaction.ExpenseDate = transaction.ExpenseDate == null ? existingTransaction.ExpenseDate : transaction.ExpenseDate;
            existingTransaction.CategoryId = category.Id;

            await _financialManagerContext.SaveChangesAsync();
        }

        private async Task<Category> GetTransactionCategoryAsync(string categoryTitle)
        {
            Category? category = await _financialManagerContext.Categories
                .Where(c => c.Title == categoryTitle && c.UserId == 1)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Title = c.Title,
                    CreatedAt = c.CreatedAt,
                })
                .FirstOrDefaultAsync();

            if (category is null)
            {
                throw new NullReferenceException("The provided category does not exist");
            }

            return category;
        }
    }
}
