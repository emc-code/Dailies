using Discord;
using Discord.WebSocket;

const string OnOfftSignal = "!1"; // stop\start - при получении сигнала чекнет и начнет чекать при каждом событие на сервере
bool checkUsers = true;

Dictionary<string, string> allUsers = new Dictionary<string, string> { { "ds_emc", "Админ Админович"},
                                                                       { "test", "Тест Тестович" } };

DiscordSocketConfig discordSocketConfig = new DiscordSocketConfig();
discordSocketConfig.GatewayIntents = GatewayIntents.All;

var discordSocketClient = new DiscordSocketClient();

discordSocketClient.MessageReceived += DiscordSocketClient_MessageReceived;

await discordSocketClient.LoginAsync(TokenType.Bot, "MTEyMTAxNjM3Mjc3NjkyNzMwMw.GycfOT.0i4fhb9rQa4eVnkS41S5-eZKHrc6JR-H2O-DvQ");
await discordSocketClient.StartAsync();

Console.Read();

Task DiscordSocketClient_MessageReceived(SocketMessage arg)
{
    string qwe = arg.Content.Trim();
    arg.Channel.SendMessageAsync("1234123 41234" + arg.Content);

    return Task.CompletedTask;

    if (arg.Channel.Name == "dailies")
    {
        if (arg.Author.IsBot == false)
        {
            /* if (arg.Content.ToUpper() == OnOfftSignal.ToUpper())
                 checkUsers = !checkUsers;

             arg.Channel.SendMessageAsync(arg.Content);

             if (checkUsers)
            */
            Check(arg);
        }
    }

    return Task.CompletedTask;
}

void Check(SocketMessage arg)
{
    var presentUsers = AsyncEnumerableExtensions.FlattenAsync<IUser>(arg.Channel.GetUsersAsync()).Result.Select(x => x.Username.ToUpper()).ToList();

    List<string> missingUsers = allUsers.Where(x => !presentUsers.Contains(x.Key.ToUpper())).Select(kv => kv.Value).ToList();

    arg.Channel.SendMessageAsync("Отсутствуют:");
    if (missingUsers.Count > 0)
        missingUsers.ForEach(x => arg.Channel.SendMessageAsync(x));
}