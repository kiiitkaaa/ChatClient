using Coursework_KSIS.Windows;
using System.Windows;
using System.Windows.Controls;

namespace Coursework_KSIS.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SettingsListUserControl.xaml
    /// </summary>
    public partial class SettingsListUserControl : UserControl
    {
        public required ChatWindow ParentWindow { get; set; }


        public SettingsListUserControl()
        {
            InitializeComponent();
        }

        private void ButtonAccaunt_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow?.ShowAccauntInfo();
        }

        private void ButtonServer_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow?.ShowConnectInfo();
        }

        private void ButtonAboutProgram_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow?.ShowProgramInfo();
        }

        private void ButtonAboutAuthor_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow?.ShowAuthorInfo();
        }
    }
}
