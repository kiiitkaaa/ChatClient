using Coursework_KSIS.Classes;
using System.Windows.Controls;

namespace Coursework_KSIS.UserControls
{
    /// <summary>
    /// Логика взаимодействия для AccauntSettingsUserControl.xaml
    /// </summary>
    public partial class AccauntSettingsUserControl : UserControl
    {
        public AccauntSettingsUserControl()
        {
            InitializeComponent();
            ShowInfo();
        }

        private void ShowInfo()
        {
            Name_LBL.Content = GlobalDataUser.PersonalName;
            UserameLBL.Content = GlobalDataUser.Username;
            Email_LBL.Content = GlobalDataUser.Email;
            Number_LBL.Content = GlobalDataUser.PhoneNumber;
        }
    }
}
