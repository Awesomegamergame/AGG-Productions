using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AGG_Productions.LauncherData;
using AGG_Productions.GameLinks;

namespace AGG_Productions.GameFunctions
{
    class GameInstall
    {
        public GameInstall(string GameName, string GameLink)
        {
            MainWindow.GameInstallObject.IsEnabled = false;
            MainWindow.button = MainWindow.GameInstallObject;
            AdminDirCheck.InstallDir(GameName);
            if (AdminDirCheck.FileDialogClosed)
            {
                AdminDirCheck.FileDialogClosed = false;
                return;
            }
            MainWindow.GameDir = File.ReadAllText($"{GameName}Dir.txt");
            MainWindow.GameInstallObject.Visibility = Visibility.Collapsed;
            MainWindow.VersionSelector.Visibility = Visibility.Visible;
            MainWindow.Play.Visibility = Visibility.Visible;
            MainWindow.GameDownloadBar.Visibility = Visibility.Visible;
            MainWindow.GameReInstallObject.Visibility = Visibility.Visible;
            VersionManager.VersionLink = GameLink;
            PlayButton2._VersionManager = new VersionManager(this);
        }
    }
}
