namespace financial_manager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public List<Transaction> Transactions { get; set; }

        public Category(int id, string title, List<Transaction> transactions)
        {
            Id = id;
            Title = title;
            CreatedAt = DateTime.Now;
            Transactions = transactions;
        }
    }
}
