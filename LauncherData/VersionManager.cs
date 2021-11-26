using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGG_Productions.LauncherData
{
    class VersionManager
    {
        public static string VersionLink;
        //https://www.dropbox.com/s/sfd89lnravmvj4j/Version.txt?dl=1
        public Dictionary<string, string> VersionLinkPairs;

        private readonly MainWindow WindowClass;
        public VersionManager(MainWindow WindowsClass)
        {
            this.WindowClass = WindowsClass;

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
