using System.Globalization;
using financial_manager.Entities;
using financial_manager.Entities.DTOs;
using financial_manager.Entities.Extentions;
using financial_manager.Entities.Models;
using financial_manager.Entities.Requests;
using financial_manager.Repositories.Interfaces;
using financial_manager.Services.Interfaces;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using financialManagerAPI.Utilities;

namespace financial_manager.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinancialManagerContext _financialManagerContext;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TransactionRepository(
            FinancialManagerContext financialManagerContext,
            ICategoryService categoryService,
            IHttpContextAccessor httpContextAccessor


        )
        {
            _financialManagerContext = financialManagerContext;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<(IEnumerable<TransactionDTO>, int)> GetTransactionsAsync(PageRequest request)
        {
            int userId = _httpContextAccessor.HttpContext!.GetUserId();

            var query = _financialManagerContext.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == userId);

            if (request.Filters.Any())
            {
                var predicate = FilterPredicateBuilder.BuildFilterPredicate<Transaction>(request.Filters);
                query = query.Where(predicate);
            }

            int totalCount = query.Count();

            if (request.SortOptions != null && request.SortOptions.Any())
            {
                var sortExpressions = request.SortOptions.Select(sortOption =>
                    $"{sortOption.FieldName} {(sortOption.Ascending ? "asc" : "desc")}");

                string sortExpression = string.Join(", ", sortExpressions);
                query = query.OrderBy(sortExpression);
            }
            else
            {
                query = query.OrderByDescending(t => t.CreatedAt);
            }

            var transactions = await query
                .Skip(request.Take * request.Skip)
                .Take(request.Take)
                .Select(t => new TransactionDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Significance = t.Significance,
                    TransactionType = t.TransactionType,
                    CreatedAt = t.CreatedAt,
                    ExpenseDate = t.ExpenseDate,
                    Category = t.Category,
                })
                .AsNoTracking()
                .ToListAsync();


            return (transactions, totalCount);
        }

        public async Task DeleteTransactionAsync(int transactionId)
        {
            if (transactionId < 0)
                throw new Exception("The identifier can only be a non-negative integer");

            Transaction? transaction = await _financialManagerContext
                .Transactions.Where(t => t.Id == transactionId)
                .FirstOrDefaultAsync();

            if (transaction is null)
            {
                throw new Exception("The specified transaction does not exist");
            }

            _financialManagerContext.Transactions.Remove(transaction);
            await _financialManagerContext.SaveChangesAsync();
        }

        public async Task CreateTransactionAsync(TransactionRequest request)
        {
            if (request is null)
            {
                throw new Exception(nameof(Transaction));
            }

            int userId = _httpContextAccessor.HttpContext!.GetUserId();

            if (request.TransactionType == "expense")
            {
                Category? category = await _categoryService.GetTransactionCategoryAsync(request.Category!.Title);

                _financialManagerContext.Transactions.Add(
                    new Transaction
                    {
                        UserId = userId,
                        CategoryId = category.Id,
                        Title = request.Title,
                        Significance = request.Significance,
                        TransactionType = request.TransactionType,
                        CreatedAt = request.CreatedAt ?? DateTime.Now,
                        ExpenseDate = (request.ExpenseDate ?? DateTime.Now).ToLocalTime(),
                    }
                );
            }
            else
            {
                _financialManagerContext.Transactions.Add(
                    new Transaction
                    {
                        UserId = userId,
                        Title = request.Title,
                        Significance = request.Significance,
                        TransactionType = request.TransactionType,
                        CreatedAt = request.CreatedAt ?? DateTime.Now,
                    }
                );
            }

            await _financialManagerContext.SaveChangesAsync();
        }

        public async Task UpdateTransactionAsync(TransactionRequest request)
        {
            int userId = _httpContextAccessor.HttpContext!.GetUserId();
            Transaction? dbTransaction = await _financialManagerContext
                .Transactions.Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == request.Id);

            if (dbTransaction is null)
            {
                throw new Exception("Transaction does not exist");
            }

            if (dbTransaction.TransactionType == "expense")
            {
                Category? category = await _categoryService.GetTransactionCategoryAsync(
                    request.Category!.Title
                );

                dbTransaction.Title = request.Title;
                dbTransaction.Significance = request.Significance;
                dbTransaction.TransactionType = request.TransactionType;
                dbTransaction.ExpenseDate = request.ExpenseDate ?? dbTransaction.ExpenseDate;
                dbTransaction.CategoryId = category.Id;
            }
            else
            {
                dbTransaction.Title = request.Title;
                dbTransaction.Significance = request.Significance;
                dbTransaction.TransactionType = request.TransactionType;
            }

            await _financialManagerContext.SaveChangesAsync();
        }
    }
}
