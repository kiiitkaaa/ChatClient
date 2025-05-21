using System.Security.Cryptography;
using System.Text;

namespace Coursework_KSIS.Classes
{
    /// <summary>
    /// Класс хэширования пороля
    /// </summary>
    static class PasswordHasher
    {
        /// <summary>
        /// Получение хэш-кода пароля
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = SHA256.HashData(passwordBytes);

            StringBuilder hashStringBuilder = new();
            foreach (byte b in hashBytes)
            {
                hashStringBuilder.Append(b.ToString("x2"));
            }

            return hashStringBuilder.ToString();
        }
    }
}