using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Coursework_KSIS.Classes
{
    /// <summary>
    /// Работа с RSA шифрованием
    /// </summary>
    public static class RSAHelper
    {
        /// <summary>
        /// Публичный ключ
        /// </summary>
        public static string? PublicKey { get; private set; }

        /// <summary>
        /// Приватный ключ
        /// </summary>
        public static string? PrivateKey { get; private set; }

        private static readonly string JsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "private_keys.json");

        /// <summary>
        /// Генерация ключей и сохранение приватного ключа по почте
        /// </summary>
        /// <param name="email"></param>
        /// <param name="keySize"></param>
        /// <returns></returns>
        public static string GenerateKeys(string email, int keySize = 2048)
        {
            using RSA rsa = RSA.Create();
            rsa.KeySize = keySize;

            PublicKey = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo());
            PrivateKey = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey());

            SavePrivateKeyToJson(email, PrivateKey);

            return PublicKey;
        }

        /// <summary>
        /// Шифрование сообщения
        /// </summary>
        /// <param name="base64PublicKey"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(string base64PublicKey, string plainText)
        {
            byte[] publicKeyBytes = Convert.FromBase64String(base64PublicKey);
            using RSA rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Дешифровка сообщения по почте
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptedText, string email)
        {
            string privateKey = LoadPrivateKeyFromJson(email);

            byte[] privateKeyBytes = Convert.FromBase64String(privateKey);
            using RSA rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
            byte[] encryptedData = Convert.FromBase64String(encryptedText);
            byte[] decrypted = rsa.Decrypt(encryptedData, RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(decrypted);
        }

        /// <summary>
        /// Сохранение ключа в JSON по почте
        /// </summary>
        /// <param name="email"></param>
        /// <param name="privateKey"></param>
        private static void SavePrivateKeyToJson(string email, string privateKey)
        {
            Dictionary<string, string> keys = [];

            if (File.Exists(JsonFilePath))
            {
                string json = File.ReadAllText(JsonFilePath);
                keys = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
            }

            keys[email] = privateKey;

            string updatedJson = JsonSerializer.Serialize(keys, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JsonFilePath, updatedJson, Encoding.UTF8);
        }

        /// <summary>
        /// Загрузка ключа из JSON по почте
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        private static string LoadPrivateKeyFromJson(string email)
        {
            if (!File.Exists(JsonFilePath))
                throw new FileNotFoundException("Файл private_keys.json не найден.");

            string json = File.ReadAllText(JsonFilePath);
            var keys = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            if (keys != null && keys.TryGetValue(email, out string? privateKey))
            {
                return privateKey;
            }

            throw new Exception($"Приватный ключ для {email} не найден.");
        }
    }
}
