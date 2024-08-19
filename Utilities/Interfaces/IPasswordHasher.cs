namespace financial_manager.Utilities.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string rawPassword, byte[] salt);
    }
}
