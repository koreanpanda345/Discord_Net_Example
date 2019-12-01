using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Discord_Net_Example.Core.Commands
{
    public class Miscellaneous : ModuleBase<SocketCommandContext>
    {
        [Command("latency"), Alias("ping"), Summary("Displays the bot's latency")]
        public async Task PingCommand()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("~Pong~");
            embed.WithDescription($"Latency is at: {Context.Client.Latency} ms");
            embed.WithFooter($"{Context.Client.CurrentUser.Username}", Context.Client.CurrentUser.GetAvatarUrl());

            await Context.Channel.SendMessageAsync("", embed: embed.Build());
        }
    }
}
