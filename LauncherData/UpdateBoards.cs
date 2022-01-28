using System;
using System.IO;
using System.Net;
using AGG_Productions.LauncherData;

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
                d.DownloadFile(new Uri(Json.JsonLink), $@"{Environment.CurrentDirectory}\LauncherLinks.json");
                DownloadBoards("Chaotic", Json.ReadJson("Chaotic", "Updateboards", "LauncherLinks"));
                DownloadBoards("EastlowsHS", Json.ReadJson("EastlowsHS", "Updateboards", "LauncherLinks"));
            }
        }
    }
}