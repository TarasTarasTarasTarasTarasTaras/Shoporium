using System.Security.Cryptography;
using System.Text;

namespace Shoporium.Business.Helpers
{
    public static class HashManager
    {
        public static byte[] ComputeSha512Hash(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            HashAlgorithm sha = new SHA512CryptoServiceProvider();
            var buffer = Encoding.UTF8.GetBytes(input);
            return sha.ComputeHash(buffer);
        }

        public static string HashPassword(byte[] encryptedUserName, string password)
        {
            var salt = Sha512Hash(encryptedUserName);
            var saltedPassword = $"{salt}{password}";

            var passwordHash = Sha512Hash(saltedPassword);

            return passwordHash;
        }

        private static string Sha512Hash(string input, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;

            var data = encoding.GetBytes(input);
            return Sha512Hash(data);
        }

        private static string Sha512Hash(byte[] data)
        {
            HashAlgorithm sha = SHA512.Create();
            var hash = sha.ComputeHash(data);
            var output = Convert.ToBase64String(hash);
            return output;
        }
    }
}
