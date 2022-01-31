using System;
using System.Net;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
#pragma warning disable IDE0052 // Remove unread private members
#pragma warning disable IDE0044 // Add readonly modifier
        private GameInstall gameInstall;
        private SelectScreen selectScreen;
#pragma warning restore IDE0052 // Remove unread private members
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
            catch (UriFormatException)
            {
                //TODO: Make it redownload the links without restarting the program
                MessageBox.Show("The link data is broken please restart the program");
            }
        }
        private void D_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            File.WriteAllText($"{MainWindow.InstallGameName}.json", e.Result.ToString());
            ObservableCollection<string> VersionstoDisplay = new ObservableCollection<string>();
            JObject jObj = (JObject)JsonConvert.DeserializeObject(File.ReadAllText($"{MainWindow.InstallGameName}.json"));
            //TODO: Change from one after it works
            for (int i = 0; i < jObj.Count; i++)
            {
                string VerJson = Json.ReadGameJsonVer("0.0.13.3", MainWindow.InstallGameName);
                string LinkJson = Json.ReadGameJsonLink("0.0.13.3", MainWindow.InstallGameName);
                VersionstoDisplay.Add(VerJson);
                //TODO: Convert this from the text file link grabber to a json object array
                VersionLinkPairs.Add(VerJson, LinkJson);

            }
            MainWindow.VersionSelector.ItemsSource = VersionstoDisplay;
            MainWindow.VersionSelector.Items.Refresh();
            MainWindow.Play.IsEnabled = true;
            MainWindow.VersionSelector.IsEnabled = true;
        }
    }
}
