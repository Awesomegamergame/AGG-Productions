using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;
using static System.Environment;
using AGG_Productions.LauncherUpdater;

namespace AGG_Productions.LauncherData
{
    internal class PluginInstaller
    {
        public void InstallFiles(string Pluginlink, string Location, string PluginName)
        {
                if (File.Exists(Location))
                    File.Delete(Location);
                WebClient webClient = new WebClient();
                webClient.DownloadFile(Pluginlink, Location);
                if (Directory.Exists($@"{CurrentDirectory}\Plugins\{PluginName}"))
                    UpgradeLauncher.DeleteDirectory($@"{CurrentDirectory}\Plugins\{PluginName}");
                ZipFile.ExtractToDirectory(Location, $@"{CurrentDirectory}\Plugins\{PluginName}");
                File.Delete(Location);
        }
    }
}
