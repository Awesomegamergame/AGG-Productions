using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        public static Button button;
        public static WebBrowser UpdateBoard;
        #endregion

        public MainWindow()
        {
            CheckInternet.CheckInternetState();
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
            Chaotic_Install.IsEnabled = false;
            button = Chaotic_Install;
            AdminDirCheck.InstallDir("Chaotic");
        }

        private void Chaotic_Notes_Initialized(object sender, EventArgs e)
        {
            UpdateBoard = (WebBrowser)sender;
            UpdateBoards.DownloadBoards("Chaotic", UpdateBoardLinks.ChaoticBoardLink);
        }
    }
}