using System.ComponentModel.DataAnnotations;

namespace financial_manager.Entities.Requests
{
    public class TransactionRequest
    {
        public int? Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Title { get; set; } = null!;
        [Required]
        [Range(1, 99999.99)]
        public decimal Significance { get; set; }
        [Required]
        public string TransactionType { get; set; } = null!;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ExpenseDate { get; set; } = DateTime.Now;
        public CategoryRequest? Category { get; set; }
    }
}
