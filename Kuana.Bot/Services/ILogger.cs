
namespace Kuana.Bot.Services
{
    public interface ILogger
    {
        Task Log(string message);
        Task Log(string message, string prefix);
        Task Log(string message, LogType logType);
        Task Log(string message, string prefix, LogType logType);
    }

    public enum LogType
    {
        None,
        Warning,
        Error,
        Debug,
        DiscordNet,
    }
}
