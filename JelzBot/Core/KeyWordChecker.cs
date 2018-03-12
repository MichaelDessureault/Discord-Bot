using Discord.Commands;
using Discord.WebSocket;
using JelzBot.Core.Extensions;
using JelzBot.Core.Giphy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JelzBot.Core {
    internal static class KeyWordChecker {
        private static Dictionary<string, string> keyToWords;
        private static Dictionary<string, string> keyToGif;
        
        public static bool KeyCheckerEnabled       { get; set; } = true;
        public static bool KeyToWordCheckerEnabled { get; set; } = true;
        public static bool KeyToGifCheckerEnabled  { get; set; } = true;
        public static bool KeyToLolCheckerEnabled  { get; set; } = true;
        public static bool MultipleWordsPerInput   { get; set; } = false;

        internal static Task KeyWordChecker_Ready () {
            Console.WriteLine("KeyWordChecker Starting");

            keyToWords = DataStorage.LoadData<Dictionary<string, string>>(DataStorage.KKeyToWordsFile);
            keyToGif = DataStorage.LoadData<Dictionary<string, string>>(DataStorage.KKeyToGifFile);

            if (keyToWords == null)
                keyToWords = new Dictionary<string, string>();

            if (keyToGif == null)
                keyToGif = new Dictionary<string, string>();

            return Task.CompletedTask;
        }

        internal static async Task CheckMessageForKeyWord (string messageContent, SocketCommandContext context) {
            if (!KeyCheckerEnabled)
                return;

            SocketTextChannel channel = context.Channel as SocketTextChannel;

            if (KeyToWordCheckerEnabled)
                await CheckForKeyToWordsKeyWord(messageContent, channel);

            if (KeyToGifCheckerEnabled)
                await CheckForKeyToGifKeyWord(messageContent, channel);

            if (KeyToLolCheckerEnabled)
                await CheckForLolOrLamo(messageContent, context.User, channel);
        }

        private static async Task CheckForKeyToWordsKeyWord (string messageContent, SocketTextChannel channel) {
            foreach (var item in keyToWords) {
                if (messageContent.Contains(item.Key, true)) {
                    await Global.SendMessageAsync(item.Value, channel);

                    if (MultipleWordsPerInput)
                        return;
                }
            }
        }

        private static async Task CheckForKeyToGifKeyWord (string messageContent, SocketTextChannel channel) {
            foreach (var item in keyToGif) {
                if (messageContent.Contains(item.Key, true)) {
                    await Gif.GetRandomGif(item.Key, channel);

                    if (MultipleWordsPerInput)
                        return;
                }
            }
        }

        private static async Task CheckForLolOrLamo (string messageContent, SocketUser user, SocketTextChannel channel) {
            if (messageContent.Contains("lol", true) || messageContent.Contains("lmao", true)) {
                await Global.SendMessageAsync($"You think you're funny {user.Username}?", channel);
            }
        }

        #region key to words

        internal static string AddOrUpdateKey_KeyToWords (string key, string word) {
            key = key.ToLower();
            return (keyToWords.AddOrUpdateKey(key, word, UpdateKeyToWordsFile)) ? $"{key} has been added" : $"Failed to add {key}";
        }

        internal static string RemoveKey_KeyToWords (string key) {
            key = key.ToLower();
            return (keyToWords.RemoveKey(key, UpdateKeyToWordsFile)) ? "Successfully removed" : $"Failed to remove {key}";
        }

        internal static string ShowAll_KeyToWords () {
            return keyToWords.GetAllKeysAndValuesToString();
        }

        internal static void Clear_KeyToWords () {
            keyToWords.Clear();
            UpdateKeyToWordsFile();
        }

        private static void UpdateKeyToWordsFile () {
            DataStorage.SaveData(keyToWords, DataStorage.KKeyToWordsFile);
        }
        
        #endregion

        #region key to gif
        internal static string AddOrUpdateKey_KeyToGif (string key) {
            key = key.ToLower();
            return (keyToGif.AddOrUpdateKey(key, key, UpdateKeyToGifFile)) ? $"{key} has been added" : $"Failed to add {key}";
        }

        internal static string RemoveKey_KeyToGif (string key) {
            key = key.ToLower();
            return (keyToGif.RemoveKey(key, UpdateKeyToGifFile)) ? "Successfully removed" : $"Failed to remove {key}";
        }

        internal static string ShowAll_KeyToGif () {
            return keyToGif.GetAllKeysToString();
        }

        internal static void Clear_KeyToGif () {
            keyToGif.Clear();
            UpdateKeyToGifFile();
        }

        private static void UpdateKeyToGifFile () {
            DataStorage.SaveData(keyToGif, DataStorage.KKeyToGifFile);
        }
        #endregion
    }
}
