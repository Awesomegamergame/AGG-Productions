using System.IO;
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
        public static bool FilesCheckPassedNo;
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
            }
        }
        public static void CheckForFilesNoInternet()
        {
            if (File.Exists(LauncherExe) && File.Exists(LauncherConfig) && File.Exists(Launcherpdb) && File.Exists(CodePackDLL) && File.Exists(ShellDLL) && File.Exists(ShellXML) && File.Exists(CodePackXML))
            {
                FilesCheckPassedNo = true;
            }
            else
            {
                FilesCheckPassedNo = false;
                MessageBox.Show("Files Are Broken/Missing But Cant Fix Files Because You Have No Internet");
            }
        }
    }
}
