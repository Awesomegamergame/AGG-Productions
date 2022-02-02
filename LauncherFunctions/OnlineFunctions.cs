using System.Windows;
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
                    Updater.LauncherUpdate();
                }
                else
                {
                    MainWindow.RepairScreenObject.Visibility = Visibility.Visible;
                    MainWindow.RepairBarObject.Visibility = Visibility.Visible;
                    MainWindow.RepairTextObject.Visibility = Visibility.Visible;
                    MainWindow.RepairBodyObject.Visibility = Visibility.Visible;
                    Updater.LauncherUpdate();
                }
            }
            else
            {
                CheckFiles.CheckForFilesNoInternet();
                if (CheckFiles.FilesCheckPassedNo == false)
                {
                    MainWindow.RepairScreenObject.Visibility = Visibility.Visible;
                    MainWindow.RepairTextObject.Visibility = Visibility.Visible;
                    MainWindow.RepairBodyObject.Content = "Please Connect To The Internet And Restart The Launcher To Repair It";
                    MainWindow.RepairBodyObject.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
