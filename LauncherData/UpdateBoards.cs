using System;
using System.IO;
using System.Net;

namespace AGG_Productions
{
    public class UpdateBoards
    {
        public static string BoardNameM;
        public static void DownloadBoards(string BoardName, string BoardHTMLLink)
        {
            BoardNameM += BoardName;
            string BoardCheck = $@"{Environment.CurrentDirectory}\UpdateBoards\{BoardName}Updates.html";
            string UpdateBoardDir = $@"{Environment.CurrentDirectory}\UpdateBoards";

            WebClient c = new WebClient();
        A:
            if (Directory.Exists(UpdateBoardDir))
            {
                if (CheckInternet.IsOnline)
                {
                    c.DownloadFileAsync(new Uri(BoardHTMLLink), $@"{Environment.CurrentDirectory}\UpdateBoards\{BoardName}Updates.html");
                }
                else
                {
                    if (File.Exists(BoardCheck))
                    {
                        MainWindow.UpdateBoard.Source = new Uri($@"{Environment.CurrentDirectory}\UpdateBoards\{BoardName}Updates.html");
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(UpdateBoardDir);
                goto A;
            }
        }
    }
}