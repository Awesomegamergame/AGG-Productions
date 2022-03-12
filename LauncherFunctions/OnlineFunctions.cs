using System.IO;
using System.Windows;
using static System.Environment;
using static AGG_Productions.MainWindow;
using AGG_Productions.LauncherUpdater;

namespace AGG_Productions.LauncherFunctions
{
    class OnlineFunctions
    {
        public static void UpdateFunctions()
        {
            if (CheckInternet.IsOnline)
            {
                CheckFiles.CheckForFiles();
                if (CheckFiles.FilesCheckPassed)
                {
                    if (!Directory.Exists("Cache"))
                        Directory.CreateDirectory("Cache");
                    if (!Directory.Exists($@"{CurrentDirectory}\Cache\Images"))
                        Directory.CreateDirectory($@"{CurrentDirectory}\Cache\Images");
                    Dynamicbuttons.SetupButtons();
                    UpdateBoards.SetupBoards();
                    Updater.LauncherUpdate();
                }
                else
                {
                    AGGWindow.RepairScreen.Visibility = Visibility.Visible;
                    AGGWindow.RepairBar.Visibility = Visibility.Visible;
                    AGGWindow.RepairText.Visibility = Visibility.Visible;
                    AGGWindow.RepairBodyText.Visibility = Visibility.Visible;
                    Updater.LauncherUpdate();
                }
            }
            else
            {
                CheckFiles.CheckForFilesNoInternet();
                if (CheckFiles.FilesCheckPassedNo == false)
                {
                    AGGWindow.RepairScreen.Visibility = Visibility.Visible;
                    AGGWindow.RepairText.Visibility = Visibility.Visible;
                    AGGWindow.RepairBodyText.Content = "Please Connect To The Internet And Restart The Launcher To Repair It";
                    AGGWindow.RepairBodyText.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
