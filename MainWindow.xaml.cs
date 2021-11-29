using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using AGG_Productions.LauncherData;
using AGG_Productions.LauncherUpdater;

namespace AGG_Productions
{
    public partial class MainWindow : Window
    {
        public static string VersionToDownload;
        public static string GameDir;
        public static Button button;
        public static WebBrowser UpdateBoard;
        public static Button Play;
        public static ComboBox VersionSelector;
        public static ComboBox VersionBox;
        public MainWindow()
        {
            CheckInternet.CheckInternetState();
            if (CheckInternet.IsOnline)
            {
                Updater.LauncherUpdate();
            }
            InitializeComponent();
        }
        private void Chaotic_Click(object sender, RoutedEventArgs e)
        {
            NoGame.Visibility = Visibility.Collapsed;
            SelectGame.Visibility = Visibility.Collapsed;
            Chaotic_Notes.Visibility = Visibility.Visible;
            Chaotic_Install.Visibility = Visibility.Visible;
            Chaotic.IsEnabled = false;

            if (File.Exists("ChaoticDir.txt"))
            {
                GameDir = File.ReadAllText("ChaoticDir.txt");
                VersionBox2.Visibility = Visibility.Visible;
                PlayButton.Visibility = Visibility.Visible;
                VersionManager.VersionLink = UpdateBoardLinks.ChaoticVersionLink;
                PlayButton2._VersionManager = new VersionManager(this);
                Chaotic_Install.Visibility = Visibility.Collapsed;
            }
        }
        private void Chaotic_Install_Click(object sender, RoutedEventArgs e)
        {
            Chaotic_Install.IsEnabled = false;
            button = Chaotic_Install;
            AdminDirCheck.InstallDir("Chaotic");
            GameDir = File.ReadAllText("ChaoticDir.txt");
            Chaotic_Install.Visibility = Visibility.Collapsed;
            VersionBox2.Visibility = Visibility.Visible;
            PlayButton.Visibility = Visibility.Visible;
            VersionManager.VersionLink = UpdateBoardLinks.ChaoticVersionLink;
            PlayButton2._VersionManager = new VersionManager(this);
        }
        private void Chaotic_Notes_Initialized(object sender, EventArgs e)
        {
            UpdateBoard = (WebBrowser)sender;
            UpdateBoards.DownloadBoards("Chaotic", UpdateBoardLinks.ChaoticBoardLink);
        }
        private void Chaotic_Version_Initialized(object sender, EventArgs e)
        {
            VersionSelector = (ComboBox)sender;
        }
        private void PlayButton_Initialized(object sender, EventArgs e)
        {
            Play = (Button)sender;
        }
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton2.Start("Chaotic");
        }
        private void VersionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VersionToDownload = VersionSelector.SelectedItem.ToString();
        }
    }
}