using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using System.Threading.Tasks;
using Discord.Commands;
using System.Reflection;
using System.Linq;
using Discord.WebSocket;

namespace Discord_Net_Example
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient Client;
        private readonly CommandService Commands;
        public CommandHandler(DiscordSocketClient Client, CommandService Commands)
        {
            this.Client = Client;
            this.Commands = Commands;
        }

        public async Task InitializeAsync()
        {
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            Commands.Log += Commands_Log;
            Client.MessageReceived += Client_MessageReceived;
        }

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgsPos = 0;
            if (!(Message.HasStringPrefix("+", ref ArgsPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgsPos))) return;

            var Result = await Commands.ExecuteAsync(Context, ArgsPos, null);
            if (!Result.IsSuccess && Result.Error != CommandError.UnknownCommand)
            {
                Console.WriteLine($"{DateTime.Now} at Command: {Commands.Search(Context, ArgsPos).Commands[0].Command.Name} in {Commands.Search(Context, ArgsPos).Commands[0].Command.Module.Name}] {Result.ErrorReason}");
                var embed = new EmbedBuilder();

                if (Result.ErrorReason == "The input text has too few parameters.")
                {
                    embed.WithTitle("***ERROR***");
                    embed.WithDescription("This command requires something check help command to see what it needs,");
                }
                else
                {
                    embed.WithTitle("***ERROR***");
                    embed.WithDescription(Result.ErrorReason);
                }
                await Context.Channel.SendMessageAsync("", embed: embed.Build());
            }
        }

        private Task Commands_Log(LogMessage Message)
        {

            Console.WriteLine(Message.Message);
            return Task.CompletedTask;
            
        }
    }
}
