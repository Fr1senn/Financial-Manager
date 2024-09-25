using System.ComponentModel.DataAnnotations;

namespace financialManagerAPI.Entities.Requests
{
    public class UserRequest
    {
        [Required]
        [MinLength(8)]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public int MonthlyBudget { get; set; }

        [Range(1, 31)]
        public short BudgetUpdateDay { get; set; }
    }
}
