using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGG_Productions.GameFunctions
{
    class ActivateBoard
    {
        public ActivateBoard(string BoardName)
        {
            MainWindow.UpdateBoard.Source = new Uri($@"{Environment.CurrentDirectory}\UpdateBoards\{BoardName}Updates.html");
        }
    }
}
