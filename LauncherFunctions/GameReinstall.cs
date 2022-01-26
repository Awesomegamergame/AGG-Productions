using System.IO;

namespace AGG_Productions.LauncherFunctions
{
    class GameReinstall
    {
        public GameReinstall()
        {
            MainWindow.GameReInstallObject.IsEnabled = false;
            MainWindow.button = MainWindow.GameReInstallObject;
            AdminDirCheck.InstallDir(MainWindow.InstallGameName);
            MainWindow.GameDir = File.ReadAllText($"{MainWindow.InstallGameName}Dir.txt");
        }
    }
}
