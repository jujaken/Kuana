
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

        private YaPrompter? promper;
        private KuanaPromp? kuana;


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
            kuana = new KuanaPromp(promper);

            client.MessageReceived += HandleMessageReceived;
        }

        private async Task HandleMessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot || !(message.MentionedUsers.Count(u => u.Id == client.CurrentUser.Id) == 1))
                return;
            try
            {
                var result = await kuana!.Send($"{message.Author}: {RemoveMentiones(message)}");
                await message.Channel.SendMessageAsync(result, messageReference: message.Reference);
            }
            catch (Exception ex)
            {
                await message.Channel.SendMessageAsync(ex.Message);
            }
        }

        private string RemoveMentiones(SocketMessage message)
        {
            return message.Content.Replace(client.CurrentUser.Mention, string.Empty);
        }
    }
}
