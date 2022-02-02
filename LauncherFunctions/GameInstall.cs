using System.IO;
using System.Windows;
using AGG_Productions.LauncherData;

namespace AGG_Productions.LauncherFunctions
{
    class GameInstall
    {
        public string GameLink;
        public GameInstall()
        {
            MainWindow.GameInstallObject.IsEnabled = false;
            MainWindow.button = MainWindow.GameInstallObject;
            AdminDirCheck.InstallDir(MainWindow.InstallGameName);
            if (AdminDirCheck.FileDialogClosed)
            {
                AdminDirCheck.FileDialogClosed = false;
                return;
            }
            MainWindow.GameDir = Json.ReadJson(MainWindow.InstallGameName);
            MainWindow.GameInstallObject.Visibility = Visibility.Collapsed;
            MainWindow.VersionSelector.Visibility = Visibility.Visible;
            MainWindow.Play.Visibility = Visibility.Visible;
            MainWindow.GameDownloadBar.Visibility = Visibility.Visible;
            MainWindow.GameReInstallObject.Visibility = Visibility.Visible;
            if (!File.Exists($"{MainWindow.InstallGameName}.json") && !File.Exists($"Games.json"))
            {
                MainWindow.InstallGameLink = null;
            }
            VersionManager.VersionLink = MainWindow.InstallGameLink;
            PlayButton._VersionManager = new VersionManager(this);
        }
    }
}
