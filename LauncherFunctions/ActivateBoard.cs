using System;
using System.IO;

namespace AGG_Productions.LauncherFunctions
{
    class ActivateBoard
    {
        public ActivateBoard(string BoardName)
        {
            if (File.Exists($@"{Environment.CurrentDirectory}\UpdateBoards\{BoardName}Updates.html"))
            {
                MainWindow.UpdateBoard.Source = new Uri($@"{Environment.CurrentDirectory}\UpdateBoards\{BoardName}Updates.html");
            }
            else
            {
                MainWindow.UpdateBoard.Navigate("about:blank");
            }
        }
    }
}
