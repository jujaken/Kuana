
using Discord.WebSocket;
using Kuana.Bot.Config;
using Microsoft.Extensions.Options;
using Zefirrat.YandexGpt.Api.Client;
using Zefirrat.YandexGpt.Base;
using Zefirrat.YandexGpt.Prompter;

namespace Kuana.Bot.Services
{
    public class GptService(DiscordSocketClient client, IHttpClientFactory httpClient, ICfgManager cfgManager) : IServiceHandler
    {
        private readonly DiscordSocketClient client = client;
        private readonly IHttpClientFactory httpClient = httpClient;
        private readonly ICfgManager cfgManager = cfgManager;

        private readonly string prompFile = "Config/PrompKuana.txt";
        private YaPrompter? promper;

        public void Install()
        {
            var cfg = cfgManager.GetData();

            var options = new YandexGptOptions()
            {
                Client = new YaClientOptions()
                {
                    AuthenticationScheme = "API-Key",
                    Token = cfg.YaGptToken,
                    CatalogId = cfg.YaGptFolder,
                },
            };

            var yaClient = new YaClient(httpClient, options.Client);
            promper = new YaPrompter(yaClient, Options.Create(options));

            client.MessageReceived += HandleMessageReceived;
        }

        private async Task HandleMessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot)
                return;
            try
            {
                var result = await promper!.SendAsync($"{GetPrompFromFile()}\n ГОВОРИТ {message.Author}: {message.Content}");
                await message.Channel.SendMessageAsync(result, messageReference: message.Reference);
            }
            catch (Exception ex)
            {
                await message.Channel.SendMessageAsync(ex.Message);
            }
        }

        private string GetPrompFromFile()
        {
            return File.ReadAllText(prompFile);
        }
    }
}
