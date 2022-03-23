using System;
using System.Net;
using System.IO;
using System.Windows;
using static System.Environment;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static AGG_Productions.MainWindow;
using AGG_Productions.LauncherFunctions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AGG_Productions.LauncherData
{
    class VersionManager
    {
        public static string VersionLink;
        public Dictionary<string, string> VersionLinkPairs;
        #region Disable Intellisense Messages
#pragma warning disable IDE0044 // Add readonly modifier
        private GameInstall gameInstall;
        private SelectScreen selectScreen;
#pragma warning restore IDE0044 // Add readonly modifier
        #endregion
        public VersionManager(GameInstall gameInstall)
        {
            this.gameInstall = gameInstall;
            VersionLinkPairs = new Dictionary<string, string>();
            Init();
        }
        public VersionManager(SelectScreen selectScreen)
        {
            this.selectScreen = selectScreen;
            VersionLinkPairs = new Dictionary<string, string>();
            Init();
        }

        private void Init()
        {
            WebClient d = new WebClient();
            d.DownloadStringCompleted += D_DownloadStringCompleted;
            try
            {
                d.DownloadStringAsync(new Uri(VersionLink));
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Can't Download Game Versions: No Internet");
            }
            catch (UriFormatException)
            {
                MessageBox.Show("Something is wrong with the cache files: Restart the program");
            }
        }
        private void D_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if(CheckInternet.IsOnline)
                File.WriteAllText($@"{CurrentDirectory}\Cache\Games\{InstallGameName}.json", e.Result.ToString());
            if (!File.Exists($@"{CurrentDirectory}\Cache\Games\{InstallGameName}.json"))
                return;
            ObservableCollection<string> VersionstoDisplay = new ObservableCollection<string>();
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText($@"{CurrentDirectory}\Cache\Games\{InstallGameName}.json"));
            dynamic json = obj.Game;
            foreach (JProperty Version in json)
            {
                string VerJson = Json.ReadGameJson(Version.Name, "version", InstallGameName, "Game");
                string LinkJson = Json.ReadGameJson(Version.Name, "link", InstallGameName, "Game");
                VersionstoDisplay.Add(VerJson);
                VersionLinkPairs.Add(VerJson, LinkJson);
            }
            VersionSelector.ItemsSource = VersionstoDisplay;
            VersionSelector.Items.Refresh();
            AGGWindow.PlayButtonGUI.IsEnabled = true;
            VersionSelector.IsEnabled = true;
        }
    }
}
