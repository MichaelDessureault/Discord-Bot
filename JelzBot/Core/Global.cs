using Discord.WebSocket;
using System.Threading.Tasks;

namespace JelzBot {
    internal static class Global {
        public static DiscordSocketClient Client { get; set; }
        
        public static Task SendMessageAsync (string message, SocketTextChannel channel) {
            channel.SendMessageAsync(message);
            return Task.CompletedTask;
        }
    }
}
