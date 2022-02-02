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
        public static string VersionToDownload, GameDir;
        public static Button button;
        public static WebBrowser UpdateBoard;
        public static Button Play;
        public static ComboBox VersionSelector;
        #region GameInstallVariables
        public static Button GameInstallObject;
        public static TextBox NoGameObject;
        public static TextBox SelectGameObject;
        public static Button GameReInstallObject;
        public static string InstallGameName = "";
        public static string InstallGameLink = "";
        #endregion
        #region Update Screen Variables Dont Edit
        public static Button Yes_Button;
        public static Button No_Button;
        public static Image UpdateScreen_Image;
        public static Label UpdateText1_Label;
        public static Label UpdateText2_Label;
        public static ProgressBar GameDownloadBar;
        public static ProgressBar UpdateDownloadBar;
        #endregion
        #region Repair Screen Dont Edit
        public static ProgressBar RepairBarObject;
        public static Image RepairScreenObject;
        public static Label RepairTextObject, RepairBodyObject;
        #endregion
        #region Launcher Version
        public static Label LocalVersionObject, LocalVersionNumberObject;
        public static Label OnlineVersionObject, OnlineVersionNumberObject;
        #endregion
        public MainWindow()
        {
            UpgradeLauncher.OldLauncherCheck();
            CheckInternet.CheckInternetState();
            InitializeComponent();
            OnlineFunctions.UpdateFunctions();
        }
        private void Chaotic_Click(object sender, RoutedEventArgs e)
        {
            _ = new ActivateBoard("Chaotic");
            _ = new SelectScreen("Chaotic");
            Chaotic.IsEnabled = false;
            EastlowsHS.IsEnabled = true;
        }
        private void EastlowsHS_Click(object sender, RoutedEventArgs e)
        {
            _ = new ActivateBoard("EastlowsHS");
            _ = new SelectScreen("EastlowsHS");
            EastlowsHS.IsEnabled = false;
            Chaotic.IsEnabled = true;
        }
        private void Game_Install_Click(object sender, RoutedEventArgs e)
        {
            _ = new GameInstall();
        }
        private void Game_ReInstall_Click(object sender, RoutedEventArgs e)
        {
            _ = new GameReinstall();
        }
        private void Game_Notes_Initialized(object sender, EventArgs e)
        {
            if (!Directory.Exists("Cache"))
                Directory.CreateDirectory("Cache");
            UpdateBoard = (WebBrowser)sender;
            UpdateBoards.SetupBoards();
        }
        private void Chaotic_Version_Initialized(object sender, EventArgs e)
        {
            VersionSelector = (ComboBox)sender;
            VersionSelector.MaxDropDownHeight = VersionSelector.MaxHeight = 110;
        }
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.Start(InstallGameName);
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
            LocalVersionObject.Visibility = Visibility.Collapsed;
            LocalVersionNumberObject.Visibility = Visibility.Collapsed;
            OnlineVersionNumberObject.Visibility = Visibility.Collapsed;
            OnlineVersionObject.Visibility = Visibility.Collapsed;
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

        #region Repair Screen Dont Edit
        private void RepairScreen_Initialized(object sender, EventArgs e)
        {
            RepairScreenObject = (Image)sender;
        }

        private void RepairBar_Initialized(object sender, EventArgs e)
        {
            RepairBarObject = (ProgressBar)sender;
        }

        private void RepairText_Initialized(object sender, EventArgs e)
        {
            RepairTextObject = (Label)sender;
        }

        private void RepairBodyText_Initialized(object sender, EventArgs e)
        {
            RepairBodyObject = (Label)sender;
        }
        #endregion

        #region Launcher Version
        private void LocalVersionNumber_Initialized(object sender, EventArgs e)
        {
            LocalVersionNumberObject = (Label)sender;
        }

        private void LocalVersion_Initialized(object sender, EventArgs e)
        {
            LocalVersionObject = (Label)sender;
        }

        private void OnlineVersionNumber_Initialized(object sender, EventArgs e)
        {
            OnlineVersionNumberObject = (Label)sender;
        }

        private void OnlineVersion_Initialized(object sender, EventArgs e)
        {
            OnlineVersionObject = (Label)sender;
        }
        #endregion

        #region All UI Stuff
        private void Game_Install_Initialized(object sender, EventArgs e)
        {
            GameInstallObject = (Button)sender;
        }
        private void NoGame_Initialized(object sender, EventArgs e)
        {
            NoGameObject = (TextBox)sender;
        }
        private void SelectGame_Initialized(object sender, EventArgs e)
        {
            SelectGameObject = (TextBox)sender;
        }
        private void Game_ReInstall_Initialized(object sender, EventArgs e)
        {
            GameReInstallObject = (Button)sender;
        }
        private void PlayButton_Initialized(object sender, EventArgs e)
        {
            Play = (Button)sender;
        }
        #endregion
    }
}