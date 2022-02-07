using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Environment;
using System.Threading.Tasks;

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
