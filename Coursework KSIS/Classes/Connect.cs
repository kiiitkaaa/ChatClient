using Coursework_KSIS.Windows;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Coursework_KSIS.Classes
{
    /// <summary>
    /// Класс для работы с сервером
    /// </summary>
    public static class Connect
    {
        public static string? IPSERVER { get; private set; }
        public static string? IPCLIENT { get; private set; }

        public const int PORT = 11000;
        private static TcpClient? client;
        private static NetworkStream? stream;

        /// <summary>
        /// Подключение к серверу
        /// </summary>
        /// <param name="ip">IP сервера</param>
        /// <returns>Состояние подключения</returns>
        public static async Task<bool> ConnectToServer(string ip)
        {
            IPSERVER = ip;

            try
            {
                client = new TcpClient();
                await client.ConnectAsync(IPSERVER, PORT);
                stream = client.GetStream();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Отключение от сервера
        /// </summary>
        public static void Disconnect()
        {
            _ = SendMessageAsync(MessageToServer.DisconnectMessage());
            stream?.Close();
            client?.Close();
        }

        /// <summary>
        /// Получение текущего IP адреса
        /// </summary>
        public static void GetCLientIP()
        {
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up &&
                    netInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    foreach (UnicastIPAddressInformation ip in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            IPCLIENT = ip.Address.ToString();
                        }
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Отпрака серверу сообщения
        /// </summary>
        /// <param name="data">Массив байт для отправки на сервер</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task SendMessageAsync(byte[] data)
        {
            if (stream == null)
            {
                throw new InvalidOperationException("Нет подключения к серверу.");
            }

            try
            {
                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отправке данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Получение сообщения от сервера
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<string> ReceiveMessageAsync()
        {
            if (stream == null)
            {
                throw new InvalidOperationException("Нет подключения к серверу.");
            }

            try
            {
                byte[] buffer = new byte[65534];
                int bytesRead = await stream.ReadAsync(buffer);

                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
            catch (Exception ex)
            {
                var errorWindow = new ErrorWindow($"Ошибка");
                errorWindow.ShowDialog();
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
                return "Error";
            }
        }
    }
}