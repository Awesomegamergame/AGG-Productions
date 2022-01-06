using System;
using System.IO;
using System.Diagnostics;

namespace AGG_Productions.LauncherUpgrade
{
    class UpgradeLauncher
    {
        public static string ChaoticLauncherFolder = @"C:\Users\Public\Chaotic Launcher";
        public static string ChaoticDevLauncherFolder = @"C:\Users\Public\Chaotic Development Launcher";
        //Auto Delete uses the unistaller's from both launchers to remove all folders and icons
        //If fails it goes to manual unistall mode which removes manualy every folder, icon, and chaotic releated file
        public static void OldLauncherCheck()
        {
            if (Directory.Exists(ChaoticLauncherFolder) || Directory.Exists(ChaoticDevLauncherFolder))
            {
                DeleteOld();
            }
            else
                return;
        }
        public static void DeleteOld()
        {
            #region All Possible Chaotic Launcher Locations
            string ChaoticLauncherUninstaller = @"C:\Users\Public\Chaotic Launcher\unins000.exe";
            string ChaoticLauncherUninstallerDat = @"C:\Users\Public\Chaotic Launcher\unins000.dat";
            string ChaoticLauncherUninstaller2 = @"C:\Users\Public\Chaotic Launcher\unins001.exe";
            string ChaoticLauncherUninstaller2Dat = @"C:\Users\Public\Chaotic Launcher\unins001.dat";
            string ChaoticDevLauncherUninstaller = @"C:\Users\Public\Chaotic Development Launcher\unins000.exe";
            string ChaoticDevLauncherUninstallerdat = @"C:\Users\Public\Chaotic Development Launcher\unins000.dat";
            string ChaoticDevLauncherUninstaller2dat = @"C:\Users\Public\Chaotic Development Launcher\unins000.dat";
            string ChaoticDevLauncherUninstaller2 = @"C:\Users\Public\Chaotic Development Launcher\unins001.exe";
            string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string ChaoticLauncherSMIcon = Path.Combine(Appdata, @"Microsoft\Windows\Start Menu\Programs\Chaotic Launcher");
            string ChaoticDevLauncherSMIcon = Path.Combine(Appdata, @"Microsoft\Windows\Start Menu\Programs\Chaotic Development Launcher");
            string ChaoticDesktopIcon = Path.Combine(Desktop, @"Desktop\Chaotic Launcher.lnk");
            string ChaoticDevDesktopIcon = Path.Combine(Desktop, @"Desktop\Chaotic Development Launcher.lnk");
            #endregion
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
