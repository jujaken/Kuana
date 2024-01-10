
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Kuana.Bot;
using Kuana.Bot.Config;
using Kuana.Bot.Services;
using Kuana.Bot.Services.Prompts;
using Kuana.Bot.Utils;
using Microsoft.Extensions.DependencyInjection;

var dialoger = new ConsoleDialoger();
dialoger.SayHello();

var socketCfg = new DiscordSocketConfig()
{
    GatewayIntents = GatewayIntents.All,
};

var serviceProvider = new ServiceCollection()
    // bot base
    .AddSingleton<IBot, KuanaBot>()
    .AddSingleton<DiscordSocketClient>()
    .AddSingleton(socketCfg)
    .AddSingleton<InteractionService>()
    .AddTransient<ICfgManager, CfgManager>()
    .AddTransient<ILogger, ConsoleLogger>()
    // gpt
    .AddHttpClient()
    .AddSingleton<KuanaGirlService>()
    .AddSingleton<IGptService, YaGptService>()
    .AddSingleton<IPromptSender<KuanaGirlPrompt>, KuanaGirlPromptSender>()
    .BuildServiceProvider();

var cfgManager = serviceProvider.GetService<ICfgManager>()!;
if (cfgManager.CreateFile())
{
    dialoger.SayWriteCfg();
    return;
}

var bot = serviceProvider.GetService<IBot>()!;
await bot.Run();

dialoger.SayGoodbye();
