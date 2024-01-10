namespace Kuana.Bot.Services.Prompts
{
    public interface IPrompt
    {
        string PromptText { get; }
        string PromptWithText(string text);
    }
}
