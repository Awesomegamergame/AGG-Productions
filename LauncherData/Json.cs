using System.IO;
using Newtonsoft.Json.Linq;

namespace AGG_Productions.LauncherData
{
    class Json
    {
        public static string BJsonLink = "https://raw.githubusercontent.com/awesomegamergame/AGG-Productions/Convert-To-Json/Webdata/Updates.json";
        public static string GJsonLink = "https://raw.githubusercontent.com/awesomegamergame/AGG-Productions/Convert-To-Json/Webdata/Games.json";
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
        public static string ReadJson(string GameName, string FileName)
        {
            JObject rss = JObject.Parse(File.ReadAllText($"{FileName}.json"));
            return (string)rss[FileName][GameName];
        }
        public static string ReadGameJson(string GVersion, string Property, string FileName, string TopLevel)
        {
            JObject rss = JObject.Parse(File.ReadAllText($"{FileName}.json"));
            return (string)rss[TopLevel][GVersion][Property];
        }
        public static bool DataCheck(string GameName)
        {
            if (File.Exists("GameDirs.json"))
            {
                JObject rss = JObject.Parse(File.ReadAllText("GameDirs.json"));
                JObject GameDirs = (JObject)rss["GameDirs"];
                JToken token = GameDirs[GameName];
                if (token != null)
                    return true;
                return false;
            }
            else
            {
                CreateJson();
                return false;
            }
        }
    }
}
