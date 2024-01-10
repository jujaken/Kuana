namespace Kuana.Bot.Services.Prompts
{
    public abstract class TxtFilePrompt : IPrompt
    {
        public abstract string FilePath { get; }
        public string PromptText => File.ReadAllText(FilePath);

        public string PromptWithText(string text)
            => $"{PromptText}\n{text}";
    }
}
