using Newtonsoft.Json;
using System.IO;

namespace JelzBot.Core {
    static class DataStorage {
        public const string KKeyToWordsFile = "JSON/KeyToWords.json";
        public const string KKeyToGifFile = "JSON/KeyToGif.json";
        public const string KTwitterKeyWordsFile = "JSON/TwitterKeyWordsFile.json";
        public const string KIgnoreTweetsByFile = "JSON/IgnoreTweetsBy.json";

        public static void SaveData<T> (T data, string file) {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllTextAsync(file, json);
        }

        public static T LoadData<T> (string file) {
            if (!ValidateFile(file))
                return default(T);

            string json = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private static bool ValidateFile (string file) {
            return (File.Exists(file));
        }
    }
}
