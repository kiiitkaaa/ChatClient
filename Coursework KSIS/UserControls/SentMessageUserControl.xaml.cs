using System.Windows.Controls;

namespace Coursework_KSIS.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SentMessageUserControl.xaml
    /// </summary>
    public partial class SentMessageUserControl : UserControl
    {
        private readonly string message;
        private readonly DateTime dateTime;

        public SentMessageUserControl(string message, DateTime dateTime)
        {
            InitializeComponent();

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
