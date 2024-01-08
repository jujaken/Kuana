
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Kuana.Bot.Config;
using Kuana.Bot.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kuana.Bot
{
    public class KuanaBot(IServiceProvider serviceProvider,
        DiscordSocketClient client, InteractionService interactionService,
        ILogger logger, ICfgManager cfgManager) : IBot
    {
        private readonly IServiceProvider serviceProvider = serviceProvider;
        private readonly DiscordSocketClient client = client;
        private readonly InteractionService interactionService = interactionService;
        private readonly ILogger logger = logger;
        private readonly ICfgManager cfgManager = cfgManager;

        private readonly List<SocketGuild> avaibleGuilds = new();

        public async Task Run()
        {
            InstallBaseHandles();

            await client.LoginAsync(TokenType.Bot, cfgManager.GetData().Token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private void InstallBaseHandles()
        {
            client.Log += HandleLog;
            client.Ready += HandleReady;
            client.GuildAvailable += HandleGuildAvailable;
        }

        private async Task HandleLog(LogMessage args)
        {
            await logger.Log(args.Message, LogType.DiscordNet);
        }

        private async Task HandleReady()
        {
            await ConnectModules();
            await ConnectServices();
        }

        private Task HandleGuildAvailable(SocketGuild guild)
        {
            avaibleGuilds.Add(guild);
            return Task.CompletedTask;
        }

        private async Task ConnectModules()
        {
            await interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);

            avaibleGuilds.ForEach(async g =>
            {
                await interactionService.RegisterCommandsToGuildAsync(g.Id);
            });

            client.InteractionCreated += async interaction =>
            {
                var scope = serviceProvider.CreateScope();
                var ctx = new SocketInteractionContext(client, interaction);
                await interactionService.ExecuteCommandAsync(ctx, scope.ServiceProvider);
            };
        }

        private Task ConnectServices()
        {
            Assembly.GetEntryAssembly()?.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IServiceHandler))).ToList().ForEach(s =>
            {
                var service = serviceProvider.GetService(s);
                if (service != null)
                    (service as IServiceHandler)!.Install();
            });

            return Task.CompletedTask;
        }

        // ???
        public async Task Stop()
        {
            await client.StopAsync();
        }
    }
}
