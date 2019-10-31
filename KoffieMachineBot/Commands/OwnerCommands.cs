using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineBot.Commands
{
	[RequireOwner]
	public class OwnerCommands : ModuleBase<SocketCommandContext>
	{
		[Command("Shutdown")]
		public async Task ShutDown()
		{
			await ReplyAsync("Shutting down the bot");
			System.Environment.Exit(1);
		}
	}
}
