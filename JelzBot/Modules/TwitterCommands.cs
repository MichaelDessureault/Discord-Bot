using Discord;
using Discord.Commands;
using JelzBot.Core.Twitter;
using System.Threading.Tasks;

namespace JelzBot.Modules {

    [Group("twitter")]
    public class TwitterCommands : ModuleBase<SocketCommandContext> {
        [Command("commands")]
        public async Task Commands () {
            var embed = new EmbedBuilder();

            embed.WithTitle("Twitter Commands");
            embed.AddField("start", "Starts listening to twitter");
            embed.AddField("stop", "Stops listening to twitter");
            embed.AddField("follow <string>", "Follow a new twitter account");
            embed.AddField("unfollow <string>", "Unfollow a twitter account (Admin only command)");
            embed.AddField("togglereplys", "Toggles if the bot displays messages when a followed account reply's to a tweet");
            embed.AddField("addkey <string>", "Add twitter key word");
            embed.AddField("removekey <string>", "Removes twitter key word");
            embed.AddField("showallkeys", "Shows all twitter key words");
            embed.AddField("clearallkeys", "Removes all twitter key words");
            embed.AddField("addignored <string>", "Ignores tweets from a followed twitter account");
            embed.AddField("remoreignored <string>", "Removes the ignored account name");
            embed.AddField("showallignored", "Shows all ignored accounts");
            embed.AddField("clearignored", "Clears ignored list");
            embed.WithColor(Color.Blue);

            await ReplyAsync("", false, embed.Build());
        }

        [Command("start")]
        public async Task Start () { 
            var output = Twitter.StartTwitterThread();
            await ReplyAsync(output);
        }

        [Command("stop")]
        public async Task Stop () {
            var output = Twitter.StopTwitterThread();
            await ReplyAsync(output);
        }

        [Command("follow")]
        public async Task Follow (string screenName) {
            bool followed = Twitter.Follow(screenName);
            await ReplyAsync((followed) ? $"Successfully followed {screenName}" : $"Failed to follow {screenName}");
        }

        [Command("unfollow"), RequireOwner]
        public async Task UnFollow (string screenName) {
            bool unfollowed = Twitter.UnFollow(screenName);
            await ReplyAsync((unfollowed) ? $"Successfully unfollowed {screenName}" : $"Failed to unfollow {screenName}");
        }

        [Command("togglereplys")]
        public async Task ToggleReplys () {
            Twitter.TweetReplys = !Twitter.TweetReplys;
            await ReplyAsync("Toggled");
        }

        #region TwitterKeyWords Commands
        [Command("addkey")]
        public async Task AddOrUpdate_TwitterKeyWords (string key, [Remainder] string word = null) {
            var output = Twitter.AddOrUpdateKey_TwitterKeyWords(key);
            await ReplyAsync(output);
        }

        [Command("removekey")]
        public async Task RemoveKey_TwitterKeyWords (string key) {
            var output = Twitter.RemoveKey_TwitterKeyWord(key);
            await ReplyAsync(output);
        }

        [Command("showallkeys")]
        public async Task ShowAll_TwitterKeyWords () {
            var output = Twitter.ShowAll_TwitterKeyWords();
            await ReplyAsync(output);
        }

        [Command("clearallkeys")]
        public async Task ClearAll_TwitterKeyWords () {
            Twitter.Clear_TwitterKeyWords();
            await ReplyAsync("Cleared");
        }
        #endregion
        
        #region IgnoreTweetsBy Commands
        [Command("addignored")]
        public async Task IgnoreTweetsBy (string screenName, [Remainder] string word = null) {
            var output = Twitter.IgnoreTweetsBy(screenName);
            await ReplyAsync(output);
        }

        [Command("removeignored")]
        public async Task RemoveIgnoreTweetsBy (string screenName) {
            var output = Twitter.RemoveKey_IgnoreTweetsBy(screenName);
            await ReplyAsync(output);
        }
        
        [Command("showallignored")]
        public async Task ShowAllIgnoredTweets () {
            var output = Twitter.ShowAll_IgnoreTweetsBy();
            await ReplyAsync(output);
        }

        [Command("clearignored")]
        public async Task ClearIgnoredTweets () {
            Twitter.Clear_IgnoreTweetsBy();
            await ReplyAsync("Cleared");
        }
        #endregion
    }
}
