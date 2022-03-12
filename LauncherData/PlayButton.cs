using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows;
using static AGG_Productions.MainWindow;

namespace AGG_Productions.LauncherData
{
    class PlayButton
    {
        public static GamePaths paths;
        public static VersionManager _VersionManager;
        public static void Start(string Name)
        {
            CheckInternet.CheckInternetState();
            VersionSelector.IsEnabled = false;
            AGGWindow.PlayButtonGUI.IsEnabled = false;
            paths = new GamePaths(VersionToDownload, Name, GameDir);

            if (File.Exists(paths.ExeFile))
            {
                Process.Start(paths.ExeFile);
                Application.Current.Shutdown();
            }
            else if (CheckInternet.IsOnline == false)
            {
                MessageBox.Show("This version of this game isn't already downloaded. Check your internet and try again");
                AGGWindow.PlayButtonGUI.IsEnabled = true;
                VersionSelector.IsEnabled = true;
            }
            else
            {
                try
                {
                    paths = new GamePaths(VersionToDownload, Name, GameDir);
                    FileDownloader downloader = new FileDownloader();

                    if (_VersionManager.VersionLinkPairs.TryGetValue(VersionToDownload, out string temp))
                    {
                        downloader.DownloadFileCompleted += Downloader_DownloadFileCompleted;
                        downloader.DownloadProgressChanged += Downloader_DownloadProgressChanged;
                        downloader.DownloadFileAsync(temp, $@"{paths.GameVersionFile}\Build({VersionToDownload}).zip");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("You must select a version in the dropdown list");
                    AGGWindow.PlayButtonGUI.IsEnabled = true;
                    VersionSelector.IsEnabled = true;
                }
            }
        }

        private static void Downloader_DownloadProgressChanged(object sender, FileDownloader.DownloadProgress progress)
        {
            AGGWindow.GameDownload.Value = progress.ProgressPercentage;
        }

        public static void Downloader_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                ZipFile.ExtractToDirectory($@"{paths.GameVersionFile}\Build({VersionToDownload}).zip", paths.GameVersionFile);
                File.Delete($@"{paths.GameVersionFile}\Build({VersionToDownload}).zip");
                Process.Start(paths.ExeFile);
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                AGGWindow.PlayButtonGUI.IsEnabled = true;
                VersionSelector.IsEnabled = true;
            }
        }
    }
}