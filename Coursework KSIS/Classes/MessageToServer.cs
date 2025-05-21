using System.Text;

namespace Coursework_KSIS.Classes
{
    /// <summary>
    /// Обработка отправляемых сообщений серверу
    /// </summary>
    static class MessageToServer
    {
        /// <summary>
        /// Сообщение для отключения соединения
        /// </summary>
        /// <returns></returns>
        public static byte[] DisconnectMessage()
        {
            string message = "0 STOP";
            return Encoding.UTF8.GetBytes(message);
        }

        /// <summary>
        /// Сообщение для входа
        /// </summary>
        /// <param name="email"></param>
        /// <param name="hashPassword"></param>
        /// <returns></returns>
        public static byte[] LogInMessage(string email, string hashPassword)
        {
            string message = $"1 {email},{hashPassword}";
            return Encoding.UTF8.GetBytes(message);
        }

        /// <summary>
        /// Сообщение для регистрации
        /// </summary>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="personalName"></param>
        /// <param name="username"></param>
        /// <param name="hashPassword"></param>
        /// <returns></returns>
        public static byte[] RegistrationMessage(string email, string phoneNumber, string personalName, string username, string hashPassword, string publicKeyRSA)
        {
            string message = $"2 {email},{phoneNumber},{personalName},{username},{hashPassword},{publicKeyRSA}";
            return Encoding.UTF8.GetBytes(message);
        }

        /// <summary>
        /// Сообщение для поиска пользователя по имени
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static byte[] SearchUserMessage(string username)
        {
            string message = $"3 {username}";
            return Encoding.UTF8.GetBytes(message);
        }

        /// <summary>
        /// Сообщение для создания чата
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static byte[] CreateChatMessage(int ID)
        {
            string message = $"4 {ID}";
            return Encoding.UTF8.GetBytes(message);
        }

        /// <summary>
        /// Сообщение для получения списка чатов
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static byte[] ShowChatsMessage(string? username)
        {
            string message = $"5 {username}";
            return Encoding.UTF8.GetBytes(message);
        }

        /// <summary>
        /// Сообщение для получения определённого чата
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        public static byte[] CurrentChatMessage(int chatID)
        {
            string message = $"6 {chatID}";
            return Encoding.UTF8.GetBytes(message);
        }

        /// <summary>
        /// Отправка сообщения пользователя
        /// </summary>
        /// <param name="senderUsername"></param>
        /// <param name="chatID"></param>
        /// <param name="messageText"></param>
        /// <returns></returns>
        public static byte[] SentMessage(string senderUsername, int chatID, string messageText, string RSAPublicKeyClient2)
        {
            string AESKey = AESHelper.GenerateKey();
            string encryptedText = AESHelper.Encrypt(messageText, AESKey);
            string key1 = RSAHelper.Encrypt(GlobalDataUser.RSAPublicKey, AESKey);
            string key2 = RSAHelper.Encrypt(RSAPublicKeyClient2, AESKey);

            string message = $"7 {senderUsername},{chatID},{encryptedText},{key1},{key2}";
            return Encoding.UTF8.GetBytes(message);
        }
    }
}