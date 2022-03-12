using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace HTMLPlayer
{
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 2)
            {
                Arguments.ProcessArguments(args[1]);
                Process[] pname = Process.GetProcessesByName(AppDomain.CurrentDomain.FriendlyName.Remove(AppDomain.CurrentDomain.FriendlyName.Length - 4));
                if (pname.Length > 1)
                {
                    pname.Where(p => p.Id != Process.GetCurrentProcess().Id).First().Kill();
                }
                MainWindow window = new MainWindow();
                window.Show();
            }
            else
                Current.Shutdown();
        }
    }
}
