using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;

namespace AGG_Productions.LauncherData
{
    internal class PluginInstaller
    {
        public string Pluginlink;
        public string PluginVerLink;
        public string Location;
        public string startPath;
        public void InstallFiles(bool _isUpdate, LauncherUpdater.Version _onlineVersion, string Pluginlink, string Location, string PluginVerLink, string startpath)
        {
            try
            {
                FileDownloader Download = new FileDownloader();
                WebClient webClient = new WebClient();
                if (!_isUpdate)
                {
                    _onlineVersion = new LauncherUpdater.Version(webClient.DownloadString(PluginVerLink));
                }
                this.Pluginlink = Pluginlink;
                this.PluginVerLink = PluginVerLink;
                this.Location = Location;
                this.startPath = startpath;
                Download.DownloadFileAsync(Pluginlink, Location, _onlineVersion);
                Download.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadGameCompletedCallback);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error installing plugin files: {ex}");
            }
            
        }
        private void DownloadGameCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string onlineVersion = ((LauncherUpdater.Version)e.UserState).ToString();
                if (!Directory.Exists(startPath))
                {
                    try
                    {
                        //Declare a temporary path to unzip your files
                        string tempPath = Path.Combine(Directory.GetCurrentDirectory(), "tempUnzip");
                        string extractPath = Directory.GetCurrentDirectory();
                        ZipFile.ExtractToDirectory(Location, tempPath);

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
                    File.Delete(Location);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finishing download: {ex}");
            }
        }
    }
}
