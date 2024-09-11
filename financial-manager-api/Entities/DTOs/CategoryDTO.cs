namespace financial_manager.Entities.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public IEnumerable<TransactionDTO>? Transactions { get; set; }
        public UserDTO? User { get; set; }
    }
}
