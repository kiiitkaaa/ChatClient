using System.Windows.Controls;

namespace Coursework_KSIS.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ReceivedMessageUserControl.xaml
    /// </summary>
    public partial class ReceivedMessageUserControl : UserControl
    {
        private readonly string sender;
        private readonly string message;
        private readonly DateTime dateTime;

        public ReceivedMessageUserControl(string sender, string message, DateTime dateTime)
        {
            InitializeComponent();

            this.sender = sender;
            this.dateTime = dateTime;
            this.message = message;
            SetLBL();
        }

        private void SetLBL()
        {
            Msg_LBL.Content = message;
            Date_LBL.Content = dateTime.ToString();
        }
    }
}
