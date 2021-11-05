using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace AGG_Productions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Chaotic_Click(object sender, RoutedEventArgs e)
        {
            NoGame.Visibility = Visibility.Collapsed;
            SelectGame.Visibility = Visibility.Collapsed;
            var dialog = new CommonOpenFileDialog();
            dialog.Title = "Select Install Directory";
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string folder = dialog.FileName;
                File.WriteAllText("WriteText.txt", folder);
                Console.Write("{0}", dialog.FileName);
                // Do something with selected folder string
            }
        }
    }
}
