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
		[Command("Hello")]
		public async Task Hello()
		{
			await ReplyAsync("Hello back!");
		}

		[Command("is gone")]
		public async Task KoffieIsGone()
		{
			await ReplyAsync(":crab:");
		}
	}
}
