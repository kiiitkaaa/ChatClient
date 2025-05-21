using Coursework_KSIS.Classes;
using System.Windows.Controls;

namespace Coursework_KSIS.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ConnectInfoUserControl.xaml
    /// </summary>
    public partial class ConnectInfoUserControl : UserControl
    {
        public ConnectInfoUserControl()
        {
            InitializeComponent();
            SetLabel();
        }

        private void SetLabel()
        {
            IP_LBL.Content = Connect.IPCLIENT;
            Port_LBL.Content = Connect.PORT;
            IPS_LBL.Content = Connect.IPSERVER;
        }
    }
}
