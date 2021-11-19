using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AGG_Productions
{
    public partial class MainWindow : Window
    {
        #region Variables
        
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Chaotic_Click(object sender, RoutedEventArgs e)
        {
            NoGame.Visibility = Visibility.Collapsed;
            SelectGame.Visibility = Visibility.Collapsed;
            Chaotic_Notes.Visibility = Visibility.Visible;
            Chaotic_Install.Visibility = Visibility.Visible;
            Chaotic.IsEnabled = false;
        }

        private void Chaotic_Install_Click(object sender, RoutedEventArgs e)
        {
            AdminDirCheck.InstallDir("Chaotic");
        }

        private void Chaotic_Notes_Initialized(object sender, EventArgs e)
        {

        }
    }
}
