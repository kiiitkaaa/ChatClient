using Coursework_KSIS.Classes;
using Coursework_KSIS.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Coursework_KSIS.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConnectPage.xaml
    /// </summary>
    public partial class ConnectPage : Page
    {
        public ConnectPage()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(IP_TB.Text))
            {
                var errorWindow = new ErrorWindow("Заполните все поля");
                errorWindow.ShowDialog();
                return;
            }

            string ip = IP_TB.Text.Trim();
            if (!InputString.IsValidIPv4(ip))
            {
                var errorWindow = new ErrorWindow("Некорректный IP адрес");
                errorWindow.ShowDialog();
                return;
            }

            ConnectionToServer();
        }

        private async void ConnectionToServer()
        {
            bool isConnected = await Connect.ConnectToServer(ip: IP_TB.Text);
            if (isConnected)
            {
                NavigationService.Navigate(new LogInPage());
            }
            else
            {
                var errorWindow = new ErrorWindow("Не удалось подключиться к серверу");
                errorWindow.ShowDialog();
            }
        }
    }
}
