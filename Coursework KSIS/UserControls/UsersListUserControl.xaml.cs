using Coursework_KSIS.Classes;
using Coursework_KSIS.Windows;
using System.Windows;
using System.Windows.Controls;

namespace Coursework_KSIS.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UsersListUserControl.xaml
    /// </summary>
    public partial class UsersListUserControl : UserControl
    {
        public required ChatWindow ParentWindow { get; set; }

        public UsersListUserControl()
        {
            InitializeComponent();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Search_TB.Text))
            {
                var errorWindow = new ErrorWindow("Заполните поле поиска");
                errorWindow.ShowDialog();
                return;
            }

            byte[] bytes = MessageToServer.SearchUserMessage(Search_TB.Text);
            _ = Connect.SendMessageAsync(bytes);
            var message = await MessageFromServer.GetSearchUser();
            if (message != null)
            {
                var searchedUser = new SearchedUserUserControl(username: message.Value.Username, name: message.Value.Name, ID: message.Value.ID, phoneNumber: message.Value.phoneNumber) { ParentWindow = ParentWindow };
                UsersContainer.Children.Add(searchedUser);
            }
        }
    }
}
