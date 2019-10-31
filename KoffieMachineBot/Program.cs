using Discord;
using Discord.WebSocket;
using System;
using KoffieMachineBot.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Reflection;

namespace KoffieMachineBot
{
	public class Program
	{
		DiscordSocketClient _client;
		CommandService _commands;
		IServiceProvider _services;

		CommandHandler _handler;

		static void Main(string[] args) => new Program().StartBotAsync().GetAwaiter().GetResult();

		public async Task StartBotAsync()
		{
			//check if the config was loaded successfully
			if (string.IsNullOrEmpty(BotConfig.bot.Token))
			{
				Console.WriteLine("No Authtoken present, please check the config file and restart the bot.");
				Console.ReadLine();
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
			await _client.LoginAsync(TokenType.Bot, BotConfig.bot.Token);
			await _client.StartAsync();

			//assign the current client object to the global client object
			Global.Client = _client;

			//set status to prefix
			await _client.SetGameAsync($"prefix {BotConfig.bot.CmdPrefix}", type: ActivityType.Listening);

			_commands = new CommandService();

			_services = new ServiceCollection()
				.AddSingleton<DiscordSocketClient>()
				.AddSingleton<CommandService>()
				.AddSingleton<HttpClient>()
				.BuildServiceProvider();

			//discover all of the commands in this assembly and load them.
			await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);

			//assign the command handler
			_handler = new CommandHandler(_client, _services, _commands);

			Global.Commands = _commands;

			//stay in this function forever
			await Task.Delay(-1);
		}

		private async Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.Message);
		}
	}
}
