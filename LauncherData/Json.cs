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
        public static string ReadAndCreate(string GameName, string Link)
        {
            JObject rss = new JObject(new JProperty("Games", new JObject()));
            File.WriteAllText("Games.json", rss.ToString());
            string json = File.ReadAllText("Games.json");
            JObject rss2 = JObject.Parse(json);
            JObject GameDirs = (JObject)rss2["Games"];
            JToken token = GameDirs[GameName];
            if (token != null)
                GameDirs.Property(GameName).Remove();
            GameDirs.Add(new JProperty(GameName, Link));
            File.WriteAllText("Games.json", rss2.ToString());
            return Link;
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
            return (string)rss["Games"][GameName];
        }
        public static string ReadJsonLink(string Link, string FileName)
        {
            JObject rss = JObject.Parse(File.ReadAllText($"{FileName}.json"));
            return (string)rss[Link];
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
