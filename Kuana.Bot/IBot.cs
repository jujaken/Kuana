namespace Kuana.Bot
{
    public interface IBot
    {
        Task Run();
        Task Stop();
    }
}