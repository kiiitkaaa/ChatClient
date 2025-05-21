using Coursework_KSIS.Windows;

namespace Coursework_KSIS.Classes
{
    /// <summary>
    /// Обработка полученных сообщений от сервера
    /// </summary>
    class MessageFromServer
    {
        /// <summary>
        /// Подтверждение входа
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> GetLogIn()
        {
            string messageFromServer = await Connect.ReceiveMessageAsync();
            if (messageFromServer == "ERROR")
            {
                return false;
            }
            else
            {
                string[] parts = messageFromServer.Split(',');

                if (!int.TryParse(parts[0], out int id))
                {
                    var errorWindow = new ErrorWindow("Не удалось распарсить ID пользователя");
                    errorWindow.ShowDialog();
                    return false;
                }

                GlobalDataUser.Id = id;
                GlobalDataUser.Username = parts[1];
                GlobalDataUser.PersonalName = parts[2];
                GlobalDataUser.Email = parts[3];
                GlobalDataUser.PhoneNumber = parts[4];
                GlobalDataUser.RSAPublicKey = parts[5];

                return true;
            }
        }

        /// <summary>
        /// Подтверждение регистрации
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> GetRegistration()
        {
            string messageFromServer = await Connect.ReceiveMessageAsync();

            if (messageFromServer == "Username" || messageFromServer == "Email" || messageFromServer == "Number")
            {
                var errorWindow = new ErrorWindow($"Некоторые данные уже используются: {messageFromServer}");
                errorWindow.ShowDialog();
                return false;
            }
            else
            {
                string[] parts = messageFromServer.Split(',');

                if (!int.TryParse(parts[0], out int id))
                {
                    var errorWindow = new ErrorWindow("Не удалось распарсить ID пользователя");
                    errorWindow.ShowDialog();
                    return false;
                }

                GlobalDataUser.Id = id;
                GlobalDataUser.Username = parts[1];
                GlobalDataUser.PersonalName = parts[2];
                GlobalDataUser.Email = parts[3];
                GlobalDataUser.PhoneNumber = parts[4];
                GlobalDataUser.RSAPublicKey = parts[5];

                return true;
            }
        }

        /// <summary>
        /// Получение результата поиска
        /// </summary>
        /// <returns></returns>
        public static async Task<(string Name, string Username, int ID, string phoneNumber)?> GetSearchUser()
        {
            string messageFromServer = await Connect.ReceiveMessageAsync();

            if (messageFromServer == "NO")
            {
                var errorWindow = new ErrorWindow($"Пользователь не найден");
                errorWindow.ShowDialog();
                return null;
            }

            string[] parts = messageFromServer.Split(',');

            if (parts.Length == 4 && int.TryParse(parts[0], out int id))
            {
                string username = parts[1];
                string name = parts[2];
                string phoneNumber = parts[3];

                return (name, username, id, phoneNumber);
            }
            else
            {
                var errorWindow = new ErrorWindow("Ошибка в ответе от сервера");
                errorWindow.ShowDialog();
                return null;
            }
        }

        /// <summary>
        /// Получение созданного чата
        /// </summary>
        /// <returns></returns>
        public static async Task<string?> GetCreatedChat()
        {
            string messageFromServer = await Connect.ReceiveMessageAsync();
            if (messageFromServer == "ERROR")
            {
                var errorWindow = new ErrorWindow($"Ошибка в ответе от сервера");
                errorWindow.ShowDialog();
                return null;
            }

            string[] parts = messageFromServer.Split(',');

            if (parts[0] == "SUCCESS")
            {
                return parts[1];
            }
            else
            {
                var errorWindow = new ErrorWindow($"Чат уже создан");
                errorWindow.ShowDialog();
                return null;
            }
        }

        /// <summary>
        /// Получение списка чатов
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ChatPreview>> GetAllChats()
        {
            var chats = new List<ChatPreview>();
            string messageFromServer = await Connect.ReceiveMessageAsync();

            if (messageFromServer == "NO")
            {
                return chats;
            }

            string[] chatEntries = messageFromServer.Split('|');

            foreach (var entry in chatEntries)
            {
                string[] parts = entry.Split(',');

                string otherName = parts[0];
                string otherUsername = parts[1];
                string otherRSAPublicKey = parts[2];
                if (!int.TryParse(parts[3], out int chatId))
                    continue;
                string lastMessage = parts[4];

                if (lastMessage == "Нет сообщений")
                {
                    chats.Add(new ChatPreview
                    {
                        OtherName = otherName,
                        OtherUsername = otherUsername,
                        OtherRSAPublicKey = otherRSAPublicKey,
                        ChatId = chatId,
                        LastMessage = lastMessage,
                    });
                }
                else
                {
                    string messageFrom = parts[5];
                    string keyAES1 = parts[6];
                    string keyAES2 = parts[7];
                    string decryptAES;

                    if (messageFrom == GlobalDataUser.Username)
                    {
                        decryptAES = RSAHelper.Decrypt(keyAES1, GlobalDataUser.Email);
                    }
                    else
                    {
                        decryptAES = RSAHelper.Decrypt(keyAES2, GlobalDataUser.Email);
                    }

                    string decryptedMessage = AESHelper.Decrypt(lastMessage, decryptAES);
                    chats.Add(new ChatPreview
                    {
                        OtherName = otherName,
                        OtherUsername = otherUsername,
                        OtherRSAPublicKey = otherRSAPublicKey,
                        ChatId = chatId,
                        LastMessage = decryptedMessage
                    });
                }
            }

            return chats;
        }

        /// <summary>
        /// Получение списка сообщений чата
        /// </summary>
        /// <returns></returns>
        public static async Task<List<(string SenderUsername, string Content, DateTime Timestamp)>> GetMessagesFromChat()
        {
            var messages = new List<(string SenderUsername, string Content, DateTime Timestamp)>();

            string messageFromServer = await Connect.ReceiveMessageAsync();
            if (messageFromServer == "ERROR")
            {
                var errorWindow = new ErrorWindow($"Ошибка в ответе от сервера");
                errorWindow.ShowDialog();
                return messages;
            }

            if (messageFromServer == "NO")
                return messages;

            var entries = messageFromServer.Split('|');

            foreach (var entry in entries)
            {
                var parts = entry.Split(',', 5);


                if (DateTime.TryParse(parts[2], null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime timestamp))
                {
                    string otherUsername = parts[0];
                    string encryptMessage = parts[1];
                    string keyAES1 = parts[3];
                    string keyAES2 = parts[4];
                    string decryptAES;

                    if (otherUsername == GlobalDataUser.Username)
                    {
                        decryptAES = RSAHelper.Decrypt(keyAES1, GlobalDataUser.Email);
                    }
                    else
                    {
                        decryptAES = RSAHelper.Decrypt(keyAES2, GlobalDataUser.Email);
                    }

                    string decryptMessage = AESHelper.Decrypt(encryptMessage, decryptAES);
                    messages.Add((otherUsername, decryptMessage, timestamp));
                }
            }
            return messages;
        }

        /// <summary>
        /// Получение статуса отправки сообщения
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> GetMessageSuccess()
        {
            string messageFromServer = await Connect.ReceiveMessageAsync();
            if (messageFromServer == "ERROR")
            {
                var errorWindow = new ErrorWindow($"Ошибка отправки сообщения");
                errorWindow.ShowDialog();
                return false;
            }
            return true;
        }
    }
}