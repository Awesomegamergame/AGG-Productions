using System.IO;
using System.Windows;
using static System.Environment;
using static AGG_Productions.MainWindow;
using AGG_Productions.LauncherData;

namespace AGG_Productions.LauncherFunctions
{
    class GameInstall
    {
        public GameInstall()
        {
            AGGWindow.Game_Install.IsEnabled = false;
            button = AGGWindow.Game_Install;
            AdminDirCheck.InstallDir(InstallGameName);
            if (AdminDirCheck.FileDialogClosed)
            {
                AdminDirCheck.FileDialogClosed = false;
                return;
            }
            GameDir = Json.ReadJson(InstallGameName);
            AGGWindow.Game_Install.Visibility = Visibility.Collapsed;
            VersionSelector.Visibility = Visibility.Visible;
            AGGWindow.PlayButtonGUI.Visibility = Visibility.Visible;
            AGGWindow.GameDownload.Visibility = Visibility.Visible;
            AGGWindow.Game_ReInstall.Visibility = Visibility.Visible;
            if (!File.Exists($@"{CurrentDirectory}\Cache\Games\{InstallGameName}.json") && !File.Exists($@"{CurrentDirectory}\Cache\Games.json"))
            {
                InstallGameLink = null;
            }
            VersionManager.VersionLink = InstallGameLink;
            PlayButton._VersionManager = new VersionManager(this);
        }
        
    }
    class GameReinstall
    {
        public GameReinstall()
        {
            AGGWindow.Game_ReInstall.IsEnabled = false;
            button = AGGWindow.Game_ReInstall;
            AdminDirCheck.InstallDir(InstallGameName);
            AGGWindow.Game_ReInstall.IsEnabled = true;
            GameDir = Json.ReadJson(InstallGameName);
        }
    }
}
