using System.IO;
using static System.Environment;
using Newtonsoft.Json.Linq;

namespace AGG_Productions.LauncherData
{
    class Json
    {
        public static string BDataLink = "https://raw.githubusercontent.com/awesomegamergame/AGG-Productions/dev/Webdata/ButtonData.json";
        public static void CreateJson()
        {
            JObject rss = new JObject(new JProperty("GameDirs", new JObject()));
            File.WriteAllText($@"{CurrentDirectory}\Cache\GameDirs.json", rss.ToString());
        }
        public static void UpdateJson(string GameName, string Path)
        {
            string json = File.ReadAllText($@"{CurrentDirectory}\Cache\GameDirs.json");
            JObject rss = JObject.Parse(json);
            JObject GameDirs = (JObject)rss["GameDirs"];
            JToken token = GameDirs[GameName];
            if (token != null)
                GameDirs.Property(GameName).Remove();
            GameDirs.Add(new JProperty(GameName, Path));
            File.WriteAllText($@"{CurrentDirectory}\Cache\GameDirs.json", rss.ToString());
        }
        public static string ReadJson(string GameName)
        {
            JObject rss = JObject.Parse(File.ReadAllText($@"{CurrentDirectory}\Cache\GameDirs.json"));
            return (string)rss["GameDirs"][GameName];
        }
        public static string ReadGameJson(string GVersion, string Property, string FileName, string TopLevel)
        {
            JObject rss = JObject.Parse(File.ReadAllText($@"{CurrentDirectory}\Cache\Games\{FileName}.json"));
            return (string)rss[TopLevel][GVersion][Property];
        }
        public static string ReadGameVerJson(string GVersion, string Property, string FileName, string TopLevel)
        {
            JObject rss = JObject.Parse(File.ReadAllText($@"{CurrentDirectory}\Cache\{FileName}.json"));
            return (string)rss[TopLevel][GVersion][Property];
        }
        public static bool DataCheck(string GameName)
        {
            if (File.Exists($@"{CurrentDirectory}\Cache\GameDirs.json"))
            {
                JObject rss = JObject.Parse(File.ReadAllText($@"{CurrentDirectory}\Cache\GameDirs.json"));
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
