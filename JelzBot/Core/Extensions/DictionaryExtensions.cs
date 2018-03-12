using System;
using System.Collections.Generic;

namespace JelzBot.Core.Extensions {
    public static class DictionaryExtensions {
        /// <summary>
        /// Adds or Updates the Key and Word in the dictionary
        /// </summary>
        /// <returns>True if successful, False if failed</returns>
        public static bool AddOrUpdateKey<T, T2> (this Dictionary<T, T2> dictionary, T key, T2 word, Action action = null) {
            try {
                if (dictionary.ContainsKey(key))
                    dictionary[key] = word;
                else
                    dictionary.Add(key, word);

                action?.Invoke(); 
                return true;
            } catch (Exception) {
                return false;
            }
        }

        /// <summary>
        /// Removes the key from the dictionary
        /// </summary>
        /// <returns>True if it was removed, False if failed </returns>
        public static bool RemoveKey<T, T2> (this Dictionary<T, T2> dictionary, T key, Action action = null) {
            if (dictionary.ContainsKey(key)) {
                dictionary.Remove(key);
                action?.Invoke();
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Gets all keys and values and returns them in a string
        /// </summary>
        public static string GetAllKeysAndValuesToString<T, T2> (this Dictionary<T, T2> dictionary) {
            string content = "";
            foreach (var item in dictionary) {
                content += $"'{item.Key}' : '{item.Value}'\n";
            }
            return content;
        }

        /// <summary>
        /// Gets all the keys in the dictionary and returns them in a string
        /// </summary>
        public static string GetAllKeysToString<T, T2> (this Dictionary<T, T2> dictionary) {
            string content = "Keys: ";
            foreach (var item in dictionary) {
                content += $"'{item.Key}' ";
            }
            return content;
        }
    }
}
