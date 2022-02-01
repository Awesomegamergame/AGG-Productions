using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using AGG_Productions.LauncherData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AGG_Productions
{
    public class UpdateBoards
    {
        public static Dictionary<string, string> VersionLinkPairsB;
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
                VersionLinkPairsB = new Dictionary<string, string>();
                WebClient d = new WebClient();
                //TODO: Change to 2 seperate json files one for update boards and one for game links
                d.DownloadFile(new Uri(Json.JsonLink), $@"{Environment.CurrentDirectory}\LauncherLinks.json");
                //TODO: Get all boards without knowing the names kinda like the version manager has
                DownloadBoards("Chaotic", Json.ReadJson("Chaotic", "Updateboards", "LauncherLinks"));
                DownloadBoards("EastlowsHS", Json.ReadJson("EastlowsHS", "Updateboards", "LauncherLinks"));

                if (!File.Exists($"{MainWindow.InstallGameName}.json"))
                    return;
                ObservableCollection<string> VersionstoDisplay = new ObservableCollection<string>();
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText($"{MainWindow.InstallGameName}.json"));
                dynamic json = obj.Game;
                foreach (JProperty Version in json)
                {
                    string VerJson = Json.ReadGameJson(Version.Name, "version", MainWindow.InstallGameName);
                    string LinkJson = Json.ReadGameJson(Version.Name, "link", MainWindow.InstallGameName);
                    VersionstoDisplay.Add(VerJson);
                    VersionLinkPairsB.Add(VerJson, LinkJson);
                }
            }
        }
    }
}