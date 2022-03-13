using System.IO;

namespace HTMLPlayer.PlayerData
{
    class GamePaths
    {
        public string HTMLFile;
        public string GamesDirectory;
        public string EmptyFolder;
        public string GameVersionFile;

        public GamePaths(string Version, string GameName, string GameDir)
        {
            GamesDirectory = Path.Combine(GameDir, $"{GameName}");
            EmptyFolder = Path.Combine(GamesDirectory, "Build");
            GameVersionFile = Path.Combine(GamesDirectory, $"Build {Version}");
            HTMLFile = Path.Combine(GameVersionFile, GameName, $"{GameName}.html");

            if (!Directory.Exists(GamesDirectory))
            {
                Directory.CreateDirectory(GamesDirectory);
            }
            if (!Directory.Exists(GameVersionFile))
            {
                Directory.CreateDirectory(GameVersionFile);
            }
            if (Directory.Exists(EmptyFolder))
            {
                Directory.Delete(EmptyFolder);
            }
        }
    }
}
