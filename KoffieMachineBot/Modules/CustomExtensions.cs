using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineBot.Modules
{
	public static class CustomExtensions
	{
		public static bool HasStringPrefixCI(this IUserMessage msg, string str, ref int argPos, StringComparison comparisonType = StringComparison.Ordinal)
		{
			string message = msg.Content.ToLower();
			bool isPrefix = message.StartsWith(str);
			return isPrefix;
		}
	}
}
