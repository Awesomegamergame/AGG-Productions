using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AGG_Productions.LauncherUpdater
{
    class Updater
    {
        public static string versionFile = Path.Combine(Directory.GetCurrentDirectory(), "AGG-Productions-Version.txt");
        public static string rootPath = Directory.GetCurrentDirectory();
        public static string gameZip = Path.Combine(rootPath, "AGG Productions Temp.zip");
        public static string startPath = @".\Chaotic Launcher Temp";
        public static string gameExe = Path.Combine(rootPath, "Updater.bat");
        public static void LauncherUpdate()
        {
            if (File.Exists(versionFile))
            {
                VersionChecker.Version localVersion = new VersionChecker.Version(File.ReadAllText(versionFile));

                try
                {
                    WebClient webClient = new WebClient();
                    VersionChecker.Version onlineVersion = new VersionChecker.Version(webClient.DownloadString("https://www.dropbox.com/s/mb1q445h8y1vgk5/ChaoticLauncherVersion.txt?dl=1"));

                    if (onlineVersion.IsDifferentThan(localVersion))
                    {
                        InstallGameFiles(true, onlineVersion);
                    }
                    else
                    {
                        //CheckForUpdates2();
                    }
                }
                catch (Exception ex)
                {
                    //Status = LauncherStatus.failed;
                    MessageBox.Show($"Error checking for game updates: {ex}");
                }
            }
            else
            {
                InstallGameFiles(false, VersionChecker.Version.zero);
            }
        }

        private static void InstallGameFiles(bool _isUpdate, VersionChecker.Version _onlineVersion)
        {
            try
            {
                WebClient webClient = new WebClient();
                if (_isUpdate)
                {
                    //Status = LauncherStatus.downloadingLauncherUpdate;
                }
                else
                {
                    //Status = LauncherStatus.downloadingLauncher;
                    _onlineVersion = new VersionChecker.Version(webClient.DownloadString("https://www.dropbox.com/s/mb1q445h8y1vgk5/ChaoticLauncherVersion.txt?dl=1"));
                }

                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadGameCompletedCallback);
                webClient.DownloadFileAsync(new Uri("https://www.dropbox.com/s/5qofh8awjh7pkc2/Chaotic%20Launcher%20Temp.zip?dl=1"), gameZip, _onlineVersion);
            }
            catch (Exception ex)
            {
                //Status = LauncherStatus.failed;
                MessageBox.Show($"Error installing game files: {ex}");
            }
        }

        private static void DownloadGameCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string onlineVersion = ((VersionChecker.Version)e.UserState).ToString();
                if (Directory.Exists(startPath))
                {
                    Directory.Delete(startPath, true);
                    ZipFile.ExtractToDirectory(gameZip, rootPath);
                    File.Delete(gameZip);
                    File.WriteAllText(versionFile, onlineVersion);
                    //VersionText.Text = onlineVersion;
                    //Status = LauncherStatus.ready;
                    ProcessStartInfo startInfo = new ProcessStartInfo(gameExe)
                    {
                        WorkingDirectory = Path.Combine(rootPath, "Chaotic Development Launcher Temp")
                    };
                    Process.Start(startInfo);
                    Application.Current.Shutdown();
                }
                else
                {
                    ZipFile.ExtractToDirectory(gameZip, rootPath);
                    File.Delete(gameZip);
                    File.WriteAllText(versionFile, onlineVersion);
                    //VersionText.Text = onlineVersion;
                    //Status = LauncherStatus.ready;
                    ProcessStartInfo startInfo = new ProcessStartInfo(gameExe)
                    {
                        WorkingDirectory = Path.Combine(rootPath, "Chaotic Development Launcher Temp")
                    };
                    Process.Start(startInfo);
                    Application.Current.Shutdown();

                }
            }
            catch (Exception ex)
            {
                //Status = LauncherStatus.failed;
                MessageBox.Show($"Error finishing download: {ex}");
            }
        }
    }
}
