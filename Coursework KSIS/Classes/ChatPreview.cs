namespace Coursework_KSIS.Classes
{
    /// <summary>
    /// Миниатюра чата из списка
    /// </summary>
    public class ChatPreview
    {
        /// <summary>
        /// Собственное имя пользователя с кем чат
        /// </summary>
        public string OtherName { get; set; } = string.Empty;
        /// <summary>
        /// Имя пользователя с кем чат
        /// </summary>
        public string OtherUsername { get; set; } = string.Empty;
        /// <summary>
        /// ID чата
        /// </summary>
        public int ChatId { get; set; }
        /// <summary>
        /// Последнее сообщение
        /// </summary>
        public string LastMessage { get; set; } = string.Empty;
        /// <summary>
        /// Публичный ключ RSA собеседника
        /// </summary>
        public string OtherRSAPublicKey { get; set; } = string.Empty;
    }
}
