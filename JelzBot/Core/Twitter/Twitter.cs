using System;
using System.Collections.Generic;
using Tweetinvi;
using Tweetinvi.Models;
using System.Threading.Tasks;
using Tweetinvi.Events;
using Tweetinvi.Streaming;
using Discord.WebSocket;
using System.Threading;
using JelzBot.Core.Extensions;

namespace JelzBot.Core.Twitter {
    class Twitter {
        private static IAuthenticatedUser _user;
        private static IUserStream _stream;

        private static SocketTextChannel _channel;
        private static Thread _twitterThread;
        
        private static Dictionary<string, string> _ignoreTweetsBy;
        private static Dictionary<string, string> _twitterKeyWords;

        public static bool TweetReplys { get; set; } = false;
        
        internal static Task Twitter_Ready () {
            Console.WriteLine("Twitter Starting");
            var twitterBotConfig = Config.TwitterBotConfig;

            // Authentication 
            var credentails = Auth.SetUserCredentials(
                twitterBotConfig.customer_key, 
                twitterBotConfig.customer_key_secret, 
                twitterBotConfig.access_token, 
                twitterBotConfig.access_token_secret
            );

            // Get information about user
            _user = User.GetAuthenticatedUser(credentails);
            _channel = Global.Client.GetChannel(twitterBotConfig.channel_id) as SocketTextChannel;

            if (_channel == null)
                Console.WriteLine("Warrning!! Channel id not found for twitterbot (update the config files channel_id)");

            _ignoreTweetsBy  = DataStorage.LoadData<Dictionary<string, string>>(DataStorage.KIgnoreTweetsByFile);
            _twitterKeyWords = DataStorage.LoadData<Dictionary<string, string>>(DataStorage.KTwitterKeyWordsFile);

            if (_ignoreTweetsBy == null)
                _ignoreTweetsBy  = new Dictionary<string, string>();

            if (_twitterKeyWords == null)
                _twitterKeyWords = new Dictionary<string, string>();
            
            var output = StartTwitterThread();
            
            return Task.CompletedTask;
        }
        
        public static string StartTwitterThread () {
            if (_twitterThread != null && _twitterThread.ThreadState == ThreadState.Running)
                return "Already listening to twitter";

            _twitterThread = new Thread(() => {
                _stream = Stream.CreateUserStream();
                _stream.AddTweetLanguageFilter(LanguageFilter.English);
                _stream.TweetCreatedByFriend += FriendTweeted;
                _stream.StartStream();
            });

            _twitterThread.Start();
            return "Started listening to twitter";
        }

        public static string StopTwitterThread () {
            if (_twitterThread.ThreadState == ThreadState.Aborted)
                return "The twitter bot is not running";

            _twitterThread.Abort();
            return "The twitter bot has stopped";
        }

        private static void FriendTweeted (object sender, TweetReceivedEventArgs e) {
            Console.WriteLine(e.Tweet.CreatedBy.Name + " has just tweeted: " + e.Tweet.Text);

            if (IsInIgnoreList(e.Tweet.CreatedBy.ScreenName))
                return;

            var replayTo = e.Tweet.InReplyToScreenName;
            // if the replayTo is not null and tweetReplays is false then leave
            if (replayTo != null && !TweetReplys)
                return;

            var message = e.Tweet.Text;
            foreach (var item in _twitterKeyWords) {
                if (message.Contains(item.Key, false, true)) {
                    Global.SendMessageAsync(e.Tweet.Url, _channel);
                    break;
                }
            }
        }

        private static bool IsInIgnoreList (string screenName) {
            return (_ignoreTweetsBy.ContainsKey(screenName));
        }
        
        public static bool Follow (string screenName) {
            return (_user.FollowUser(screenName));
        }

        public static bool UnFollow (string screenName) {
            return (_user.UnFollowUser(screenName));
        }
        
        #region TwitterKeyWords Methods
        internal static string AddOrUpdateKey_TwitterKeyWords (string key) {
            key = key.ToLower();
            return (_twitterKeyWords.AddOrUpdateKey(key, key, UpdateTwitterKeyWordsFile)) ? $"{key} has been added" : $"Failed to add {key}";
        }

        internal static string RemoveKey_TwitterKeyWord (string key) {
            key = key.ToLower();
            return (_twitterKeyWords.RemoveKey(key, UpdateTwitterKeyWordsFile)) ? "Successfully removed" : $"Failed to remove {key}";
        }

        internal static string ShowAll_TwitterKeyWords () {
            return _twitterKeyWords.GetAllKeysToString();
        }

        internal static void Clear_TwitterKeyWords () {
            _twitterKeyWords.Clear();
            UpdateTwitterKeyWordsFile();
        }

        private static void UpdateTwitterKeyWordsFile () {
            DataStorage.SaveData(_twitterKeyWords, DataStorage.KTwitterKeyWordsFile);
        }
        #endregion

        #region IgnoreTweetsBy Methods
        public static string IgnoreTweetsBy (string screenName) {
            var friendIds = _user.GetFriendIds();
            foreach (var id in friendIds) {
                if (User.GetUserFromId(id).ScreenName.Equals(screenName)) {
                    _ignoreTweetsBy.AddOrUpdateKey(screenName, screenName, UpdateIgnoreTweetsFile);
                    return $"{screenName} will now be ignored when they post";
                }
            }
            return $"{screenName} was not found or not being followed";
        }

        internal static string RemoveKey_IgnoreTweetsBy (string key) {
            key = key.ToLower();
            return (_ignoreTweetsBy.RemoveKey(key, UpdateIgnoreTweetsFile)) ? "Successfully removed" : $"Failed to remove {key}";
        }

        internal static string ShowAll_IgnoreTweetsBy () {
            return _ignoreTweetsBy.GetAllKeysToString();
        }

        internal static void Clear_IgnoreTweetsBy () {
            _ignoreTweetsBy.Clear();
            UpdateTwitterKeyWordsFile();
        }

        private static void UpdateIgnoreTweetsFile () {
            DataStorage.SaveData(_ignoreTweetsBy, DataStorage.KIgnoreTweetsByFile);
        }
        #endregion
    }
}
