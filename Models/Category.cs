namespace financial_manager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<Transaction>? Transactions { get; set; }

        public Category() { }

        public Category(int id, string title, List<Transaction> transactions, DateTime createdAt)
        {
            Id = id;
            Title = title;
            CreatedAt = createdAt;
            Transactions = transactions;
        }
    }
}
