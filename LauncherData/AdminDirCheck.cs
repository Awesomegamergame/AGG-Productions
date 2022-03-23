using System;
using System.IO;
using System.Windows;
using AGG_Productions.LauncherData;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace AGG_Productions
{
    public class AdminDirCheck
    {
        private const string AdminCheck = "AdminCheck";
        private const string AdminCheckName = "AdminCheck.txt";
        public static bool FileDialogClosed = false;
        public static void InstallDir(string InstallName)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                Title = "Select Install Directory",
                IsFolderPicker = true
            };
        Start:
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string folder = dialog.FileName;
                try
                {
                    File.WriteAllText(Path.Combine(folder, AdminCheckName), AdminCheck);
                    File.Delete(Path.Combine(folder, AdminCheckName));
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Application Isnt Ran With Admin Choose A Folder That Doesnt Need Admin");
                    goto Start;
                }
                Json.UpdateJson(InstallName, folder);
            }
            else
            {
                FileDialogClosed = true;
            }
        }
    }
}