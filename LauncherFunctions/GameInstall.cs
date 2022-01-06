using System.IO;
using System.Windows;
using AGG_Productions.LauncherData;

namespace AGG_Productions.LauncherFunctions
{
    class GameInstall
    {
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
            MainWindow.GameDir = File.ReadAllText($"{MainWindow.InstallGameName}Dir.txt");
            MainWindow.GameInstallObject.Visibility = Visibility.Collapsed;
            MainWindow.VersionSelector.Visibility = Visibility.Visible;
            MainWindow.Play.Visibility = Visibility.Visible;
            MainWindow.GameDownloadBar.Visibility = Visibility.Visible;
            MainWindow.GameReInstallObject.Visibility = Visibility.Visible;
            VersionManager.VersionLink = MainWindow.InstallGameLink;
            PlayButton2._VersionManager = new VersionManager(this);
        }
    }
}
