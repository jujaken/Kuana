
using Discord.WebSocket;
using Kuana.Bot.Services.Prompts;

namespace Kuana.Bot.Services
{
    public class KuanaGirlService(DiscordSocketClient client, 
        IGptService gptService, IPromptSender<KuanaGirlPrompt> sender) : IServiceHandler
    {
        private readonly DiscordSocketClient client = client;
        private readonly IGptService gptService = gptService;
        private readonly IPromptSender<KuanaGirlPrompt> sender = sender;

        public void Install()
        {
            client.MessageReceived += HandleMessageReceived;
        }

        private async Task HandleMessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot || !(message.MentionedUsers.Count(u => u.Id == client.CurrentUser.Id) == 1))
                return;
            try
            {
                var result = await sender.Send(gptService, $"{message.Author}: {RemoveMentiones(message)}");
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
