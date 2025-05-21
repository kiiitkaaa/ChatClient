namespace Coursework_KSIS.Classes
{
    /// <summary>
    /// Данные текущего пользователя
    /// </summary>
    static class GlobalDataUser
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public static int Id { get; set; }

        /// <summary>
        /// Имя пользователя (@example)
        /// </summary>
        public static string? Username { get; set; }

        /// <summary>
        /// Собственное имя пользователя
        /// </summary>
        public static string? PersonalName { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public static string? Email { get; set; }

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public static string? PhoneNumber { get; set; }

        /// <summary>
        /// Публичный ключ RSA данного пользователя
        /// </summary>
        public static string? RSAPublicKey { get; set; }
    }
}