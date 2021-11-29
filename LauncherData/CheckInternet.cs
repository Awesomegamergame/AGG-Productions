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
                using (var client = new WebClient())
                using (var stream = client.OpenRead("https://www.google.com"))
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
