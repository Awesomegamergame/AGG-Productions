using System.IO;
using System.Windows;
using static System.Environment;
using AGG_Productions.LauncherData;

namespace AGG_Productions.LauncherFunctions
{
    class SelectScreen
    {
        public string GameLink;
        public SelectScreen(string GameName)
        {
            if (!Directory.Exists($@"{CurrentDirectory}\Cache\Games"))
            {
                Directory.CreateDirectory($@"{CurrentDirectory}\Cache\Games");
            }
            MainWindow.InstallGameName = $"{GameName}";
            if (File.Exists($@"{CurrentDirectory}\Cache\Games.json"))
            {
                MainWindow.InstallGameLink = Json.ReadJson(MainWindow.InstallGameName, "Games");
                GameLink = MainWindow.InstallGameLink;
            }
            else if (File.Exists($@"{CurrentDirectory}\Cache\Games\{GameName}.json") && !File.Exists($@"{CurrentDirectory}\Cache\Games.json"))
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
                if (File.Exists($"{GameName}Dir.txt"))
                    File.Delete($"{GameName}Dir.txt");
            }
            else
            {
                MainWindow.VersionSelector.Visibility = Visibility.Collapsed;
                MainWindow.Play.Visibility = Visibility.Collapsed;
                MainWindow.GameDownloadBar.Visibility = Visibility.Collapsed;
                MainWindow.GameInstallObject.Visibility = Visibility.Visible;
                MainWindow.GameInstallObject.IsEnabled = true;
                MainWindow.GameReInstallObject.Visibility = Visibility.Collapsed;
                if (File.Exists($"{GameName}Dir.txt"))
                    File.Delete($"{GameName}Dir.txt");
            }
        }
    }
}
