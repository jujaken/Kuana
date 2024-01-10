namespace Kuana.Bot.Services
{
    public interface IGptService
    {
        Task<string> GetAnswer(string text);
    }
}
