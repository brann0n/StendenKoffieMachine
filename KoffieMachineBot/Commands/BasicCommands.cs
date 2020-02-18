using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace KoffieMachineBot.Commands
{
	public class BasicCommands : ModuleBase<SocketCommandContext>
	{
		[Command("Hello"), Summary("yoink")]
		public async Task Hello()
		{
			await ReplyAsync("Hello back!");
		}


		[Command("ping"), Summary("sends the ping back")]
		public async Task Ping()
		{
			await ReplyAsync($"Pong! {Global.Client.Latency} ms");
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

        [Command("help"), Summary("help command made by Ryan")]
        public async Task HelpAsync()
        {
            EmbedBuilder e = new EmbedBuilder();
            string commands = "";

            foreach(var c in Global.Commands.Commands)
            {
                commands += $"`{c.Name}` ";
            }

            e.AddField("Command listing", commands);
            e.WithTitle("Help");
            e.WithDescription("Hier, je commands, veel plezier.");

            await ReplyAsync(embed: e.Build());
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

		[Command("members"), Summary("Gives a nice update about the members"), Alias("m")]
		public async Task Members()
		{
			if (Context.Guild.Id == 486166479188918284) //kekistan
			{
				//get role objects
				SocketRole inf1role = Context.Guild.Roles.FirstOrDefault(i => i.Id == 616008345899368478);
				SocketRole inf2role = Context.Guild.Roles.FirstOrDefault(i => i.Id == 540216225339539458);
				SocketRole inf3role = Context.Guild.Roles.FirstOrDefault(i => i.Id == 625432605235478556);
				SocketRole inf4role = Context.Guild.Roles.FirstOrDefault(i => i.Id == 625432666371653662);
				SocketRole infFallenrole = Context.Guild.Roles.FirstOrDefault(i => i.Id == 510429544918810624);
				SocketRole infTeacherrole = Context.Guild.Roles.FirstOrDefault(i => i.Id == 623124878438170625);

				//other role objects
				SocketRole adminsRole = Context.Guild.Roles.FirstOrDefault(i => i.Id == 486167214702067722);
				SocketRole modsRole = Context.Guild.Roles.FirstOrDefault(i => i.Id == 486167477974466581);
				SocketRole weebsRole = Context.Guild.Roles.FirstOrDefault(i => i.Id == 623561010476744754);

				//get member count of role
				string inf1Users = Context.Guild.Users.Where(i => i.Roles.Contains(inf1role)).Count().ToString();
				string inf2Users = Context.Guild.Users.Where(i => i.Roles.Contains(inf2role)).Count().ToString();
				string inf3Users = Context.Guild.Users.Where(i => i.Roles.Contains(inf3role)).Count().ToString();
				string inf4Users = Context.Guild.Users.Where(i => i.Roles.Contains(inf4role)).Count().ToString();
				string infFallenUsers = Context.Guild.Users.Where(i => i.Roles.Contains(infFallenrole)).Count().ToString();
				int infTeacherUsers = Context.Guild.Users.Where(i => i.Roles.Contains(infTeacherrole)).Count();
				int adminsUsers = Context.Guild.Users.Where(i => i.Roles.Contains(adminsRole)).Count();
				int modsUsers = Context.Guild.Users.Where(i => i.Roles.Contains(modsRole)).Count();
				int weebsUsers = Context.Guild.Users.Where(i => i.Roles.Contains(weebsRole)).Count();

				//string teacher = infTeacherUsers == 1 ? "is 1 teacher." : $"zijn {infTeacherUsers} teachers.";
				//string replyMessage = $"INF1 heeft {inf1Users} members. \r\nINF2 heeft {inf2Users} members. \r\nINF3 heeft {inf3Users} members. \r\nINF4 heeft {inf4Users} members. \r\nEr zijn {infFallenUsers} Fallen Soldiers. \r\nEn er {teacher}";

				var builder = new EmbedBuilder();

				builder.WithTitle($"Member info in {Context.Guild.Name}");
				builder.AddField("INF1 members: ", inf1Users, true);
				builder.AddField("INF2 members: ", inf2Users, true);
				builder.AddField("INF3 members: ", inf3Users, true);
				builder.AddField("INF4 members: ", inf4Users, true);
				builder.AddField("Fallen members: ", infFallenUsers, true);
				builder.AddField("Teachers: ", infTeacherUsers, true);
				string mentionsString = $"Er zijn {adminsUsers} admins, {modsUsers} mods en {weebsUsers} weebs!";
				builder.AddField("Honorable mentions", mentionsString);

				//builder.AddInlineField("Mods: ", modsUsers);
				//builder.AddInlineField("Admins: ", adminsUsers);
				//builder.AddInlineField("Weebs: ", weebsUsers);

				//builder.WithThumbnailUrl("url");

				builder.WithColor(Color.Gold);
				await Context.Channel.SendMessageAsync("", false, builder.Build());
			}
			else
			{
				await ReplyAsync($"This server has {Context.Guild.MemberCount} members!");
			}
		}

		[Command("git"), Summary("Returns the git url of the bot")]
		public async Task Git()
		{
			var builder = new EmbedBuilder();
			builder.Title = "StendenKoffieMachine Git";
			builder.Url = "https://github.com/brann0n/StendenKoffieMachine";
			builder.AddField("Discord channel", "<#639491665740169246>");

			await Context.Channel.SendMessageAsync("", false, builder.Build());
		}
	}
}
