using System.IO;
using System.Windows;
using AGG_Productions.LauncherData;

namespace AGG_Productions.LauncherFunctions
{
    class SelectScreen
    {
        public string GameLink;
        public SelectScreen(string GameName)
        {
            MainWindow.InstallGameName = $"{GameName}";
            if (File.Exists($"Games.json"))
            {
                MainWindow.InstallGameLink = Json.ReadJson(MainWindow.InstallGameName, "Games");
                GameLink = MainWindow.InstallGameLink;
            }
            else if (File.Exists($"{GameName}.json") && !File.Exists($"Games.json"))
            {
                string JsonLink = Json.ReadJsonLink("Link", GameName);
                MainWindow.InstallGameLink = Json.ReadAndCreate(GameName, JsonLink);
                GameLink = MainWindow.InstallGameLink;
            }
            MainWindow.NoGameObject.Visibility = Visibility.Collapsed;
            MainWindow.SelectGameObject.Visibility = Visibility.Collapsed;
            MainWindow.UpdateBoard.Visibility = Visibility.Visible;
            MainWindow.GameInstallObject.Visibility = Visibility.Visible;
            MainWindow.GameInstallObject.Content = $"Install {GameName}";

            if (Json.DataCheck(MainWindow.InstallGameName))
            {
                MainWindow.GameDir = Json.ReadJson(MainWindow.InstallGameName);
                MainWindow.VersionSelector.Visibility = Visibility.Visible;
                MainWindow.Play.Visibility = Visibility.Visible;
                MainWindow.GameDownloadBar.Visibility = Visibility.Visible;
                MainWindow.GameInstallObject.Visibility = Visibility.Collapsed;
                MainWindow.GameReInstallObject.Visibility = Visibility.Visible;
                VersionManager.VersionLink = GameLink;
                PlayButton._VersionManager = new VersionManager(this);
            }
            else
            {
                MainWindow.VersionSelector.Visibility = Visibility.Collapsed;
                MainWindow.Play.Visibility = Visibility.Collapsed;
                MainWindow.GameDownloadBar.Visibility = Visibility.Collapsed;
                MainWindow.GameInstallObject.Visibility = Visibility.Visible;
                MainWindow.GameInstallObject.IsEnabled = true;
                MainWindow.GameReInstallObject.Visibility = Visibility.Collapsed;
            }
        }
    }
}
