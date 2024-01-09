namespace Kuana.Bot.Services
{
    public interface IPrompRule
    {
        Task<string> Send(string input);
    }
}
