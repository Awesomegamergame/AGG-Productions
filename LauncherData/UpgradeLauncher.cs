using System;
using System.IO;
using System.Diagnostics;

namespace AGG_Productions.LauncherData
{
    class UpgradeLauncher
    {
        #region All Possible Chaotic Launcher Locationss
        public static string ChaoticLauncherUninstaller = @"C:\Users\Public\Chaotic Launcher\unins000.exe";
        public static string ChaoticLauncherUninstallerDat = @"C:\Users\Public\Chaotic Launcher\unins000.dat";
        public static string ChaoticLauncherUninstaller2 = @"C:\Users\Public\Chaotic Launcher\unins001.exe";
        public static string ChaoticLauncherUninstaller2Dat = @"C:\Users\Public\Chaotic Launcher\unins001.dat";
        public static string ChaoticLauncherFolder = @"C:\Users\Public\Chaotic Launcher";
        public static string ChaoticDevLauncherUninstaller = @"C:\Users\Public\Chaotic Development Launcher\unins000.exe";
        public static string ChaoticDevLauncherUninstallerdat = @"C:\Users\Public\Chaotic Development Launcher\unins000.dat";
        public static string ChaoticDevLauncherUninstaller2dat = @"C:\Users\Public\Chaotic Development Launcher\unins000.dat";
        public static string ChaoticDevLauncherUninstaller2 = @"C:\Users\Public\Chaotic Development Launcher\unins001.exe";
        public static string ChaoticDevLauncherFolder = @"C:\Users\Public\Chaotic Development Launcher";
        public static string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string ChaoticLauncherSMIcon = Path.Combine(Appdata, @"Microsoft\Windows\Start Menu\Programs\Chaotic Launcher");
        public static string ChaoticDevLauncherSMIcon = Path.Combine(Appdata, @"Microsoft\Windows\Start Menu\Programs\Chaotic Development Launcher");
        public static string ChaoticDesktopIcon = Path.Combine(Desktop, @"Desktop\Chaotic Launcher.lnk");
        public static string ChaoticDevDesktopIcon = Path.Combine(Desktop, @"Desktop\Chaotic Development Launcher.lnk");
        #endregion

        //Auto Delete uses the unistaller's from both launchers to remove all folders and icons
        //If fails it goes to manual unistall mode which removes manualy every folder, icon, and chaotic releated file
        public static void DeleteOld()
        {
            if (File.Exists(ChaoticLauncherUninstaller) && File.Exists(ChaoticLauncherUninstallerDat))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = ChaoticLauncherUninstaller,
                    Arguments = @"/verysilent"
                };
                var process = Process.Start(startInfo);
                process.WaitForExit();
            }
            if (File.Exists(ChaoticDevLauncherUninstaller) && File.Exists(ChaoticDevLauncherUninstallerdat))
            {
                ProcessStartInfo startInfo2 = new ProcessStartInfo
                {
                    FileName = ChaoticDevLauncherUninstaller,
                    Arguments = @"/verysilent"
                };
                var process = Process.Start(startInfo2);
                process.WaitForExit();
            }
            if (File.Exists(ChaoticLauncherUninstaller2) && File.Exists(ChaoticLauncherUninstaller2Dat))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = ChaoticLauncherUninstaller2,
                    Arguments = @"/verysilent"
                };
                var process = Process.Start(startInfo);
                process.WaitForExit();
            }
            if (File.Exists(ChaoticDevLauncherUninstaller2) && File.Exists(ChaoticDevLauncherUninstaller2dat))
            {
                ProcessStartInfo startInfo2 = new ProcessStartInfo
                {
                    FileName = ChaoticDevLauncherUninstaller2,
                    Arguments = @"/verysilent"
                };
                var process = Process.Start(startInfo2);
                process.WaitForExit();
            }
            if (Directory.Exists(ChaoticLauncherFolder))
            {
                DeleteDirectory(ChaoticLauncherFolder);
            }
            if (Directory.Exists(ChaoticDevLauncherFolder))
            {
                DeleteDirectory(ChaoticDevLauncherFolder);
            }
            if (Directory.Exists(ChaoticLauncherSMIcon))
            {
                DeleteDirectory(ChaoticLauncherSMIcon);
            }
            if (Directory.Exists(ChaoticDevLauncherSMIcon))
            {
                DeleteDirectory(ChaoticDevLauncherSMIcon);
            }
            if (File.Exists(ChaoticDesktopIcon))
            {
                File.Delete(ChaoticDesktopIcon);
            }
            if (File.Exists(ChaoticDevDesktopIcon))
            {
                File.Delete(ChaoticDevDesktopIcon);
            }
        }
        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
    }
}
