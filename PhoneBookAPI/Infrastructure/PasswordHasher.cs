namespace PhoneBookAPI.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using PhoneBookAPI.Data.Entities;
    using PhoneBookAPI.Infrastructure.Interfaces;
    using System.Security.Cryptography;

    public class PasswordHasher : IPasswordHasher
    {

        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';
        public (string hash, string salt) Hash(string Password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(Password, salt, Iterations, _hashAlgorithmName, KeySize);
            return (string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash)), Convert.ToBase64String(salt));
        }

        public bool Verify(string HashPassword, string InputPassword)
        {
            var elements = HashPassword.Split(Delimiter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);
            var hashInput = Rfc2898DeriveBytes.Pbkdf2(InputPassword, salt, Iterations, _hashAlgorithmName, KeySize);
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }

      
    }
}
