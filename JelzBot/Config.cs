using System.IO;
using Newtonsoft.Json;

namespace JelzBot {
    static class Config {
        #region const fileLocations
        private const string KConfigFolder           = "Resources";
        private const string KDiscord_ConfigFile     = "discordconfig.json";
        private const string KTwitter_ConfigFile     = "twitterconfig.json";
        private const string KGiphy_ConfigFile       = "giphyconfig.json";
        #endregion

        internal static DiscordConfig DiscordBotConfig { get; private set; }
        internal static TwitterConfig TwitterBotConfig { get; private set; }
        internal static GiphyConfig   GiphyBotConfig   { get; private set; }

        static Config () {
            if (!Directory.Exists(KConfigFolder))
                Directory.CreateDirectory(KConfigFolder);

            DiscordBotConfig = Setup<DiscordConfig>(KConfigFolder + "/" + KDiscord_ConfigFile);
            TwitterBotConfig = Setup<TwitterConfig>(KConfigFolder + "/" + KTwitter_ConfigFile);
            GiphyBotConfig   = Setup<GiphyConfig>  (KConfigFolder + "/" + KGiphy_ConfigFile);
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
            public ulong  channel_id;
        }

        internal struct GiphyConfig { 
            public string key;
        }
    }
}
