using System.IO;
using System.Windows;
using static System.Environment;
using static AGG_Productions.MainWindow;
using AGG_Productions.LauncherData;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
            if (File.Exists($@"{CurrentDirectory}\Cache\ButtonData.json"))
            {
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText($@"{CurrentDirectory}\Cache\ButtonData.json"));
                dynamic json = obj.Games;
                foreach (JProperty Names in json)
                {
                    string Name = Json.ReadGameVerJson(Names.Name, "name", "ButtonData", "Games");
                    if (Name.Equals(GameName))
                    {
                        InstallGameLink = Json.ReadGameVerJson(Names.Name, "game", "ButtonData", "Games");
                        GameLink = InstallGameLink;
                    }
                }
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
            }
            else
            {
                VersionSelector.Visibility = Visibility.Collapsed;
                AGGWindow.PlayButtonGUI.Visibility = Visibility.Collapsed;
                AGGWindow.GameDownload.Visibility = Visibility.Collapsed;
                AGGWindow.Game_Install.Visibility = Visibility.Visible;
                AGGWindow.Game_Install.IsEnabled = true;
                AGGWindow.Game_ReInstall.Visibility = Visibility.Collapsed;
            }
        }
    }
}
