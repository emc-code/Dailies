using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

var discordSocketClient = new DiscordSocketClient();
await discordSocketClient.LoginAsync(TokenType.Bot, "MTEyMTAxNjM3Mjc3NjkyNzMwMw.GMFRnq.FoxJtOIL0DpZMO4gXuQaIlBcumkmBa72yxhgDE");
await discordSocketClient.StartAsync();

discordSocketClient.Log += LogMessage;

await Task.Delay(-1);

static Task LogMessage(LogMessage arg)
{
    Console.WriteLine(arg);
    return Task.CompletedTask;
}