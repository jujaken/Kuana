
using Zefirrat.YandexGpt.Prompter;

namespace Kuana.Bot.Services
{
    public class KuanaPromp : IPrompRule
    {
        private readonly YaPrompter prompter;
        private readonly List<string> memory = [];
        private readonly string prompFile = "Config/PrompKuana.txt";

        public KuanaPromp(YaPrompter prompter)
        {
            this.prompter = prompter;
            memory.Add(GetPrompFromFile());
        }

        public async Task<string> Send(string input)
        {
            var r = await prompter.SendAsync(UseMemoryRule(input) + " КУАНА:");

            if (r.Contains("куана: ", StringComparison.CurrentCultureIgnoreCase))
                r = new string(r.Skip(7).ToArray());

            UseMemoryRule("КУАНА: " + r);
            return r;
        }

        private string GetPrompFromFile()
        {
            return File.ReadAllText(prompFile);
        }

        private string UseMemoryRule(string msg)
        {
            if (memory.Count == 10)
                memory.Remove(memory.Skip(1).First());
            memory.Add(msg);

            var r = string.Empty;
            memory.ForEach(m => r += m + '\n');
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(r);
            return r;
        }
    }
}
