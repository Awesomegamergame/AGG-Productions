using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using AGG_Productions.LauncherData;
using AGG_Productions.LauncherUpdater;
using AGG_Productions.GameLinks;
using AGG_Productions.Repair;
using AGG_Productions.LauncherUpgrade;
using AGG_Productions.GameFunctions;

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
        public static Button GameInstallObject;
        public static TextBox NoGameObject;
        public static TextBox SelectGameObject;
        public static Button GameReInstallObject;
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
        public static Label RepairTextObject;
        public static Label RepairBodyObject;
        #endregion
        #region Launcher Version
        public static Label LocalVersionObject;
        public static Label LocalVersionNumberObject;
        public static Label OnlineVersionObject;
        public static Label OnlineVersionNumberObject;
        #endregion
        public MainWindow()
        {
            if (Directory.Exists(UpgradeLauncher.ChaoticLauncherFolder) || Directory.Exists(UpgradeLauncher.ChaoticDevLauncherFolder))
            {
                UpgradeLauncher.DeleteOld();
            }
            CheckInternet.CheckInternetState();
            InitializeComponent();
            if (CheckInternet.IsOnline)
            {
                CheckFiles.CheckForFiles();
                if (CheckFiles.FilesCheckPassed)
                {
                    Updater.LauncherUpdate();
                }
                else
                {
                    RepairScreenObject.Visibility = Visibility.Visible;
                    RepairBarObject.Visibility = Visibility.Visible;
                    RepairTextObject.Visibility = Visibility.Visible;
                    RepairBodyObject.Visibility = Visibility.Visible;
                    Updater.LauncherUpdate();
                }
            }
            else
            {
                CheckFiles.CheckForFilesNoInternet();
                if (CheckFiles.FilesCheckPassedNo == false)
                {
                    RepairScreenObject.Visibility = Visibility.Visible;
                    RepairTextObject.Visibility = Visibility.Visible;
                    RepairBodyObject.Content = "Please Connect To The Internet And Restart The Launcher To Repair It";
                    RepairBodyObject.Visibility = Visibility.Visible;
                }
            }
        }
        private void Chaotic_Click(object sender, RoutedEventArgs e)
        {
            ActivateBoard activateBoard = new ActivateBoard("Chaotic");
            SelectScreen selectScreen = new SelectScreen("Chaotic", Links.ChaoticLink);
            Chaotic.IsEnabled = false;
            EastlowsHS.IsEnabled = true;
        }
        private void EastlowsHS_Click(object sender, RoutedEventArgs e)
        {
            ActivateBoard activateBoard = new ActivateBoard("EastlowsHS");
            SelectScreen selectScreen = new SelectScreen("EastlowsHS", Links.ChaoticLink);
            EastlowsHS.IsEnabled = false;
            Chaotic.IsEnabled = true;
        }
        private void Game_Install_Click(object sender, RoutedEventArgs e)
        {
            GameInstall gameInstall = new GameInstall("Chaotic", Links.ChaoticLink);
        }
        private void Game_ReInstall_Click(object sender, RoutedEventArgs e)
        {
            Game_ReInstall.IsEnabled = false;
            button = Game_ReInstall;
            AdminDirCheck.InstallDir("Chaotic");
            GameDir = File.ReadAllText("ChaoticDir.txt");
        }
        private void Game_Notes_Initialized(object sender, EventArgs e)
        {
            UpdateBoard = (WebBrowser)sender;
            UpdateBoards.DownloadBoards("Chaotic", UpdateBoardLinks.ChaoticBoardLink);
            UpdateBoards.DownloadBoards("EastlowsHS", UpdateBoardLinks.EastlowsHS);
        }
        private void Chaotic_Version_Initialized(object sender, EventArgs e)
        {
            VersionSelector = (ComboBox)sender;
            VersionSelector.MaxDropDownHeight = VersionSelector.MaxHeight = 110;
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
        #endregion
    }
}