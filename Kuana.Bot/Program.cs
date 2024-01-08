
using Kuana.Bot;
using Kuana.Bot.Config;
using Kuana.Bot.Services;
using Kuana.Bot.Utils;
using Microsoft.Extensions.DependencyInjection;

var dialoger = new ConsoleDialoger();
dialoger.SayHello();

var serviceProvider = new ServiceCollection()
    .AddSingleton<IBot, KuanaBot>()
    .AddTransient<ICfgManager, CfgManager>()
    .AddTransient<ILogger, ConsoleLogger>()
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
