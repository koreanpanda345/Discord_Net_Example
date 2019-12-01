using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using System.Linq;
namespace Discord_Net_Example
{
    public class Bot
    {
        private DiscordSocketClient Client;
        private CommandService Commands;
        private IServiceProvider Service;

        public Bot()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });
            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });
        }

        public async Task MainAsync()
        {
            var cmdHandler = new CommandHandler(Client, Commands);
            await cmdHandler.InitializeAsync();

            Client.Ready += Client_Ready;
            Client.Log += Client_Log;
            if (Config.bot.token == "" || Config.bot.token == null) return;

            await Client.LoginAsync(TokenType.Bot, Config.bot.token);
            await Client.StartAsync();
            await Task.Delay(-1);
        }

        private Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source} {Message.Message}");
            return Task.CompletedTask;
        }

        private async Task Client_Ready()
        {
            await Client.SetGameAsync($"{Client.CurrentUser.Username} is ready");
            await Client.SetStatusAsync(UserStatus.Online);
        }
    }
}
