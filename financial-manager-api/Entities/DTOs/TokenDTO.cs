namespace financial_manager.Entities.DTOs
{
    public class TokenDTO
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public bool isRevoked { get; set; }
        public UserDTO? User { get; set; }
    }
}
