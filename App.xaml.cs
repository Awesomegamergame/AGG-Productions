using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace AGG_Productions
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string GameName;
        public static int ArgumentNumber;
        //1 = AntiCheat
        private void App_Startup(object sender, StartupEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if(args.Length == 3)
                ProcessArguments(args[1], args[2]);
            Process[] pname = Process.GetProcessesByName(AppDomain.CurrentDomain.FriendlyName.Remove(AppDomain.CurrentDomain.FriendlyName.Length - 4));
            if (pname.Length > 1)
            {
                pname.Where(p => p.Id != Process.GetCurrentProcess().Id).First().Kill();
            }
            MainWindow window = new MainWindow();
            window.Show();
        }
        public void ProcessArguments(string arg1, string arg2)
        {
            
        }
    }
}
