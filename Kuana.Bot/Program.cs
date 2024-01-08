
using Kuana.Bot;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IBot, KuanaBot>()
    .BuildServiceProvider();
