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
        public static string versionFile = Path.Combine(Directory.GetCurrentDirectory(), "AGG Productions Version.txt");
        public static string rootPath = Directory.GetCurrentDirectory();
        public static string gameZip = Path.Combine(rootPath, "AGG Productions Temp.zip");
        public static string startPath = @".\AGG Productions Temp";
        public static void LauncherUpdate()
        {
            if (File.Exists(versionFile))
            {
                Version localVersion = new Version(File.ReadAllText(versionFile));

                try
                {
                    WebClient webClient = new WebClient();
                    Version onlineVersion = new Version(webClient.DownloadString(UpdateBoardLinks.LauncherVerLink));

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
                InstallGameFiles(false, Version.zero);
            }
        }

        private static void InstallGameFiles(bool _isUpdate, Version _onlineVersion)
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
                    _onlineVersion = new Version(webClient.DownloadString(UpdateBoardLinks.LauncherVerLink));
                }

                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadGameCompletedCallback);
                webClient.DownloadFileAsync(new Uri(UpdateBoardLinks.LauncherLink), gameZip, _onlineVersion);
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
                string onlineVersion = ((Version)e.UserState).ToString();
                if (Directory.Exists(startPath))
                {
                    Directory.Delete(startPath, true);
                    ZipFile.ExtractToDirectory(gameZip, rootPath);
                    File.Delete(gameZip);
                    File.WriteAllText(versionFile, onlineVersion);
                    //VersionText.Text = onlineVersion;
                    //Status = LauncherStatus.ready;

                    //Application.Current.Shutdown();
                }
                else
                {
                    File.Move(@".\AGG Productions.exe", @".\AGG Productions.exe.old");
                    File.Move(@".\AGG Productions.pdb", @".\AGG Productions.pdb.old");
                    try
                    {
                        //Declare a temporary path to unzip your files
                        string tempPath = Path.Combine(Directory.GetCurrentDirectory(), "tempUnzip");
                        string extractPath = Directory.GetCurrentDirectory();
                        ZipFile.ExtractToDirectory(gameZip, tempPath);

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
                    //ZipFile.ExtractToDirectory(gameZip, rootPath);
                    File.Delete(gameZip);
                    MessageBox.Show(onlineVersion);
                    File.WriteAllText(versionFile, onlineVersion);
                    //VersionText.Text = onlineVersion;
                    //Status = LauncherStatus.ready;


                    //Application.Current.Shutdown();

                }
            }
            catch (Exception ex)
            {
                //Status = LauncherStatus.failed;
                MessageBox.Show($"Error finishing download: {ex}");
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
                if (_versionStrings.Length != 4)
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
}