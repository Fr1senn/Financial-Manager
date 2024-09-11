using System.ComponentModel.DataAnnotations;

namespace financial_manager.Entities.Requests
{
    public class RegisterRequest
    {
        [Required]
        [MinLength(8)]
        public string FullName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(12)]
        public string Password { get; set; } = null!;
        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
