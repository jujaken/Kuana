
using Kuana.Bot.Models;

namespace Kuana.Bot.Services.Prompts
{
    public class KuanaGirlPromptSender(KuanaGirlModel girlModel) : IPromptSender<KuanaGirlPrompt>
    {
        private readonly KuanaGirlPrompt prompt = new();
        public KuanaGirlPrompt Prompt => prompt;

        private readonly KuanaGirlModel girlModel = girlModel;

        public KuanaGirlPromptSender() : this(new KuanaGirlModel() { MemmoryMessage = [], MemmorySize = 10 })
        {
        }

        public async Task<string> Send(IGptService gpt, string message)
        {
            var answer = await gpt.GetAnswer(Prompt.PromptWithText($"{GetMemmory()}{message}"));

            answer = answer.ToLower();

            if (answer.Contains("куана: "))
                answer = answer[7..];

            girlModel.AddToMemmoryMessage(GetAuthor(message), message);
            girlModel.AddToMemmoryMessage("kuana", answer);

            return answer;
        }

        private string GetMemmory()
        {
            var str = string.Empty;

            girlModel.MemmoryMessage.ForEach(m =>
            {
                str += $"{m.Author}: {m.Message}\n";
            });

            return str;
        }

        private string GetAuthor(string msg)
            => msg[..msg.IndexOf(':')];
    }
}
