using financial_manager.Entities;
using financial_manager.Entities.Models;
using financial_manager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Services;

public class TransactionService : ITransactionService
{
    private readonly FinancialManagerContext _financialManagerContext;

    public TransactionService(FinancialManagerContext financialManagerContext)
    {
        _financialManagerContext = financialManagerContext;
    }

    public async Task<IEnumerable<MonthlyTransactionData>> GetMonthlyTransactionsAsync(
        int year,
        int userId
    )
    {
        return await _financialManagerContext
            .Transactions.Where(t =>
                t.UserId == userId
                && (
                    t.CreatedAt.Year == year
                    || (t.ExpenseDate.HasValue && t.ExpenseDate.Value.Year == year)
                )
            )
            .GroupBy(t => new
            {
                IncomeMonth = t.TransactionType == "income" ? t.CreatedAt.Month : (int?)null,
                ExpenseMonth = t.TransactionType == "expense" && t.ExpenseDate.HasValue
                    ? t.ExpenseDate.Value.Month
                    : (int?)null,
            })
            .Select(g => new MonthlyTransactionData
            {
                Month = g.Key.IncomeMonth ?? g.Key.ExpenseMonth,
                Income = g.Where(t => t.TransactionType == "income" && t.CreatedAt.Year == year)
                    .Sum(t => t.Significance),
                Expense = g.Where(t =>
                        t.TransactionType == "expense"
                        && t.ExpenseDate.HasValue
                        && t.ExpenseDate.Value.Year == year
                    )
                    .Sum(t => t.Significance),
            })
            .ToListAsync();
    }
}
