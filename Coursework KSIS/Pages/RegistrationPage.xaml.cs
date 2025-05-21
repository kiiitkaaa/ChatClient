using Coursework_KSIS.Classes;
using Coursework_KSIS.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Coursework_KSIS.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Connect.Disconnect();
            Application.Current.Shutdown();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PasswordReg_TB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordReg_TB.Password.Length > 0)
            {
                PassReg_TB.Visibility = Visibility.Hidden;
            }
            else
            {
                PassReg_TB.Visibility = Visibility.Visible;
            }
        }

        private void CheckPasword_TB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (CheckPasword_TB.Password.Length > 0)
            {
                PassCheck_TB.Visibility = Visibility.Hidden;
            }
            else
            {
                PassCheck_TB.Visibility = Visibility.Visible;
            }
        }

        private async void ButtonRegistr_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Name_TB.Text) 
                ||string.IsNullOrEmpty(CheckPasword_TB.Password)
                || string.IsNullOrEmpty(PasswordReg_TB.Password))
            {
                var errorWindow = new ErrorWindow("Заполните все поля");
                errorWindow.ShowDialog();
                return;
            }

            string email = EmailReg_TB.Text.Trim();
            if (!InputString.IsValidEmail(email))
            {
                var errorWindow = new ErrorWindow("Некорректный email. \nПример: example@domain.com");
                errorWindow.ShowDialog();
                return;
            }

            string phoneNumber = Number_TB.Text.Trim();
            if (!InputString.IsValidPhoneNumber(phoneNumber))
            {
                var errorWindow = new ErrorWindow("Некорректный номер телефона. \nПример: +1234567890");
                errorWindow.ShowDialog();
                return;
            }

            string username = Username_TB.Text.Trim();
            if (!InputString.IsValidUsername(username))
            {
                var errorWindow = new ErrorWindow("Некорректное имя пользователя. \nПример: @example123");
                errorWindow.ShowDialog();
                return;
            }

            if (PasswordReg_TB.Password != CheckPasword_TB.Password)
            {
                var errorWindow = new ErrorWindow("Проверка пароля не совпадает.");
                errorWindow.ShowDialog();
                return;
            }

            string publicRSA = RSAHelper.GenerateKeys(email);

            string hashPassword = PasswordHasher.HashPassword(CheckPasword_TB.Password);
            byte[] bytes = MessageToServer.RegistrationMessage(email: email, phoneNumber: phoneNumber, personalName: Name_TB.Text, username: username, hashPassword: hashPassword, publicKeyRSA: publicRSA);

            _ = Connect.SendMessageAsync(bytes);

            if (await MessageFromServer.GetRegistration())
            {
                ChatWindow chatWindow = new();
                chatWindow.Show();

                Application.Current.MainWindow.Close();
            }
            return;
        }
    }
}
