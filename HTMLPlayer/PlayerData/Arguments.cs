namespace HTMLPlayer.PlayerData
{
    class Arguments
    {
        public static string Gamename;
        public static string GameDir;
        public static string Version;
        public static void ProcessArguments(string arg1, string arg2, string arg3)
        {
            Gamename = arg1;
            GameDir = arg2;
            Version = arg3;
        }
    }
}