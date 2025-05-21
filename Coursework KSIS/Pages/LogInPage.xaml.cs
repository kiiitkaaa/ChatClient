using Coursework_KSIS.Classes;
using Coursework_KSIS.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Coursework_KSIS.Pages
{
    /// <summary>
    /// Логика взаимодействия для LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Connect.Disconnect();
            Application.Current.Shutdown();
        }

        private async void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            string email = Email_TB.Text.Trim();
            if (!InputString.IsValidEmail(email))
            {
                var errorWindow = new ErrorWindow("Некорректный email. \nПример: example@domain.com");
                errorWindow.ShowDialog();
                return;
            }

            if (string.IsNullOrEmpty(Password_TB.Password))
            {
                var errorWindow = new ErrorWindow("Введите пароль");
                errorWindow.ShowDialog();
                return;
            }

            string hashPassword = PasswordHasher.HashPassword(Password_TB.Password);
            byte[] bytes = MessageToServer.LogInMessage(email: email, hashPassword: hashPassword);

            _ = Connect.SendMessageAsync(bytes);

            if (!await MessageFromServer.GetLogIn())
            {
                var errorWindow = new ErrorWindow("Некоректный логин или пароль");
                errorWindow.ShowDialog();
                return;
            }

            ChatWindow chatWindow = new();
            chatWindow.Show();

            Application.Current.MainWindow.Close();
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void Password_TB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password_TB.Password.Length > 0)
            {
                Password_Block.Visibility = Visibility.Hidden;
            }
            else
            {
                Password_Block.Visibility = Visibility.Visible;
            }
        }
    }
}
