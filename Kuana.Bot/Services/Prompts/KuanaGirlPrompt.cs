using Kuana.Bot.Models;
using Zefirrat.YandexGpt.Prompter;

namespace Kuana.Bot.Services.Prompts
{
    public class KuanaGirlPrompt : TxtFilePrompt
    {
        public override string FilePath => "Config/PrompKuana.txt";
    }
}
