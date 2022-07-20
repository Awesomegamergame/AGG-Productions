using System;
using System.IO;
using System.Net;
using static System.Environment;
using AGG_Productions.LauncherData;
using static AGG_Productions.MainWindow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AGG_Productions.LauncherFunctions;

namespace AGG_Productions
{
    public class UpdateBoards
    {
        public static string BoardNameM;
        public static void DownloadBoards(string BoardName, string BoardHTMLLink, string CacheLoc)
        {
            BoardNameM += BoardName;
            string UpdateBoardDir = $@"{CurrentDirectory}\{CacheLoc}\UpdateBoards";

            WebClient c = new WebClient();
        A:
            if (Directory.Exists(UpdateBoardDir))
            {
                if (CheckInternet.IsOnline)
                {
                    c.DownloadFileAsync(new Uri(BoardHTMLLink), $@"{CurrentDirectory}\{CacheLoc}\UpdateBoards\{BoardName}Updates.html");
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
                if (File.Exists($@"{CurrentDirectory}\Cache\ButtonData.json"))
                {
                    dynamic obj = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText($@"{CurrentDirectory}\Cache\ButtonData.json"));
                    dynamic json = obj.Games;
                    foreach (JProperty Version in json)
                    {
                        string LinkJson = Json.ReadGameVerJson(Version.Name, "updates", "ButtonData", "Games", "Cache");
                        DownloadBoards(Version.Name, LinkJson, "Cache");
                    }
                }
                if(Dynamicbuttons.DevMode && File.Exists($@"{CurrentDirectory}\DevCache\ButtonData.json"))
                {
                    dynamic obj = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText($@"{CurrentDirectory}\DevCache\ButtonData.json"));
                    dynamic json = obj.Games;
                    foreach (JProperty Version in json)
                    {
                        string LinkJson = Json.ReadGameVerJson(Version.Name, "updates", "ButtonData", "Games", "DevCache");
                        DownloadBoards(Version.Name, LinkJson, "DevCache");
                    }
                }
            }
        }
    }
    public class ActivateBoard
    {
        public ActivateBoard(string BoardName)
        {
            if (File.Exists($@"{CurrentDirectory}\Cache\UpdateBoards\{BoardName}Updates.html"))
            {
                AGGWindow.UpdateBoard.Source = new Uri($@"{CurrentDirectory}\Cache\UpdateBoards\{BoardName}Updates.html");
            }
            else
            {
                AGGWindow.UpdateBoard.Navigate("about:blank");
            }
        }
    }
}