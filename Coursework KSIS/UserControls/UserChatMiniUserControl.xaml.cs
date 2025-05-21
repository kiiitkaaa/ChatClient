using Coursework_KSIS.Classes;
using Coursework_KSIS.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coursework_KSIS.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UserChatMiniUserControl.xaml
    /// </summary>
    public partial class UserChatMiniUserControl : UserControl
    {
        private readonly string name;
        private readonly string lastMsg;
        private readonly int chatID;
        private readonly string otherUsername;
        private readonly string RSAPublicKeyClient2;

        public required ChatWindow ParentWindow { get; set; }

        public UserChatMiniUserControl(string name, string lastMsg, int chatID, string RSAPublicKeyClient2, string otherUsername)
        {
            InitializeComponent();

            this.otherUsername = otherUsername;
            this.RSAPublicKeyClient2 = RSAPublicKeyClient2;
            this.name = name;
            this.lastMsg = lastMsg;
            this.chatID = chatID;
            SetLBL();
        }

        private void SetLBL()
        {
            Name_LBL.Content = name;
            LastMsg_LBL.Content = lastMsg;
        }

        private async void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            byte[] bytes = MessageToServer.CurrentChatMessage(chatID);
            _ = Connect.SendMessageAsync(bytes);
            var messages = await MessageFromServer.GetMessagesFromChat();

            ParentWindow?.OpenChat(messages, chatID, name, RSAPublicKeyClient2, otherUsername);
        }
    }
}
