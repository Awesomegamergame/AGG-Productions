using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;
using static System.Environment;

namespace AGG_Productions.LauncherData
{
    internal class PluginInstaller
    {
        public void InstallFiles(string Pluginlink, string Location, string PluginName)
        {
            try
            {
                if(File.Exists(Location))
                    File.Delete(Location);
                WebClient webClient = new WebClient();
                webClient.DownloadFile(Pluginlink, Location);
                ZipFile.ExtractToDirectory(Location, $@"{CurrentDirectory}\Plugins\{PluginName}");
                File.Delete(Location);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error installing plugin files: {ex}");
            }
        }
    }
}
