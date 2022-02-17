using System.Diagnostics;
using System.Windows;

namespace AGG_Productions.LauncherFunctions
{
    class Arguments
    {
        public static void ProcessArguments(string arg1, string arg2)
        {
            switch (arg1)
            {
                case "AntiCheat":
                    foreach (Process process in Process.GetProcessesByName(arg2))
                    {
                        process.Kill();
                    }
                    MessageBox.Show($"{arg2}'s Anti-Cheat has detected that you are cheating and has closed the game.", "AGG Productions");
                    break;
                default:
                    MessageBox.Show("Invalid Argument");
                    break;
            }
        }
    }
}
