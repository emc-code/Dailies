using Discord;
using Discord.WebSocket;

const string StopSignal = "sss";    // stop
const string CheckSignal = "ccc";   // ccc
const int StartHour = 18;
const int StartMinute = 00;

bool checkUsers = true;

Dictionary<string, string> allUsers = new Dictionary<string, string> { { "ds_emc", "Админ Админович"},
                                                                       { "test", "Тест Тестович" } };

DiscordSocketConfig discordSocketConfig = new DiscordSocketConfig();
discordSocketConfig.GatewayIntents = GatewayIntents.MessageContent;

var discordSocketClient = new DiscordSocketClient();

discordSocketClient.MessageReceived += DiscordSocketClient_MessageReceived;



await discordSocketClient.LoginAsync(TokenType.Bot, "MTEyMTAxNjM3Mjc3NjkyNzMwMw.GycfOT.0i4fhb9rQa4eVnkS41S5-eZKHrc6JR-H2O-DvQ");
await discordSocketClient.StartAsync();

Console.Read();

Task DiscordSocketClient_MessageReceived(SocketMessage arg)
{

    if (arg.Channel.Name == "dailies")
    {
        if (arg.Author.IsBot == false)
        {
            if (arg.Content.ToUpper() == StopSignal.ToUpper())
                checkUsers = false;

            if (arg.Content.ToUpper() == CheckSignal.ToUpper())
                Check(arg);
        }
    }

    return Task.CompletedTask;
}

string GetLastMessage(SocketMessage arg)
{
    if (arg.Channel is SocketTextChannel textChannel)
    {
        var lastMessage = textChannel.GetMessagesAsync(10).FlattenAsync().Result.ToList().Where(x => x.Author.IsBot == false).Select(x => x.Content).ToList();

        return "123";
    }

    return string.Empty;
}

void Check(SocketMessage arg)
{
    var presentUsers = AsyncEnumerableExtensions.FlattenAsync<IUser>(arg.Channel.GetUsersAsync()).Result.Select(x => x.Username.ToUpper()).ToList();

    List<string> missingUsers = allUsers.Where(x => !presentUsers.Contains(x.Key.ToUpper())).Select(kv => kv.Value).ToList();

    arg.Channel.SendMessageAsync("Отсутствуют:");
    if (missingUsers.Count > 0)
        missingUsers.ForEach(x => arg.Channel.SendMessageAsync(x));
}