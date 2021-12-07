using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AGG_Productions.Repair
{
    class CheckFiles
    {
        #region Files
        public static string rootPath = Directory.GetCurrentDirectory();
        public static string LauncherExe = Path.Combine(rootPath, "AGG Productions.exe");
        public static string LauncherConfig = Path.Combine(rootPath, "AGG Productions.exe.config");
        public static string Launcherpdb = Path.Combine(rootPath, "AGG Productions.pdb");
        public static string CodePackDLL = Path.Combine(rootPath, "Microsoft.WindowsAPICodePack.dll");
        public static string ShellDLL = Path.Combine(rootPath, "Microsoft.WindowsAPICodePack.Shell.dll");
        public static string ShellXML = Path.Combine(rootPath, "Microsoft.WindowsAPICodePack.Shell.xml");
        public static string CodePackXML = Path.Combine(rootPath, "Microsoft.WindowsAPICodePack.xml");
        public static bool FilesCheckPassed;
        #endregion
        public static void CheckForFiles()
        {
            if (File.Exists(LauncherExe) && File.Exists(LauncherConfig) && File.Exists(Launcherpdb) && File.Exists(CodePackDLL) && File.Exists(ShellDLL) && File.Exists(ShellXML) && File.Exists(CodePackXML))
            {
                FilesCheckPassed = true;
            }
            else
            {
                FilesCheckPassed = false;
                MessageBox.Show("Files Are Broken/Missing");
            }
        }
        public static void CheckForFilesNoInternet()
        {

        }
        public static void FixFiles()
        {

        }
    }
}
