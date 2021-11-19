using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace AGG_Productions
{
    class AdminDirCheck
    {
        #region Variables
        const string AdminCheck = "AdminCheck";
        const string AdminCheckName = "AdminCheck.txt";
        #endregion

        public static void InstallDir(string InstallName)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                Title = "Select Install Directory",
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string folder = dialog.FileName;
                File.WriteAllText(InstallName + "Dir.txt", folder);
                try
                {
                    File.WriteAllText(Path.Combine(folder, AdminCheckName), AdminCheck);
                    File.Delete(Path.Combine(folder, AdminCheckName));
                }
                catch (UnauthorizedAccessException)
                {
                    File.Delete(InstallName + "Dir.txt");
                    MessageBox.Show("Application Isnt Ran With Admin Choose A Folder That Doesnt Need Admin");
                }

            }
        }
    }
}
