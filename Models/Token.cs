namespace financial_manager.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; } = null!;
        public bool IsRevoked { get; set; }
        public DateTime ExpirationDate { get; set; }
        public User? User { get; set; }
    }
}
