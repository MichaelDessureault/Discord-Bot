using System;

namespace JelzBot.Core.Extensions {
    public static class StringExtensions {
        public static bool Contains (this String str, string word, bool exact, bool allowrepeated = false) {
            str = str.ToLower();
            word = word.ToLower();

            if (exact) {
                string[] wordsArry = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < wordsArry.Length; i++) {
                    if (wordsArry[i].Equals(word)) {
                        if (!allowrepeated && i != wordsArry.Length - 1) {
                            // if allowreapted is disabled and a word is repeated then it returns false
                            return !(wordsArry[i + 1].Equals(word));
                        }
                        return true;
                    }
                }
            } else {
                return str.Contains(word);
            }

            return false;
        }
    }
}
