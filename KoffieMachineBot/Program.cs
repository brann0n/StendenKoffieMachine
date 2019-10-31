using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineBot
{
	class Program
	{
		DiscordSocketClient _client;

		static void Main(string[] args) => new Program().StartBotAsync().GetAwaiter().GetResult();


		public async Task StartBotAsync()
		{
			//check if the config was loaded successfully
			if (string.IsNullOrEmpty(Modules.BotConfig.bot.Token))
			{
				Console.WriteLine("No Authtoken present, please check the config file and restart the bot.");
				return;
			}

			//Set log Level
			_client = new DiscordSocketClient(new DiscordSocketConfig
			{
				LogLevel = LogSeverity.Verbose
			});

			//Assign client event triggers.
			_client.Log += Log;

			//Connect the bot to discord
			await _client.LoginAsync(TokenType.Bot, Modules.BotConfig.bot.Token);
			await _client.StartAsync();


			//stay in this function forever
			await Task.Delay(-1);
		}

		private async Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.Message);
		}
	}
}
