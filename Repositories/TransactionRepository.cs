using financial_manager.Entities;
using financial_manager.Models;
using financial_manager.Repositories.Interfaces;
using financial_manager.Services;
using financial_manager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace financial_manager.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinancialManagerContext _financialManagerContext;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionRepository(FinancialManagerContext financialManagerContext, ICategoryService categoryService, IHttpContextAccessor httpContextAccessor)
        {
            _financialManagerContext = financialManagerContext;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(int packSize = 10, int pageNumber = 0)
        {
            if (packSize < 0) throw new ArgumentException("The collection size can only be a non-negative integer");

            if (pageNumber < 0) throw new ArgumentException("The page number can only be a non-negative integer");

            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return await _financialManagerContext.Transactions
                .Include(t => t.Category)
                .OrderByDescending(t => t.CreatedAt)
                .Skip(packSize * pageNumber)
                .Take(packSize)
                .Where(t => t.UserId == userId)
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
            if (transactionId < 0) throw new ArgumentException("The identifier can only be a non-negative integer");

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

            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (transaction.TransactionType == "expense")
            {
                Category? category = await _categoryService.GetTransactionCategoryAsync(transaction.Category!.Title);
                _financialManagerContext.Transactions.Add(new TransactionEntity
                {
                    UserId = userId,
                    CategoryId = category.Id,
                    Title = transaction.Title,
                    Significance = transaction.Significance,
                    TransactionType = transaction.TransactionType,
                    CreatedAt = transaction.CreatedAt,
                    ExpenseDate = transaction.ExpenseDate ?? DateTime.Now
                });
            }
            else
            {
                _financialManagerContext.Transactions.Add(new TransactionEntity
                {
                    UserId = userId,
                    Title = transaction.Title,
                    Significance = transaction.Significance,
                    TransactionType = transaction.TransactionType,
                    CreatedAt = transaction.CreatedAt,
                });
            }

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

            if (existingTransaction.TransactionType == "expense")
            {
                Category? category = await _categoryService.GetTransactionCategoryAsync(transaction.Category!.Title);

                existingTransaction.Title = transaction.Title;
                existingTransaction.Significance = transaction.Significance;
                existingTransaction.TransactionType = transaction.TransactionType;
                existingTransaction.ExpenseDate = transaction.ExpenseDate ?? existingTransaction.ExpenseDate;
                existingTransaction.CategoryId = category.Id;
            }
            else
            {
                existingTransaction.Title = transaction.Title;
                existingTransaction.Significance = transaction.Significance;
                existingTransaction.TransactionType = transaction.TransactionType;

            }


            await _financialManagerContext.SaveChangesAsync();
        }

        public async Task<int> GetUserTransactionQuantityAsync()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return (await _financialManagerContext.Transactions.Where(t => t.UserId == userId).AsNoTracking().ToListAsync()).Count();
        }
    }
}
