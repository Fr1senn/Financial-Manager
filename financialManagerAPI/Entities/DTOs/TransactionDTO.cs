using financial_manager.Entities.Models;

namespace financial_manager.Entities.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Significance { get; set; }
        public string TransactionType { get; set; } = null!;
        public DateTime CreatedAt {  get; set; } = DateTime.Now;
        public Category? Category { get; set; }
        public DateTime? ExpenseDate { get; set; } = DateTime.Now;
    }
}
