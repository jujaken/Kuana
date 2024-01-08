
using Discord;
using Discord.WebSocket;
using Kuana.Bot.Config;
using Kuana.Bot.Services;

namespace Kuana.Bot
{
    public class KuanaBot(DiscordSocketClient client, ILogger logger, ICfgManager cfgManager) : IBot
    {
        private readonly DiscordSocketClient client = client;
        private readonly ILogger logger = logger;
        private readonly ICfgManager cfgManager = cfgManager;

        public async Task Run()
        {
            client.Log += (args) => logger.Log(args.Message, LogType.DiscordNet);

            await client.LoginAsync(TokenType.Bot, cfgManager.GetData().Token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        // ???
        public async Task Stop()
        {
            await client.StopAsync();
        }
    }
}
