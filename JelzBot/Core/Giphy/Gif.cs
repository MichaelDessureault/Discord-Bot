using GiphyDotNet.Model.Results;
using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using GiphyDotNet.Model.Parameters;

namespace JelzBot.Core.Giphy {
    internal static class Gif {
        private static GiphyDotNet.Manager.Giphy giphy;

        internal static Task Gif_Ready () {
            Console.WriteLine("Gif Starting");
            giphy = new GiphyDotNet.Manager.Giphy(Config.giphyBot.key);
            return Task.CompletedTask;
        }

        internal static async Task GetRandomGif (string tag, SocketTextChannel channel) {
            GiphyRandomResult random_gifResult = await giphy.RandomGif (new RandomParameter() {Tag = $"\"{tag}\""});
            await Global.SendMessageAsync(random_gifResult.Data.Url, channel);
        }

        internal static async Task GetRandomSticker (string tag, SocketTextChannel channel) {
            GiphyRandomResult random_gifResult = await giphy.RandomSticker(new RandomParameter() {Tag = $"\"{tag}\""});
            await Global.SendMessageAsync(random_gifResult.Data.Url, channel);
        }
    }
}
