using System.IO;
using System.Windows;
using AGG_Productions.LauncherData;

namespace AGG_Productions.LauncherFunctions
{
    class SelectScreen
    {
        public SelectScreen(string GameName, string GameLink)
        {
            MainWindow.InstallGameName = $"{GameName}";
            MainWindow.InstallGameLink = $"{GameLink}";
            MainWindow.NoGameObject.Visibility = Visibility.Collapsed;
            MainWindow.SelectGameObject.Visibility = Visibility.Collapsed;
            MainWindow.UpdateBoard.Visibility = Visibility.Visible;
            MainWindow.GameInstallObject.Visibility = Visibility.Visible;
            MainWindow.GameInstallObject.Content = $"Install {GameName}";

            if (File.Exists($"{GameName}Dir.txt"))
            {
                MainWindow.GameDir = File.ReadAllText($"{GameName}Dir.txt");
                MainWindow.VersionSelector.Visibility = Visibility.Visible;
                MainWindow.Play.Visibility = Visibility.Visible;
                MainWindow.GameDownloadBar.Visibility = Visibility.Visible;
                VersionManager.VersionLink = GameLink;
                PlayButton._VersionManager = new VersionManager(this);
                MainWindow.GameInstallObject.Visibility = Visibility.Collapsed;
                MainWindow.GameReInstallObject.Visibility = Visibility.Visible;
            }
        }
    }
}
