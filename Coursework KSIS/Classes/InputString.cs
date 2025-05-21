using System.Net;
using System.Text.RegularExpressions;

namespace Coursework_KSIS.Classes
{
    /// <summary>
    /// Класс проверки вводимых строк
    /// </summary>
    public static class InputString
    {
        /// <summary>
        /// Проверка ввода IPv4
        /// </summary>
        /// <param name="ip">IP</param>
        /// <returns>Подтвеждение корректности ввода</returns>
        public static bool IsValidIPv4(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                return false;

            string pattern = @"^(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\."
                           + @"(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\."
                           + @"(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\."
                           + @"(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])$";

            if (!Regex.IsMatch(ip, pattern))
                return false;

            return IPAddress.TryParse(ip, out _);
        }

        /// <summary>
        /// Проверка ввода почты
        /// </summary>
        /// <param name="email">Почта</param>
        /// <returns>Подтвеждение корректности ввода</returns>
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$";
            Regex regex = new(pattern);

            return regex.IsMatch(email);
        }

        /// <summary>
        /// Проверка ввода номера телефона
        /// </summary>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <returns>Подтвеждение корректности ввода</returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^(\+?[1-9]{1}[0-9]{1,14})$";
            Regex regex = new(pattern);

            return regex.IsMatch(phoneNumber);
        }

        /// <summary>
        /// Проверка ввода имени пользователя
        /// </summary>
        /// <param name="username">Имя полтзователя</param>
        /// <returns>Подтвеждение корректности ввода</returns>
        public static bool IsValidUsername(string username)
        {
            string pattern = @"^@[a-zA-Z0-9]+$";
            Regex regex = new(pattern);

            return regex.IsMatch(username);
        }
    }
}