using Newtonsoft.Json;
using System;
using System.IO;

namespace KoffieMachineBot.Modules
{
	class BotConfig
	{
		private const string ConfigFolder = "Resources"; //Folder in which you store the config files. (relative to project, but can be anywhere on the disk)
		private const string ConfigFile = "config.json";

		public static BotConfigProperties bot;

		static BotConfig()
		{
			//check if the directory is present, otherwise create it.
			if (!Directory.Exists(ConfigFolder))
				Directory.CreateDirectory(ConfigFolder);

			//check if the config file is present, otherwise create it.
			if (!File.Exists(ConfigFolder + "/" + ConfigFile))
			{
				bot = new BotConfigProperties();
				string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
				File.WriteAllText(ConfigFolder + "/" + ConfigFile, json);
				Console.WriteLine("Config file was not present, creating file now.");
			}
			else
			{
				string json = File.ReadAllText(ConfigFolder + "/" + ConfigFile);
				bot = JsonConvert.DeserializeObject<BotConfigProperties>(json);
			}
		}
	}

	public struct BotConfigProperties
	{
		public string Token;
		public string CmdPrefix;
	}
}
