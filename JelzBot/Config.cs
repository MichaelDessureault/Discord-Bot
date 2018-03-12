using System.IO;
using Newtonsoft.Json;

namespace JelzBot {
    static class Config {
        #region const fileLocations
        private const string configFolder           = "Resources";
        private const string discord_configFile     = "discordconfig.json";
        private const string twitter_configFile     = "twitterconfig.json";
        private const string giphy_configFile       = "giphyconfig.json";
        private const string cleverbot_configFile   = "cleverbotconfig.json";
        #endregion

        internal static DiscordConfig discordBot;
        internal static TwitterConfig twitterBot;
        internal static GiphyConfig giphyBot;

        static Config () {
            if (!Directory.Exists(configFolder))
                Directory.CreateDirectory(configFolder);

            discordBot = Setup<DiscordConfig>(configFolder + "/" + discord_configFile);
            twitterBot = Setup<TwitterConfig>(configFolder + "/" + twitter_configFile);
            giphyBot   = Setup<GiphyConfig>  (configFolder + "/" + giphy_configFile);
        }

        static T Setup<T> (string file) {
            T obj = default(T);

            if (!File.Exists(file)) {
                var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                File.WriteAllText(file, json);
            } else {
                string json = File.ReadAllText(file);
                obj = JsonConvert.DeserializeObject<T>(json);
            }

            return obj;
        }

        internal struct DiscordConfig {
            public string token;
            public string cmdPrefix;
            public string cmdPrefix2;
        }

        internal struct TwitterConfig {
            public string customer_key;
            public string customer_key_secret;
            public string access_token;
            public string access_token_secret;
        }

        internal struct GiphyConfig { 
            public string key;
        }
    }
}
