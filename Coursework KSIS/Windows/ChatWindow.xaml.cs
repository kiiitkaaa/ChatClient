using Coursework_KSIS.Classes;
using Coursework_KSIS.UserControls;
using System.Windows;
using System.Windows.Input;

namespace Coursework_KSIS.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        public ChatWindow()
        {
            InitializeComponent();
            ShowChats();
            Connect.GetCLientIP();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Connect.Disconnect();
            Application.Current.Shutdown();
        }

        private void ButtonMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private async void ShowChats()
        {
            MiniContainer.Children.Clear();
            LargeContainer.Children.Clear();

            byte[] bytes = MessageToServer.ShowChatsMessage(GlobalDataUser.Username);
            _ = Connect.SendMessageAsync(bytes);
            var message = await MessageFromServer.GetAllChats();

            var newMini = new ChatListUserControl(message, this);
            var newLarge = new LoadEnterUserControl();

            MiniContainer.Children.Add(newMini);
            LargeContainer.Children.Add(newLarge);
        }

        private void ButtonChats_Click(object sender, RoutedEventArgs e)
        {
            ShowChats();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            MiniContainer.Children.Clear();
            LargeContainer.Children.Clear();

            var newMini = new SettingsListUserControl { ParentWindow = this };
            var newLarge = new LoadEnterUserControl();

            MiniContainer.Children.Add(newMini);
            LargeContainer.Children.Add(newLarge);
        }

        private void ButtonUsers_Click(object sender, RoutedEventArgs e)
        {
            MiniContainer.Children.Clear();
            LargeContainer.Children.Clear();

            var newMini = new UsersListUserControl { ParentWindow = this };
            var newLarge = new LoadEnterUserControl();

            MiniContainer.Children.Add(newMini);
            LargeContainer.Children.Add(newLarge);
        }

        public void ShowAccauntInfo()
        {
            LargeContainer.Children.Clear();
            var newLarge = new AccauntSettingsUserControl();
            LargeContainer.Children.Add(newLarge);
        }

        public void ShowConnectInfo()
        {
            LargeContainer.Children.Clear();
            var newLarge = new ConnectInfoUserControl();
            LargeContainer.Children.Add(newLarge);
        }

        public void ShowProgramInfo()
        {
            LargeContainer.Children.Clear();
            var newLarge = new AboutProgramUserControl();
            LargeContainer.Children.Add(newLarge);
        }

        public void ShowAuthorInfo()
        {
            LargeContainer.Children.Clear();
            var newLarge = new AboutAuthorUserControl();
            LargeContainer.Children.Add(newLarge);
        }

        public void ShowUserAction(string name, string username, int ID, string phoneNumber)
        {
            LargeContainer.Children.Clear();
            var actionUser = new UserActionUserControl(name: name, username: username, ID: ID, phoneNumber: phoneNumber) { ParentWindow = this };
            LargeContainer.Children.Add(actionUser);
        }

        public void OpenChat(List<(string SenderUsername, string Content, DateTime Timestamp)> chatMessages, int chatID, string name, string RSAPublicKeyClient2, string otherUsername)
        {
            LargeContainer.Children.Clear();
            var largeChat = new ChattingUserControl(chatMessages, chatID, name, RSAPublicKeyClient2, otherUsername, this);
            LargeContainer.Children.Add(largeChat);
        }

        public async void UpdateChatArea(int chatID, string name, string RSAPublicKeyClient2, string otherUsername)
        {
            MiniContainer.Children.Clear();
            LargeContainer.Children.Clear();

            byte[] bytesAllChats = MessageToServer.ShowChatsMessage(GlobalDataUser.Username);
            _ = Connect.SendMessageAsync(bytesAllChats);
            var message = await MessageFromServer.GetAllChats();

            var newMini = new ChatListUserControl(message, this);

            byte[] bytesCurrentChat = MessageToServer.CurrentChatMessage(chatID);
            _ = Connect.SendMessageAsync(bytesCurrentChat);
            var messages = await MessageFromServer.GetMessagesFromChat();

            var largeChat = new ChattingUserControl(messages, chatID, name, RSAPublicKeyClient2, otherUsername, this);

            MiniContainer.Children.Add(newMini);
            LargeContainer.Children.Add(largeChat);
        }
    }
}
