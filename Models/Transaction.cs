using financial_manager.Models.Enums;

namespace financial_manager.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Significance { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public DateTime CreatedAt { get; private set; }

        public Transaction(int id, string title, decimal significance, TransactionType transactionType, DateTime? expenseDate = null)
        {
            Id = id;
            Title = title;
            Significance = significance;
            TransactionType = transactionType;
            ExpenseDate = expenseDate;
            CreatedAt = DateTime.Now;
        }
    }
}
