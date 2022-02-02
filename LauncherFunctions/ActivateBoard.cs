using System;
using System.IO;
using static System.Environment;

namespace AGG_Productions.LauncherFunctions
{
    class ActivateBoard
    {
        public ActivateBoard(string BoardName)
        {
            if (File.Exists($@"{CurrentDirectory}\Cache\UpdateBoards\{BoardName}Updates.html"))
            {
                MainWindow.UpdateBoard.Source = new Uri($@"{CurrentDirectory}\Cache\UpdateBoards\{BoardName}Updates.html");
            }
            else
            {
                MainWindow.UpdateBoard.Navigate("about:blank");
            }
        }
    }
}
