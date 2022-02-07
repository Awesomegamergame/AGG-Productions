using System.IO;
using static System.Environment;

namespace AGG_Productions.LauncherFunctions
{
    class Cache
    {
        public static void Create()
        {
            if (!Directory.Exists("Cache"))
                Directory.CreateDirectory("Cache");
            if (!Directory.Exists($@"{CurrentDirectory}\Cache\Images"))
                Directory.CreateDirectory($@"{CurrentDirectory}\Cache\Images");
        }
    }
}
