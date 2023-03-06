using System.Net;

namespace AGG_Productions
{
    class CheckInternet
    {
        public static bool IsOnline;
        public static void CheckInternetState()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Proxy = null;
                using (var stream = wc.OpenRead("https://www.google.com"))
                {
                    IsOnline = true;
                }
            }
            catch
            {
                IsOnline = false;
            }
        }
    }
}
