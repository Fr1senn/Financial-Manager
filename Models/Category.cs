namespace financial_manager.Models
{
    public class Category
    {
        public int Id { get; init; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Transaction>? Transactions { get; set; }
        public User? User { get; set; }

        public Category() { }
    }
}
