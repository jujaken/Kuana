namespace Kuana.Bot.Services.Prompts
{
    public interface IPromptSender<T> where T : IPrompt
    {
        T Prompt { get; }
        Task<string> Send(IGptService gpt, string message);
    }
}