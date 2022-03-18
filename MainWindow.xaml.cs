using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using AGG_Productions.LauncherData;
using AGG_Productions.LauncherUpdater;
using AGG_Productions.LauncherFunctions;

namespace AGG_Productions
{
    public partial class MainWindow : Window
    {
        public static MainWindow AGGWindow;

        public bool SettingsEnabled = false;
        public static string GameDir;
        public string VersionToDownload;
        public static bool HTML;
        public static Button button;
        public static ComboBox VersionSelector;
        public static string InstallGameName = "";
        public static string InstallGameLink = "";
        public MainWindow()
        {
            UpgradeLauncher.OldLauncherCheck();
            CheckInternet.CheckInternetState();
            InitializeComponent();
            AGGWindow = this;
            OnlineFunctions.UpdateFunctions();
        }
        public static void Game_Click(object sender, RoutedEventArgs e)
        {
            string GameName = (sender as Button).Name;
            HTML = (bool)(sender as Button).Tag;
            _ = new ActivateBoard(GameName);
            _ = new SelectScreen(GameName);
        }
        private void Game_Install_Click(object sender, RoutedEventArgs e)
        {
            _ = new GameInstall();
        }
        private void Game_ReInstall_Click(object sender, RoutedEventArgs e)
        {
            _ = new GameReinstall();
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsEnabled = SettingsM.Menu(SettingsEnabled);
        }
        private void Version_Initialized(object sender, EventArgs e)
        {
            VersionSelector = (ComboBox)sender;
            VersionSelector.MaxDropDownHeight = VersionSelector.MaxHeight = 110;
        }
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var Play = new PlayButton();
            Play.Start(InstallGameName, HTML, VersionToDownload);
        }
        private void VersionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VersionSelector.SelectedItem == null)
                VersionToDownload = null;
            else
                VersionToDownload = VersionSelector.SelectedItem.ToString();
        }
        #region Update Screen Buttons Dont Edit
        private void No_Click(object sender, RoutedEventArgs e)
        {
            UpdateScreen.Visibility = Visibility.Collapsed;
            Yes.Visibility = Visibility.Collapsed;
            No.Visibility = Visibility.Collapsed;
            UpdateText1.Visibility = Visibility.Collapsed;
            UpdateText2.Visibility = Visibility.Collapsed;
            LocalVersion.Visibility = Visibility.Collapsed;
            LocalVersionNumber.Visibility = Visibility.Collapsed;
            OnlineVersionNumber.Visibility = Visibility.Collapsed;
            OnlineVersion.Visibility = Visibility.Collapsed;
        }

        private void AGGB_Click(object sender, RoutedEventArgs e)
        {
            CheckInternet.CheckInternetState();
            AGGB.IsEnabled = false;
            if (CheckInternet.IsOnline)
            {
                if (Updater.VersionDetector == 1)
                {
                    Updater.UpdaterVersion();
                    Updater.VersionDetector = 0;
                }
                else if (Updater.VersionDetector == 2)
                {
                    Updater.UpdaterVersion();
                    Updater.VersionDetector = 0;
                }
            }
            else
            {
                MessageBox.Show("No Internet Connection");
                AGGB.IsEnabled = true;
            }
        }

        private void HTMLUB_Click(object sender, RoutedEventArgs e)
        {
            HTMLUB.IsEnabled = false;
            if (File.Exists($@"{Environment.CurrentDirectory}\Plugins\HTMLPlayer\HTMLPlayer.exe"))
            {
                UpgradeLauncher.DeleteDirectory($@"{Environment.CurrentDirectory}\Plugins\HTMLPlayer");
                HTMLVer.Content = $"Local Version: ";
            }
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Yes.Visibility = Visibility.Collapsed;
            No.Visibility = Visibility.Collapsed;
            UpdateProgress.Visibility = Visibility.Visible;
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
        #endregion
    }
}