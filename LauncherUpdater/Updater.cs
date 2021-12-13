using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;
using AGG_Productions.Repair;

namespace AGG_Productions.LauncherUpdater
{
    class Updater
    {
        public static string LauncherLink = "https://www.dropbox.com/s/27bwz9ct96qltlx/AGG%20Productions%20Temp.zip?dl=1";
        public static string LauncherVerLink = "https://www.dropbox.com/s/l0s6jjask4paool/AGG%20Productions%20Version.txt?dl=1";
        public static string startPath = @".\AGG Productions Temp";
        public static string exeOld = Path.Combine(CheckFiles.rootPath, "AGG Productions.exe.old");
        public static string pdbOld = Path.Combine(CheckFiles.rootPath, "AGG Productions.pdb.old");
        public static string versionFile = Path.Combine(CheckFiles.rootPath, "AGG Productions Version.txt");
        public static string launcherZip = Path.Combine(CheckFiles.rootPath, "AGG Productions Temp.zip");
        public static int VersionDetector = 0;
        public static Version onlineVersion;
        public static Version localVersion;
        public static void LauncherUpdate()
        {
            if (File.Exists(exeOld))
            {
                File.Delete(exeOld);
            }
            if (File.Exists(pdbOld))
            {
                File.Delete(pdbOld);
            }
            if (Directory.Exists(startPath))
            {
                Directory.Delete(startPath);
            }

            if (File.Exists(versionFile))
            {
                localVersion = new Version(File.ReadAllText(versionFile));

                try
                {
                    WebClient webClient = new WebClient();
                    onlineVersion = new Version(webClient.DownloadString(LauncherVerLink));
                    if (CheckFiles.FilesCheckPassed == false)
                    {
                        InstallGameFiles(false, Version.zero);
                    }
                    else if (onlineVersion.IsDifferentThan(localVersion))
                    {
                        VersionDetector += 1;
                        MainWindow.UpdateScreen_Image.Visibility = Visibility.Visible;
                        MainWindow.Yes_Button.Visibility = Visibility.Visible;
                        MainWindow.No_Button.Visibility = Visibility.Visible;
                        MainWindow.UpdateText1_Label.Visibility = Visibility.Visible;
                        MainWindow.UpdateText2_Label.Visibility = Visibility.Visible;
                        MainWindow.LocalVersionObject.Visibility = Visibility.Visible;
                        MainWindow.OnlineVersionObject.Visibility = Visibility.Visible;
                        MainWindow.LocalVersionNumberObject.Content = localVersion;
                        MainWindow.OnlineVersionNumberObject.Content = onlineVersion;
                        MainWindow.LocalVersionNumberObject.Visibility = Visibility.Visible;
                        MainWindow.OnlineVersionNumberObject.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking for game updates: {ex}");
                }
            }
            else if (CheckFiles.FilesCheckPassed == false)
            {
                InstallGameFiles(false, Version.zero);
            }
            else
            {
                WebClient webClient = new WebClient();
                onlineVersion = new Version(webClient.DownloadString(LauncherVerLink));
                VersionDetector += 2;
                MainWindow.UpdateScreen_Image.Visibility = Visibility.Visible;
                MainWindow.Yes_Button.Visibility = Visibility.Visible;
                MainWindow.No_Button.Visibility = Visibility.Visible;
                MainWindow.UpdateText1_Label.Visibility = Visibility.Visible;
                MainWindow.UpdateText2_Label.Visibility = Visibility.Visible;
                MainWindow.LocalVersionObject.Visibility = Visibility.Visible;
                MainWindow.OnlineVersionObject.Visibility = Visibility.Visible;
                MainWindow.LocalVersionNumberObject.Content = "Unknown";
                MainWindow.OnlineVersionNumberObject.Content = onlineVersion;
                MainWindow.LocalVersionNumberObject.Visibility = Visibility.Visible;
                MainWindow.OnlineVersionNumberObject.Visibility = Visibility.Visible;
            }
        }

        public static void InstallGameFiles(bool _isUpdate, Version _onlineVersion)
        {
            try
            {
                FileDownloader LauncherDownload = new FileDownloader();
                WebClient webClient = new WebClient();
                if (!_isUpdate)
                {
                    _onlineVersion = new Version(webClient.DownloadString(LauncherVerLink));
                }
                LauncherDownload.DownloadFileAsync(LauncherLink, launcherZip, _onlineVersion);
                LauncherDownload.DownloadProgressChanged += LauncherDownload_DownloadProgressChanged;
                LauncherDownload.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadGameCompletedCallback);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error installing game files: {ex}");
            }
        }

