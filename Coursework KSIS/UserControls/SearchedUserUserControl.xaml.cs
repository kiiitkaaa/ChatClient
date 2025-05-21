using Coursework_KSIS.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coursework_KSIS.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SearchedUserUserControl.xaml
    /// </summary>
    public partial class SearchedUserUserControl : UserControl
    {
        public required ChatWindow ParentWindow { get; set; }

        private readonly string name;
        private readonly string username;
        private readonly int ID;
        private readonly string phoneNumber;

        public SearchedUserUserControl(string username, string name, int ID, string phoneNumber)
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
            Username_LBL.Content = username;
            Name_LBL.Content = name;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ParentWindow.ShowUserAction(name: name, username: username, ID: ID, phoneNumber: phoneNumber);
        }
    }
}
