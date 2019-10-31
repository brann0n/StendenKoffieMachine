using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace KoffieMachineBot.Commands
{
	public class BasicCommands : ModuleBase<SocketCommandContext>
	{
		[Command("Hello"), Summary("yoink")]
		public async Task Hello()
		{
			await ReplyAsync("Hello back!");
		}

		[Command("coin"), Alias("coin flip"), Summary("krijg een gok verslaving")]
		public async Task CoinFlip()
		{
			var rng = new Random();
			if (rng.NextDouble() < 0.5)
			{
				await ReplyAsync("Heads!");
			}
			else
			{
				await ReplyAsync("Tails!");
			}
		}

		[Command("koffie"), Summary("Dikke koffie tijd")]
		public async Task TimeForKoffie()
		{
			string emoinName = Context.Guild.Emotes.First(e => e.Name == "stendenkoffie").Name.ToString();
			string emoinid = Context.Guild.Emotes.First(e => e.Name == "stendenkoffie").Id.ToString();
			await ReplyAsync($"It is always time for koffie! <:{emoinName}:{emoinid}>");
		}
    
		[Command("is gone"), Summary("returns the crab emote")]
		public async Task KoffieIsGone()
		{
			await ReplyAsync(":crab:");
		}

		[Command("list commands"), Summary("This command gives a list of commands")]
		public async Task ListCommands()
		{
			var embed = new EmbedBuilder();
			embed.WithTitle("List of commands");
			foreach(var command in Global.Commands.Commands)
			{
				embed.AddField(command.Name, command.Summary);
			}

			await Context.Channel.SendMessageAsync("", false, embed.Build());
		}
	}
}
