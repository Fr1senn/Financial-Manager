using System.ComponentModel.DataAnnotations;

namespace financial_manager.Entities.Requests
{
    public class CategoryRequest
    {
        public int? Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Title { get; set; } = null!;
        public DateTime? CreateAt { get; set; } = DateTime.Now;
    }
}
