using Coursework_KSIS.Classes;
using Coursework_KSIS.Windows;
using System.Windows;
using System.Windows.Controls;

namespace Coursework_KSIS.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UserActionUserControl.xaml
    /// </summary>
    public partial class UserActionUserControl : UserControl
    {
        public required ChatWindow ParentWindow { get; set; }

        private readonly string name;
        private readonly string username;
        private readonly int ID;
        private readonly string phoneNumber;

        public UserActionUserControl(string name, string username, int ID, string phoneNumber)
        {
            InitializeComponent();

            this.name = name;
            this.username = username;
            this.ID = ID;
            this.phoneNumber = phoneNumber;
            SetLabel();
        }

        private void SetLabel()
        {
            Name_LBL.Content = name;
            Username_LBL.Content = username;
            Number_LBL.Content = phoneNumber;
        }

        private async void ButtonCreateChat_Click(object sender, RoutedEventArgs e)
        {
            byte[] bytes = MessageToServer.CreateChatMessage(ID);
            _ = Connect.SendMessageAsync(bytes);
            var message = await MessageFromServer.GetCreatedChat();
            if (message != null)
            {
                var infoWindow = new InfoWindow("Чат создан");
                infoWindow.ShowDialog();
            }
        }
    }
}
