using System;
using System.Windows;
using Microsoft.Web.WebView2.Wpf;
using HTMLPlayer.PlayerData;

namespace HTMLPlayer
{
    public partial class MainWindow : Window
    {
        public static WebView2 WebControlObject;
        public MainWindow()
        {
            InitializeComponent();
            GamePaths paths = new GamePaths(Arguments.Version, Arguments.Gamename, Arguments.GameDir);
            WebControlObject.Source = new Uri(paths.HTMLFile);
        }
        private void WebControl_Initialized(object sender, EventArgs e)
        {
            WebControlObject = (WebView2)sender;
        }
    }
}
