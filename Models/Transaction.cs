namespace financial_manager.Models
{
    public class Transaction
    {
        public int Id { get; init; }
        public string Title { get; set; } = string.Empty;
        public decimal Significance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ExpenseDate { get; set; } = DateTime.Now;
        public Category? Category { get; set; }

        private string _transactionType;
        public string TransactionType
        {
            get => _transactionType;
            set
            {
                if (value != "income" && value != "expense")
                {
                    throw new ArgumentException("Transaction type must be either 'income' or 'expense'.");
                }
                _transactionType = value;
            }
        }

        public Transaction() { }
    }
}
