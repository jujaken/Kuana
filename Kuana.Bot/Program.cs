
using Kuana.Bot;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IBot, KuanaBot>()
    .BuildServiceProvider();

var bot = serviceProvider.GetService<IBot>()!;
await bot.Run();
