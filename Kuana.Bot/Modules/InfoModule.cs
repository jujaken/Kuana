
using Discord.Interactions;

namespace Kuana.Bot.Modules
{
    [Group("info", "kuana bot info")]
    public class InfoModule : ModuleBase
    {
        [SlashCommand("kuana-bot", "i say about me")]
        public async Task SayAboutKuana()
        {
            await RespondAsync("im kuana~~");
        }
    }
}
