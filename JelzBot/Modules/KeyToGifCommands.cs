using Discord;
using Discord.Commands;
using JelzBot.Core;
using JelzBot.Interface;
using System.Threading.Tasks;

namespace JelzBot.Modules {

    [Group("keytogif")]
    public class KeyToGifCommands : ModuleBase<SocketCommandContext>, IDictionaryCommands {

        [Command("commands")]
        public async Task Commands () {
            var embed = new EmbedBuilder();

            embed.WithTitle("Key To Gif Commands");
            embed.AddField("addkey <string>", "Adds a new word to track for gifs");
            embed.AddField("removekey <string>", "Stops the word from being tracked");
            embed.AddField("clear", "Removes all tracked words");
            embed.AddField("showall", "Shows all tracked words");
            embed.AddField("toggleauto", "Toggles the word tracker on and off");
            embed.WithColor(Color.Blue);

            await ReplyAsync("", false, embed.Build());
        }
        
        [Command("addkey")]
        public async Task AddOrUpdate (string key, [Remainder] string word = null) {
            var output = KeyWordChecker.AddOrUpdateKey_KeyToGif(key);
            await ReplyAsync(output);
        }

        [Command("removekey")]
        public async Task Remove (string key) {
            var output = KeyWordChecker.RemoveKey_KeyToGif(key);
            await ReplyAsync(output);
        }

        [Command("clear")]
        public async Task Clear () {
            KeyWordChecker.Clear_KeyToGif();
            await ReplyAsync("Cleared");
        }

        [Command("showall")]
        public async Task ShowAll () {
            var output = KeyWordChecker.ShowAll_KeyToGif();
            await ReplyAsync(output);
        }


        [Command("toggleauto")]
        public async Task KeyToGif_ToggleAuto () {
            KeyWordChecker.KeyToGifCheckerEnabled = !KeyWordChecker.KeyToGifCheckerEnabled;
            await ReplyAsync("Toggled");
        }
    }
}
