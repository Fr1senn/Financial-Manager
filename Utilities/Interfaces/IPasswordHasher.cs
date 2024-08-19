namespace financial_manager.Utilities.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string rawPassword, byte[] salt);

        byte[] GenerateSalt();

        bool VerifyPassword(string rawPassword, string hashedPassword, byte[] salt);
    }
}
