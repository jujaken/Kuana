
using Kuana.Bot;
using Kuana.Bot.Config;
using Kuana.Bot.Utils;
using Microsoft.Extensions.DependencyInjection;

var dialoger = new ConsoleDialoger();

dialoger.SayHello();

// cfg
ICfgManager cfgManager = new CfgManager();
if (cfgManager.CreateFile())
{
    dialoger.SayWriteCfg();
    return;
}

var serviceProvider = new ServiceCollection()
    .AddSingleton<IBot, KuanaBot>()
    .AddSingleton(cfgManager.GetData())
    .BuildServiceProvider();

var bot = serviceProvider.GetService<IBot>()!;
await bot.Run();

dialoger.SayGoodbye();
