using System;
using System.IO;
using System.Net;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Environment;
using static AGG_Productions.MainWindow;
using System.Windows.Media.Imaging;
using AGG_Productions.LauncherData;
using AGG_Productions.LauncherUpdater;

namespace AGG_Productions.LauncherFunctions
{
    internal class Dynamicbuttons
    {
        public static bool DevMode = false;
        public static void SetupButtons()
        {
            if (File.Exists("devgames.txt"))
            {
                if (!Directory.Exists("DevCache"))
                    Directory.CreateDirectory("DevCache");
                if (!Directory.Exists($@"{CurrentDirectory}\DevCache\Images"))
                    Directory.CreateDirectory($@"{CurrentDirectory}\DevCache\Images");
                DevMode = true;
                ReadJson("DevCache");
            }
            else
            {
                if(Directory.Exists("DevCache"))
                    UpgradeLauncher.DeleteDirectory("DevCache");
            }
            if (CheckInternet.IsOnline)
            {
                WebClient d = new WebClient();
                d.DownloadFile(new Uri(Json.BDataLink), $@"{CurrentDirectory}\Cache\ButtonData.json");
            }
            ReadJson("Cache");
        }
        private static void ReadJson(string CacheLoc)
        {
            if (!File.Exists($@"{CurrentDirectory}\{CacheLoc}\ButtonData.json"))
                return;
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText($@"{CurrentDirectory}\{CacheLoc}\ButtonData.json"));
            dynamic json = obj.Games;
            foreach (JProperty Names in json)
            {
                WebClient d = new WebClient();
                string ImageLink = Json.ReadGameVerJson(Names.Name, "image", "ButtonData", "Games", CacheLoc);
                string GameName = Json.ReadGameVerJson(Names.Name, "name", "ButtonData", "Games", CacheLoc);
                string HTML = Json.ReadGameVerJson(Names.Name, "html", "ButtonData", "Games", CacheLoc);
                if (CheckInternet.IsOnline)
                    d.DownloadFile(new Uri(ImageLink), $@"{CurrentDirectory}\{CacheLoc}\Images\{GameName}.jpg");
                Button newBtn = new Button();
                if (File.Exists($@"{CurrentDirectory}\{CacheLoc}\Images\{GameName}.jpg"))
                {
                    BitmapImage btm = new BitmapImage();
                    btm.BeginInit();
                    btm.CacheOption = BitmapCacheOption.OnLoad;
                    btm.UriSource = new Uri($@"{CurrentDirectory}\{CacheLoc}\Images\{GameName}.jpg", UriKind.RelativeOrAbsolute);
                    btm.EndInit();
                    Image img = new Image
                    {
                        Source = btm,
                        Stretch = Stretch.Fill
                    };
                    newBtn.Content = img;
                }
                else
                    newBtn.Content = GameName;
                newBtn.Name = GameName;
                if (HTML.Equals("Yes") || HTML.Equals("yes"))
                    newBtn.Tag = true;
                else
                    newBtn.Tag = false;
                newBtn.Height = 50;
                newBtn.Width = 191;
                newBtn.HorizontalAlignment = HorizontalAlignment.Left;
                AGGWindow.List.Items.Add(newBtn);
                newBtn.Click += new RoutedEventHandler(Game_Click);
            }
        }
    }
}
