using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineBot
{
    public class Global
    {
        public static DiscordSocketClient Client;
        public static CommandService Commands;
    }
}