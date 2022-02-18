using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using AGG_Productions.LauncherFunctions;

namespace AGG_Productions
{
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if(args.Length == 3)
            {
                Arguments.ProcessArguments(args[1], args[2]);
                Process[] pname = Process.GetProcessesByName(AppDomain.CurrentDomain.FriendlyName.Remove(AppDomain.CurrentDomain.FriendlyName.Length - 4));
                if (pname.Length > 1)
                {
                    pname.Where(p => p.Id != Process.GetCurrentProcess().Id).First().Kill();
                }
            }
            else
            {
                var exists = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1;
                if(exists)
                {
                    Current.Shutdown();
                }
            }
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
