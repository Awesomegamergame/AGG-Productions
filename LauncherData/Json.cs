using System.IO;
using Newtonsoft.Json.Linq;

namespace AGG_Productions.LauncherData
{
    class Json
    {
        public static void CreateJson()
        {
            JObject rss = new JObject(new JProperty("GameDirs", new JObject()));
            File.WriteAllText("GameDirs.json", rss.ToString());
        }
        public static void UpdateJson(string GameName, string Path)
        {
            string json = File.ReadAllText("GameDirs.json");
            JObject rss = JObject.Parse(json);
            JObject GameDirs = (JObject)rss["GameDirs"];
            JToken token = GameDirs[GameName];
            if (token != null)
                GameDirs.Property(GameName).Remove();
            GameDirs.Add(new JProperty(GameName, Path));
            File.WriteAllText("GameDirs.json", rss.ToString());
        }
        public static string ReadJson(string GameName)
        {
            JObject rss = JObject.Parse(File.ReadAllText("GameDirs.json"));
            return (string)rss["GameDirs"][GameName];
        }
        public static bool DataCheck(string GameName)
        {
            JObject rss = JObject.Parse(File.ReadAllText("GameDirs.json"));
            JObject GameDirs = (JObject)rss["GameDirs"];
            JToken token = GameDirs[GameName];
            if (token != null)
                return true;
            return false;
        }
    }
}
