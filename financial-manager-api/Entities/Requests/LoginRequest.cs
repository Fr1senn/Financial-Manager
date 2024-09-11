using System.ComponentModel.DataAnnotations;

namespace financial_manager.Entities.Requests
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(12)]
        public string Password { get; set; } = null!;
    }
}
