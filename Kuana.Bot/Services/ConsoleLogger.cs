

namespace Kuana.Bot.Services
{
    public class ConsoleLogger : ILogger
    {
        public Task Log(string message)
             => ConsoleLog(message);

        public Task Log(string message, string prefix)
            => ConsoleLog(message, prefix);


        public Task Log(string message, LogType logType)
            => ConsoleLog(message, logType:logType);

        public Task Log(string message, string prefix, LogType logType)

            => ConsoleLog(message, prefix, logType);


        private Task ConsoleLog(string message, string? prefix = null, LogType? logType = null)
        {
            var prx1 = prefix is null ? string.Empty : $"<{prefix}> ";
            var prx2 = logType is null || logType == LogType.None ? string.Empty : $"({logType}) ";

            Console.WriteLine($"p{DateTime.Now:hh:mm:ss}] {prx1}{prx2}- {message}");

            return Task.CompletedTask;
        }
    }
}
