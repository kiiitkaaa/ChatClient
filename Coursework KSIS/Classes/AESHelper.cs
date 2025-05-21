using System.Security.Cryptography;
using System.Text;

namespace Coursework_KSIS.Classes
{
    /// <summary>
    /// Работа с AES шифрованием
    /// </summary>
    public static class AESHelper
    {
        private static readonly byte[] IV =
        [
            0xA1, 0xB2, 0xC3, 0xD4, 0xE5, 0xF6, 0x07, 0x18,
            0x29, 0x3A, 0x4B, 0x5C, 0x6D, 0x7E, 0x8F, 0x90
        ];

        /// <summary>
        /// Генерация случайного AES-ключа
        /// </summary>
        /// <returns>Ключ AES</returns>
        public static string GenerateKey()
        {
            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }

        /// <summary>
        /// Шифрование текста с использованием переданного base64 AES-ключа
        /// </summary>
        /// <param name="plainText">Исходный текст</param>
        /// <param name="base64Key">Ключ AES</param>
        /// <returns>Зашифрованный текст</returns>
        public static string Encrypt(string plainText, string base64Key)
        {
            byte[] key = Convert.FromBase64String(base64Key);

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = IV;

            var encryptor = aes.CreateEncryptor();
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Расшифровка текста с использованием переданного base64 AES-ключа
        /// </summary>
        /// <param name="cipherText">Зашифрованный текст</param>
        /// <param name="base64Key">Ключ AES</param>
        /// <returns>Расшифрованный текст</returns>
        public static string Decrypt(string cipherText, string base64Key)
        {
            byte[] key = Convert.FromBase64String(base64Key);

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = IV;

            var decryptor = aes.CreateDecryptor();
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            byte[] decrypted = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
