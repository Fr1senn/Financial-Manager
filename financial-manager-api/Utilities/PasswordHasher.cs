using System.Security.Cryptography;
using financial_manager.Utilities.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace financial_manager.Utilities
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public static string HashPassword(string password, byte[] salt)
        {
            var hashedPassword = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: HashSize
            );

            return Convert.ToBase64String(hashedPassword);
        }

        public static byte[] GenerateSalt()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var salt = new byte[SaltSize];
                rng.GetBytes(salt);
                return salt;
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword, byte[] salt)
        {
            var hashedInputPassword = HashPassword(password, salt);

            return hashedPassword == hashedInputPassword;
        }
    }
}
