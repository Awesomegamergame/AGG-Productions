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
            string UpdateBoardDir = $@"{Environment.CurrentDirectory}\UpdateBoards";

            WebClient c = new WebClient();
        A:
            if (Directory.Exists(UpdateBoardDir))
            {
                c.DownloadFileCompleted += C_DownloadFileCompleted;
                if (CheckInternet.IsOnline)
                {
                    c.DownloadFileAsync(new Uri(BoardHTMLLink), $@"{Environment.CurrentDirectory}\UpdateBoards\{BoardName}Updates.html");
                }
                else
                {
                    MainWindow.UpdateBoard.Source = new Uri($@"{Environment.CurrentDirectory}\UpdateBoards\{BoardName}Updates.html");
                }
            }
            else
            {
                Directory.CreateDirectory(UpdateBoardDir);
                goto A;
            }
        }
        public static void C_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MainWindow.UpdateBoard.Source = new Uri($@"{Environment.CurrentDirectory}\UpdateBoards\{BoardNameM}Updates.html");
        }
    }
}