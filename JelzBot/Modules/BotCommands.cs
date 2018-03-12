using Discord;
using Discord.Commands;
using Discord.WebSocket;
using JelzBot.Core;
using JelzBot.Core.Giphy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JelzBot.Modules {
    public class BotCommands : ModuleBase<SocketCommandContext> {
        
        private Dictionary<string, string> commandsDictionary = new Dictionary<string, string> () {
            { "keytowords addkey", "Adds a new key word and it's description.  Example: JBkeytowords addkey example this is an example" },
            { "keytowords removekey", "Removes a key word" },
            { "keytowords clear", "Removes all key words" },
            { "keytowords showall", "Shows all key words" },
            { "keytowords toggleauto", "Stops only the key to words from responding" },
            { "keytogif addkey", "Adds a new key word for a gif" },
            { "keytogif removekey", "Removes key word" },
            { "keytogif clear", "Removes all key words" },
            { "keytogif showall", "Shows all key words" },
            { "keytogif toggleauto", "Stops only the key to gif from responding" },
        };

        [Command("help")]
        public async Task HelpAysnc () {
            await CommandsAsync();
        }

        [Command("commands")]
        public async Task CommandsAsync () {
            var embed = new EmbedBuilder();
            embed.WithTitle("Commands");
            embed.AddField("help", "displays this message");
            embed.AddField("commands", "displays this message");
            embed.AddField("toggleauto", "Stops the bot from responding to key words");
            embed.AddField("togglemulti", "Prevents the bot from putting multiple responses per user input");
            embed.AddField("stickerof [string]", "Finds a sticker of whatever string is inserted after the command");
            embed.AddField("gifof [string]", "Finds a gif of whatever string is inserted after the command");
            embed.AddField("admin commands", "shows all admin commands");
            embed.AddField("twitter commands", "Shows all the twitter commands");
            embed.AddField("keytowords commands", "Shows all the key to words commands");
            embed.AddField("keytogif commands", "shows all the key to gif commands");
            embed.Color = Color.Blue;

            await ReplyAsync("", false, embed.Build());
        }
        
        [Command("toggleauto")]
        public async Task ToggleAutoResponse () {
            KeyWordChecker.KeyCheckerEnabled = !KeyWordChecker.KeyCheckerEnabled;
            await ReplyAsync("Toggled");
        }

        [Command("togglemulti")]
        public async Task ToggleMultipleWordsPerInput () {
            KeyWordChecker.MultipleWordsPerInput = !KeyWordChecker.MultipleWordsPerInput;
            await ReplyAsync("Toggled");
        }

        [Command("stickerof")]
        public async Task StickerOf ([Remainder] string item) {
            await Gif.GetRandomSticker(item, Context.Channel as SocketTextChannel);
        }

        [Command("gifof")]
        public async Task GifOf ([Remainder] string item) {
            await Gif.GetRandomGif(item, Context.Channel as SocketTextChannel);
        }
    }
}
