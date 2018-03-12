using Discord;
using Discord.Commands;
using Discord.WebSocket;
using JelzBot.Core;
using JelzBot.Interface;
using System.Threading.Tasks;

namespace JelzBot.Modules {

    [Group("keytowords")]
    public class KeyToWordsCommands : ModuleBase<SocketCommandContext>, IDictionaryCommands {

        [Command("commands")]
        public async Task Commands () {
            var embed = new EmbedBuilder();

            embed.WithTitle("Key To Words Commands");
            embed.AddField("addkey <string> <string>", "Adds a new word to track and it's output when detected");
            embed.AddField("addkey <string> <@user> <string>", "Adds a new word to track and a user to tag and it's output when detected");
            embed.AddField("removekey <string>", "Stops the word from being tracked");
            embed.AddField("clear", "Removes all tracked words");
            embed.AddField("showall", "Shows all tracked words");
            embed.AddField("toggleauto", "Toggles the word tracker on and off");
            embed.WithColor(Color.Blue);

            await ReplyAsync("", false, embed.Build());
        }
        
        [Command("addkey")]
        public async Task AddOrUpdate (string key, [Remainder] string word = null) {
            string output = KeyWordChecker.AddOrUpdateKey_KeyToWords(key, word);
            await ReplyAsync(output);
        }

        [Command("addkey")]
        public async Task AddOrUpdate (string key, SocketUser user, [Remainder] string message = null) {
            string output = KeyWordChecker.AddOrUpdateKey_KeyToWords(key, user.Mention + " " + message);
            await ReplyAsync(output);
        }

        [Command("removekey")]
        public async Task Remove (string key) {
            var output = KeyWordChecker.RemoveKey_KeyToWords(key);
            await ReplyAsync(output);
        }

        [Command("clear")]
        public async Task Clear () {
            KeyWordChecker.Clear_KeyToWords();
            await ReplyAsync("Cleared");
        }

        [Command("showall")]
        public async Task ShowAll () {
            var output = KeyWordChecker.ShowAll_KeyToWords();
            await ReplyAsync(output);
        }
        
        [Command("toggleauto")]
        public async Task KeyToWords_ToggleAuto () {
            KeyWordChecker.KeyToWordCheckerEnabled = !KeyWordChecker.KeyToWordCheckerEnabled;
            await ReplyAsync("Toggled");
        }
    }
}
