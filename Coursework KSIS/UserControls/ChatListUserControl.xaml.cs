using Coursework_KSIS.Classes;
using Coursework_KSIS.Windows;
using System.Windows.Controls;

namespace Coursework_KSIS.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ChatListUserControl.xaml
    /// </summary>
    public partial class ChatListUserControl : UserControl
    {
        public List<ChatPreview>? chatPreviews;
        public ChatWindow ParentWindow { get; set; }

        public ChatListUserControl(List<ChatPreview>? chatPreviews, ChatWindow ParentWindow)
        {
            InitializeComponent();

            this.chatPreviews = chatPreviews;
            this.ParentWindow = ParentWindow;
            SetChats();
        }

        private void SetChats()
        {
            if (chatPreviews == null)
                return;

            foreach (var item in chatPreviews)
            {
                ChatsContainer.Children.Add(
                    new UserChatMiniUserControl(item.OtherName, item.LastMessage, item.ChatId, item.OtherRSAPublicKey, item.OtherUsername)
                    {
                        ParentWindow = this.ParentWindow
                    });
            }
        }
    }
}
