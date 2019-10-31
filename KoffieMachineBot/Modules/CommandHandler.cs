using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineBot.Modules
{
	public class CommandHandler
	{
		DiscordSocketClient _client;
		CommandService _commands;
		IServiceProvider _services;

		public CommandHandler(DiscordSocketClient client, IServiceProvider services, CommandService commands)
		{
			_client = client;
			_commands = commands;
			_services = services;

			//assign _client events
			_client.MessageReceived += HandleCommandAsync;
		}

		private async Task HandleCommandAsync(SocketMessage s)
		{
			//check if the event gave a SocketMessage, otherwise exit the method
			if (!(s is SocketUserMessage msg)) return;

			SocketCommandContext context = new SocketCommandContext(_client, msg);

			//check if the message came from a bot, otherwise exit the method
			if (context.User.IsBot) return;

			//check if the message had the bot's prefix
			int argPos = BotConfig.bot.CmdPrefix.Length;
			if (msg.HasStringPrefixCI(BotConfig.bot.CmdPrefix.ToLower(), ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
			{
				//tell the commandservice to check the assembly for this command and then execute it.
				var result = await _commands.ExecuteAsync(context, argPos, _services);

				//handle any errors
				if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
				{					
					Console.WriteLine(result.ErrorReason);
				}
			}			
		}
	}
}
