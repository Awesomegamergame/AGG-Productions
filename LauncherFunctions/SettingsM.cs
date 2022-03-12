using System.Windows;
using static AGG_Productions.MainWindow;

namespace AGG_Productions.LauncherFunctions
{
    internal class SettingsM
    {
        public static bool Menu(bool Enabled)
        {
            if(Enabled == true)
            {
                AGGWindow.SettingScreen.Visibility = Visibility.Collapsed;
                return false;
            }
            else
            {
                AGGWindow.SettingScreen.Visibility = Visibility.Visible;
                return true;
            }

        }
    }
}
