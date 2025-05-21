using Coursework_KSIS.Classes;
using Coursework_KSIS.Windows;
using System.Windows.Controls;

namespace Coursework_KSIS.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ChattingUserControl.xaml
    /// </summary>
    public partial class ChattingUserControl : UserControl
    {
        private readonly ChatWindow parentWindow;
        private readonly int chatID;
        private readonly string name;
        private readonly string otherUsername;
        private readonly string RSAPublicKeyClient2;
        private readonly List<(string SenderUsername, string Content, DateTime Timestamp)> chatMessages;

        public ChattingUserControl(List<(string SenderUsername, string Content, DateTime Timestamp)> chatMessages, int chatID, string name, string RSAPublicKeyClient2, string otherUsername, ChatWindow parentWindow)
        {
            InitializeComponent();

            this.parentWindow = parentWindow;
            this.chatID = chatID;
            this.chatMessages = chatMessages;
            this.name = name;
            this.RSAPublicKeyClient2 = RSAPublicKeyClient2;
            this.otherUsername = otherUsername;
            ShowMessages();
        }

        private void ShowMessages()
        {
            Name_LBL.Content = name;
            Username_LBL.Content = otherUsername;

            if (chatMessages == null || chatMessages.Count == 0)
                return;

            foreach (var message in chatMessages)
            {
                if (message.SenderUsername == GlobalDataUser.Username)
                    MessagesContainer.Children.Add(new SentMessageUserControl(message.Content, message.Timestamp));
                else
                    MessagesContainer.Children.Add(new ReceivedMessageUserControl(message.SenderUsername, message.Content, message.Timestamp));
            }

            ScrollViewerMessages.ScrollToEnd();
        }

        private async void ButtonSend_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string? messageText = Msg_TB.Text;
            if (messageText == null)
                return;
            if (GlobalDataUser.Username == null)
                return;

            byte[] bytes = MessageToServer.SentMessage(chatID: chatID, messageText: messageText, senderUsername: GlobalDataUser.Username, RSAPublicKeyClient2: RSAPublicKeyClient2);
            _ = Connect.SendMessageAsync(bytes);
            bool status = await MessageFromServer.GetMessageSuccess();

            if (status)
            {
                parentWindow.UpdateChatArea(chatID, name, RSAPublicKeyClient2, otherUsername);
            }
            return;
        }

        private void ButtonUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            parentWindow.UpdateChatArea(chatID, name, RSAPublicKeyClient2, otherUsername);
        }
    }
}