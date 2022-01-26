using System;
using System.Net;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AGG_Productions.LauncherFunctions;

namespace AGG_Productions.LauncherData
{
    class VersionManager
    {
        public static string VersionLink;
        public Dictionary<string, string> VersionLinkPairs;
        private GameInstall gameInstall;
        private SelectScreen selectScreen;
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
            d.DownloadStringAsync(new Uri(VersionLink));
        }
        private void D_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string temp = e.Result;
            string[] VersionLinks = temp.Split('\n');
            ObservableCollection<string> VersionstoDisplay = new ObservableCollection<string>();
            for(int i = 0; i < VersionLinks.Length; i++)
            {
                string[] Version_Link = VersionLinks[i].Split(' ');
                VersionLinkPairs.Add(Version_Link[0], Version_Link[1]);
                VersionstoDisplay.Add(Version_Link[0]);
            }
            MainWindow.VersionSelector.ItemsSource = VersionstoDisplay;
            MainWindow.VersionSelector.Items.Refresh();
            MainWindow.Play.IsEnabled = true;
            MainWindow.VersionSelector.IsEnabled = true;
        }
    }
}
