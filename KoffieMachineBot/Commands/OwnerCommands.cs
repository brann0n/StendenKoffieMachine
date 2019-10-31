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
		[Command("Shutdown"), Summary("turns the bot off, owner only")]
		public async Task ShutDown()
		{
			await ReplyAsync("Shutting down the bot");
			System.Environment.Exit(1);
		}

		[Command("status"), Summary("sets the status, owner only")]
		public async Task Status([Remainder]string status = "")
		{
			await Global.Client.SetGameAsync(status);
			await Global.Client.SetStatusAsync(Discord.UserStatus.Online);			
		}
	}
}
