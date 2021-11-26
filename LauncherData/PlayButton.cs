using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AGG_Productions.LauncherData
{
    class PlayButton2
    {
        public static GamePaths paths;
        public static VersionManager _VersionManager;
        public static void Start(string Name)
        {
            MainWindow.VersionSelector.IsEnabled = false;
            MainWindow.Play.IsEnabled = false;
            paths = new GamePaths(MainWindow.VersionToDownload, Name, MainWindow.GameDir);

            if (File.Exists(paths.ExeFile))
            {
                Process.Start(paths.ExeFile);
                App.Current.Shutdown();
            }
            else
            {
                try
                {
                    paths = new GamePaths(MainWindow.VersionToDownload, Name, MainWindow.GameDir);
                    FileDownloader downloader = new FileDownloader();

                    if (_VersionManager.VersionLinkPairs.TryGetValue(MainWindow.VersionToDownload, out string temp))
                    {
                        downloader.DownloadFileCompleted += Downloader_DownloadFileCompleted;
                        downloader.DownloadFileAsync(temp, $@"{paths.GameVersionFile}\Build({MainWindow.VersionToDownload}).zip");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("You must select a version in the dropdown list");
                    MainWindow.Play.IsEnabled = true;
                    MainWindow.VersionSelector.IsEnabled = true;
                }
            }
        }

        public static void Downloader_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                ZipFile.ExtractToDirectory($@"{paths.GameVersionFile}\Build({MainWindow.VersionToDownload}).zip", paths.GameVersionFile);
                File.Delete($@"{paths.GameVersionFile}\Build({MainWindow.VersionToDownload}).zip");
                Process.Start(paths.ExeFile);
                App.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MainWindow.Play.IsEnabled = true;
                MainWindow.VersionSelector.IsEnabled = true;
            }
        }
    }
}