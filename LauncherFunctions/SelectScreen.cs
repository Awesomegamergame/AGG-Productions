using System.IO;
using System.Windows;
using static System.Environment;
using static AGG_Productions.MainWindow;
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
            InstallGameName = $"{GameName}";
            if (File.Exists($@"{CurrentDirectory}\Cache\Games.json"))
            {
                InstallGameLink = Json.ReadJson(InstallGameName, "Games");
                GameLink = InstallGameLink;
            }
            else if (File.Exists($@"{CurrentDirectory}\Cache\Games\{GameName}.json") && !File.Exists($@"{CurrentDirectory}\Cache\Games.json"))
            {
                string JsonLink = Json.ReadJsonLink("Link", GameName);
                InstallGameLink = Json.ReadAndCreate(GameName, JsonLink);
                GameLink = InstallGameLink;
            }
            AGGWindow.NoGame.Visibility = Visibility.Collapsed;
            AGGWindow.SelectGame.Visibility = Visibility.Collapsed;
            AGGWindow.UpdateBoard.Visibility = Visibility.Visible;
            AGGWindow.Game_Install.Visibility = Visibility.Visible;
            AGGWindow.Game_Install.Content = $"Install {GameName}";

            if (Json.DataCheck(InstallGameName))
            {
                GameDir = Json.ReadJson(InstallGameName);
                VersionSelector.Visibility = Visibility.Visible;
                AGGWindow.PlayButtonGUI.Visibility = Visibility.Visible;
                AGGWindow.GameDownload.Visibility = Visibility.Visible;
                AGGWindow.Game_Install.Visibility = Visibility.Collapsed;
                AGGWindow.Game_ReInstall.Visibility = Visibility.Visible;
                VersionManager.VersionLink = GameLink;
                PlayButton._VersionManager = new VersionManager(this);
                if (File.Exists($"{GameName}Dir.txt"))
                    File.Delete($"{GameName}Dir.txt");
            }
            else
            {
                VersionSelector.Visibility = Visibility.Collapsed;
                AGGWindow.PlayButtonGUI.Visibility = Visibility.Collapsed;
                AGGWindow.GameDownload.Visibility = Visibility.Collapsed;
                AGGWindow.Game_Install.Visibility = Visibility.Visible;
                AGGWindow.Game_Install.IsEnabled = true;
                AGGWindow.Game_ReInstall.Visibility = Visibility.Collapsed;
                if (File.Exists($"{GameName}Dir.txt"))
                    File.Delete($"{GameName}Dir.txt");
            }
        }
    }
}
