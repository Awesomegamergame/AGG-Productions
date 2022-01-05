using AGG_Productions.GameFunctions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;

namespace AGG_Productions.LauncherData
{
    class VersionManager
    {
        public static string VersionLink;
        public Dictionary<string, string> VersionLinkPairs;
        private SelectScreen selectScreen;
        private GameInstall gameInstall;
#pragma warning disable IDE0052 // Remove unread private members
        private readonly MainWindow WindowClass;
#pragma warning restore IDE0052 // Remove unread private members
        public VersionManager(MainWindow WindowsClass)
        {
            this.WindowClass = WindowsClass;

            VersionLinkPairs = new Dictionary<string, string>();
            Init();
        }

        public VersionManager(SelectScreen selectScreen)
        {
            this.selectScreen = selectScreen;
        }

        public VersionManager(GameInstall gameInstall)
        {
            this.gameInstall = gameInstall;
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
