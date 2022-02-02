using AGG_Productions.LauncherData;

namespace AGG_Productions.LauncherFunctions
{
    class GameReinstall
    {
        public GameReinstall()
        {
            MainWindow.GameReInstallObject.IsEnabled = false;
            MainWindow.button = MainWindow.GameReInstallObject;
            AdminDirCheck.InstallDir(MainWindow.InstallGameName);
            MainWindow.GameReInstallObject.IsEnabled = true;
            MainWindow.GameDir = Json.ReadJson(MainWindow.InstallGameName);
        }
    }
}
