using AGG_Productions.LauncherData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Environment;

namespace AGG_Productions.LauncherFunctions
{
    internal class Dynamicbuttons
    {
        public static void SetupButtons()
        {
            #region Dynamic Buttons
            if (CheckInternet.IsOnline)
            {
                WebClient d = new WebClient();
                d.DownloadFile(new Uri(Json.BDataLink), $@"{CurrentDirectory}\Cache\ButtonData.json");
            }
            if (!File.Exists($@"{CurrentDirectory}\Cache\ButtonData.json"))
                return;
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText($@"{CurrentDirectory}\Cache\ButtonData.json"));
            dynamic json = obj.Games;
            foreach (JProperty Names in json)
            {
                WebClient d = new WebClient();
                string ImageLink = Json.ReadGameVerJson(Names.Name, "link", "ButtonData", "Games");
                string GameName = Json.ReadGameVerJson(Names.Name, "name", "ButtonData", "Games");
                if (CheckInternet.IsOnline)
                    d.DownloadFile(new Uri(ImageLink), $@"{CurrentDirectory}\Cache\Images\{GameName}.jpg");
                Button newBtn = new Button();
                if (File.Exists($@"{CurrentDirectory}\Cache\Images\{GameName}.jpg"))
                {
                    BitmapImage btm = new BitmapImage();
                    btm.BeginInit();
                    btm.CacheOption = BitmapCacheOption.OnLoad;
                    btm.UriSource = new Uri($@"{CurrentDirectory}\Cache\Images\{GameName}.jpg", UriKind.RelativeOrAbsolute);
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
                newBtn.Height = 50;
                newBtn.Width = 191;
                newBtn.HorizontalAlignment = HorizontalAlignment.Left;
                MainWindow.ListObject.Items.Add(newBtn);
                newBtn.Click += new RoutedEventHandler(MainWindow.Game_Click);
            }
            #endregion
        }
    }
}
