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
        #region Update Screen Variables Dont Edit
        public static Button Yes_Button;
        public static Button No_Button;
        public static Image UpdateScreen_Image;
        public static Label UpdateText1_Label;
        public static Label UpdateText2_Label;
        public static ProgressBar GameDownloadBar;
        public static ProgressBar UpdateDownloadBar;
        #endregion
        public MainWindow()
        {
            CheckInternet.CheckInternetState();
            InitializeComponent();
            if (CheckInternet.IsOnline)
            {
                Updater.LauncherUpdate();
            }
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
                GameDownloadBar.Visibility = Visibility.Visible;
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
            if (AdminDirCheck.FileDialogClosed)
            {
                AdminDirCheck.FileDialogClosed = false;
                return;
            }
            GameDir = File.ReadAllText("ChaoticDir.txt");
            Chaotic_Install.Visibility = Visibility.Collapsed;
            VersionBox2.Visibility = Visibility.Visible;
            PlayButton.Visibility = Visibility.Visible;
            GameDownloadBar.Visibility = Visibility.Visible;
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

        #region Update Screen Buttons Dont Edit
        private void No_Click(object sender, RoutedEventArgs e)
        {
            UpdateScreen_Image.Visibility = Visibility.Collapsed;
            Yes_Button.Visibility = Visibility.Collapsed;
            No_Button.Visibility = Visibility.Collapsed;
            UpdateText1_Label.Visibility = Visibility.Collapsed;
            UpdateText2_Label.Visibility = Visibility.Collapsed;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Yes_Button.Visibility = Visibility.Collapsed;
            No_Button.Visibility = Visibility.Collapsed;
            UpdateDownloadBar.Visibility = Visibility.Visible;
            if (Updater.VersionDetector == 1)
            {
                Updater.UpdaterVersion();
                Updater.VersionDetector = 0;
            }
            else if(Updater.VersionDetector == 2)
            {
                Updater.UpdaterVersion();
                Updater.VersionDetector = 0;
            }
        }

        private void Yes_Initialized(object sender, EventArgs e)
        {
            Yes_Button = (Button)sender;
        }

        private void No_Initialized(object sender, EventArgs e)
        {
            No_Button = (Button)sender;
        }

        private void UpdateText1_Initialized(object sender, EventArgs e)
        {
            UpdateText1_Label = (Label)sender;
        }

        private void UpdateText2_Initialized(object sender, EventArgs e)
        {
            UpdateText2_Label = (Label)sender;
        }

        private void UpdateScreen_Initialized(object sender, EventArgs e)
        {
            UpdateScreen_Image = (Image)sender;
        }
        private void ProgressBar_Initialized(object sender, EventArgs e)
        {
            GameDownloadBar = (ProgressBar)sender;
        }
        private void UpdateBar_Initialized(object sender, EventArgs e)
        {
            UpdateDownloadBar = (ProgressBar)sender;
        }
        #endregion
    }
}