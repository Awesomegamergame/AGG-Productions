using System.Windows;

namespace AGG_Productions.LauncherFunctions
{
    internal class SettingsM
    {
        public static bool Menu(bool Enabled)
        {
            if(Enabled == true)
            {
                MainWindow.SettingsMenuObject.Visibility = Visibility.Collapsed;
                return false;
            }
            else
            {
                MainWindow.SettingsMenuObject.Visibility = Visibility.Visible;
                return true;
            }

        }
    }
}