        private static void LauncherDownload_DownloadProgressChanged(object sender, FileDownloader.DownloadProgress progress)
        {
            MainWindow.UpdateDownloadBar.Value = progress.ProgressPercentage;
            MainWindow.RepairBarObject.Value = progress.ProgressPercentage;
        }

        private static void DownloadGameCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string onlineVersion = ((Version)e.UserState).ToString();
                if (!Directory.Exists(startPath))
                {
                    if (File.Exists(CheckFiles.LauncherExe))
                    {
                        File.Move(@".\AGG Productions.exe", @".\AGG Productions.exe.old");
                    }
                    if (File.Exists(CheckFiles.Launcherpdb))
                    {
                        File.Move(@".\AGG Productions.pdb", @".\AGG Productions.pdb.old");
                    }
                    try
                    {
                        //Declare a temporary path to unzip your files
                        string tempPath = Path.Combine(Directory.GetCurrentDirectory(), "tempUnzip");
                        string extractPath = Directory.GetCurrentDirectory();
                        ZipFile.ExtractToDirectory(launcherZip, tempPath);

                        //build an array of the unzipped files
                        string[] files = Directory.GetFiles(tempPath);

                        foreach (string file in files)
                        {
                            FileInfo f = new FileInfo(file);
                            //Check if the file exists already, if so delete it and then move the new file to the extract folder
                            if (File.Exists(Path.Combine(extractPath, f.Name)))
                            {
                                File.Delete(Path.Combine(extractPath, f.Name));
                                File.Move(f.FullName, Path.Combine(extractPath, f.Name));
                            }
                            else
                            {
                                File.Move(f.FullName, Path.Combine(extractPath, f.Name));
                            }
                        }
                        //Delete the temporary directory.
                        Directory.Delete(tempPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    File.Delete(launcherZip);
                    File.WriteAllText(versionFile, onlineVersion);

                    Process.Start(CheckFiles.LauncherExe);
                    Application.Current.Shutdown();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finishing download: {ex}");
            }
        }
        public static void UpdaterVersion()
        {
            if(VersionDetector == 1)
            {
                InstallGameFiles(true, onlineVersion);
            }
            else if(VersionDetector == 2)
            {
                InstallGameFiles(false, Version.zero);
            }
        }
    }

    public struct Version
    {
        internal static Version zero = new Version(0, 0, 0);

        private readonly short major;
        private readonly short minor;
        private readonly short subMinor;

        internal Version(short _major, short _minor, short _subMinor)
        {
            major = _major;
            minor = _minor;
            subMinor = _subMinor;
        }
        internal Version(string _version)
        {
            string[] _versionStrings = _version.Split('.');
            if (_versionStrings.Length != 3)
            {
                major = 0;
                minor = 0;
                subMinor = 0;
                return;
            }

            major = short.Parse(_versionStrings[0]);
            minor = short.Parse(_versionStrings[1]);
            subMinor = short.Parse(_versionStrings[2]);
        }
        internal bool IsDifferentThan(Version _otherVersion)
        {
            if (major != _otherVersion.major)
            {
                return true;
            }
            else
            {
                if (minor != _otherVersion.minor)
                {
                    return true;
                }
                else
                {
                    if (subMinor != _otherVersion.subMinor)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override string ToString()
        {
            return $"{major}.{minor}.{subMinor}";
        }
    }
}