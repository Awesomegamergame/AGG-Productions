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
        public WebBrowser UpdateBoard;
        public string UpdateBoardDir = $@"{Environment.CurrentDirectory}\UpdateBoards";
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
            Chaotic_Install.IsEnabled = false;
            button = Chaotic_Install;
            AdminDirCheck.InstallDir("Chaotic");
        }

        private void Chaotic_Notes_Initialized(object sender, EventArgs e)
        {
            UpdateBoard = (WebBrowser)sender;

            WebClient c = new WebClient();
            A:
            if (Directory.Exists(UpdateBoardDir))
            {
                c.DownloadFileCompleted += C_DownloadFileCompleted;
                c.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/awesomegamergame/AGG-Productions/master/UpdateBoards/ChaoticUpdates.html"), $@"{Environment.CurrentDirectory}\UpdateBoards\ChaoticUpdates.html");
                UpdateBoard.Source = new Uri($@"{Environment.CurrentDirectory}\UpdateBoards\ChaoticUpdates.html");
            }
            else
            {
                Directory.CreateDirectory(UpdateBoardDir);
                goto A;
            }
        }

        private void C_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            UpdateBoard.Source = new Uri($@"{Environment.CurrentDirectory}\UpdateBoards\ChaoticUpdates.html");
        }
    }
}