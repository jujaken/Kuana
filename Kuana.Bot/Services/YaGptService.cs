
using Discord.WebSocket;
using Kuana.Bot.Config;
using Kuana.Bot.Services.Prompts;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Zefirrat.YandexGpt.Api.Client;
using Zefirrat.YandexGpt.Base;
using Zefirrat.YandexGpt.Prompter;

namespace Kuana.Bot.Services
{
    public class YaGptService : IGptService
    {
        private readonly YaPrompter promper;

        public YaGptService(DiscordSocketClient client, IHttpClientFactory httpClient, ICfgManager cfgManager)
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
        }

        public Task<string> GetAnswer(string text)
            => promper.SendAsync(text);
    }
}
