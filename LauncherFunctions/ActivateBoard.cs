using System;

namespace AGG_Productions.LauncherFunctions
{
    class ActivateBoard
    {
        public ActivateBoard(string BoardName)
        {
            MainWindow.UpdateBoard.Source = new Uri($@"{Environment.CurrentDirectory}\UpdateBoards\{BoardName}Updates.html");
        }
    }
}
