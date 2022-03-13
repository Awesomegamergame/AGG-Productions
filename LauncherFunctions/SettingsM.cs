using System.Reflection;
using System.Windows;
using System.IO;
using static System.Environment;
using static AGG_Productions.MainWindow;

namespace AGG_Productions.LauncherFunctions
{
    internal class SettingsM
    {
        public static bool Menu(bool Enabled)
        {
            string versions = null;
            if (File.Exists($@"{CurrentDirectory}\Plugins\HTMLPlayer\HTMLPlayer.exe"))
            {
                Assembly assembly1 = Assembly.LoadFile($@"{CurrentDirectory}\Plugins\HTMLPlayer\HTMLPlayer.exe");
                System.Version version1 = assembly1.GetName().Version;
                versions = version1.ToString();
                versions = versions.Substring(0, versions.Length - 2);
            }
            Assembly assembly = Assembly.GetExecutingAssembly();
            System.Version version = assembly.GetName().Version;
            string versionS = version.ToString();
            versionS = versionS.Substring(0, versionS.Length - 2);
            if (Enabled == true)
            {
                AGGWindow.SettingScreen.Visibility = Visibility.Collapsed;
                AGGWindow.AGGVer.Visibility = Visibility.Collapsed;
                AGGWindow.HTMLVer.Visibility = Visibility.Collapsed;
                return false;
            }
            else
            {
                AGGWindow.SettingScreen.Visibility = Visibility.Visible;
                AGGWindow.AGGVer.Visibility = Visibility.Visible;
                AGGWindow.HTMLVer.Visibility = Visibility.Visible;
                AGGWindow.AGGVer.Content = $"AGG Productions Version: {versionS}";
                AGGWindow.HTMLVer.Content = $"HTML Player Version: {versions}";
                AGGWindow.UpdateBoard.Visibility = Visibility.Collapsed;
                AGGWindow.GameDownload.Visibility = Visibility.Collapsed;
                AGGWindow.PlayButtonGUI.Visibility = Visibility.Collapsed;
                AGGWindow.VersionBox.Visibility = Visibility.Collapsed;
                AGGWindow.Game_Install.Visibility = Visibility.Collapsed;
                AGGWindow.Game_ReInstall.Visibility = Visibility.Collapsed;
                AGGWindow.NoGame.Visibility = Visibility.Visible;
                AGGWindow.SelectGame.Visibility = Visibility.Visible;
                return true;
            }

        }
    }
}
