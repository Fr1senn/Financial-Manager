namespace financial_manager.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Significance { get; set; }
        public string TransactionType { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Category? Category { get; set; }

        public Transaction() { }

        public Transaction(int id, string title, decimal significance, string transactionType, Category? category, DateTime? createdAt = null, DateTime? expenseDate = null)
        {
            Id = id;
            Title = title;
            Significance = significance;
            TransactionType = transactionType;
            Category = category;
            ExpenseDate = expenseDate;
            CreatedAt = createdAt ?? DateTime.Now;
        }
    }
}
