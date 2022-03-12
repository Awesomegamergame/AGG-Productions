using System;
using System.Windows;
using Microsoft.Web.WebView2.Wpf;

namespace HTMLPlayer
{
    public partial class MainWindow : Window
    {
        public static WebView2 WebControlObject;
        public MainWindow()
        {
            InitializeComponent();
            WebControlObject.Source = new Uri($@"{Environment.CurrentDirectory}\{Arguments.Gamename}.html");
        }

        private void WebControl_Initialized(object sender, EventArgs e)
        {
            WebControlObject = (WebView2)sender;
        }
    }
}
