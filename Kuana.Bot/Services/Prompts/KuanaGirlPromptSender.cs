
using Kuana.Bot.Models;

namespace Kuana.Bot.Services.Prompts
{
    public class KuanaGirlPromptSender(KuanaGirlModel girlModel) : IPromptSender<KuanaGirlPrompt>
    {
        private readonly KuanaGirlPrompt prompt = new();
        public KuanaGirlPrompt Prompt => prompt;
        
        private KuanaGirlModel girlModel = girlModel;

        public KuanaGirlPromptSender() : this(new KuanaGirlModel() { MemmoryMessage = [], MemmorySize = 10 })
        {
        }

        public async Task<string> Send(IGptService gpt, string message)
        {
            var answer = await gpt.GetAnswer(Prompt.PromptWithText(message));

            girlModel.AddToMemmoryMessage(GetAuthor(message), message);
            girlModel.AddToMemmoryMessage("kuana", answer);

            return answer;
        }

        private string GetAuthor(string msg)
            => msg[..msg.IndexOf(':')];
    }
}
