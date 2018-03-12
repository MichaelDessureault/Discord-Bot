using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using JelzBot.Core;
using JelzBot.Core.Giphy;
using JelzBot.Core.Twitter;

namespace JelzBot {
    class Program {
        static void Main (string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
        
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        private Config.DiscordConfig discordBotConfig; 

        public async Task RunBotAsync () {
            discordBotConfig = Config.discordBot;

            // setup
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();
            
            // event subscriptions
            _client.Log += Log;
            _client.Ready += KeyWordChecker.KeyWordChecker_Ready;
            _client.Ready += Gif.Gif_Ready;
            _client.Ready += Twitter.Twitter_Ready;

            await RegisterCommandAsync();
            await _client.LoginAsync(TokenType.Bot, discordBotConfig.token);
            await _client.StartAsync();
            
            Global.Client = _client;

            await Task.Delay(-1);
        }
        
        private Task Log (LogMessage arg) {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync () {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync (SocketMessage arg) {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot)
                return;
            
            int argPos = 0;

            var hasPrefix = message.HasStringPrefix(discordBotConfig.cmdPrefix, ref argPos);
            
            var context = new SocketCommandContext(_client, message);

            if (!hasPrefix)
                await KeyWordChecker.CheckMessageForKeyWord(message.Content, context);
            
            if (hasPrefix || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) {
                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
}
