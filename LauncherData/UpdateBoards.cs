using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using AGG_Productions.LauncherData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AGG_Productions
{
    public class UpdateBoards
    {
        public static string BoardNameM;
        public static void DownloadBoards(string BoardName, string BoardHTMLLink)
        {
            BoardNameM += BoardName;
            string UpdateBoardDir = $@"{Environment.CurrentDirectory}\UpdateBoards";

            WebClient c = new WebClient();
        A:
            if (Directory.Exists(UpdateBoardDir))
            {
                if (CheckInternet.IsOnline)
                {
                    c.DownloadFileAsync(new Uri(BoardHTMLLink), $@"{Environment.CurrentDirectory}\UpdateBoards\{BoardName}Updates.html");
                }
            }
            else
            {
                Directory.CreateDirectory(UpdateBoardDir);
                goto A;
            }
        }
        public static void SetupBoards()
        {
            if (CheckInternet.IsOnline)
            {
                WebClient d = new WebClient();
                d.DownloadFile(new Uri(Json.BJsonLink), $@"{Environment.CurrentDirectory}\Updates.json");
                d.DownloadFile(new Uri(Json.GJsonLink), $@"{Environment.CurrentDirectory}\Games.json");
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("Updates.json"));
                dynamic json = obj.Updates;
                foreach (JProperty Version in json)
                {
                    string LinkJson = Json.ReadGameJson(Version.Name, "link", "Updates", "Updates");
                    MessageBox.Show(LinkJson);
                    DownloadBoards(Version.Name, LinkJson);
                }
            }
        }
    }
}