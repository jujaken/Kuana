namespace Kuana.Bot.Config
{
    public class BotConfig
    {
        public string Token { get; set; } = string.Empty;
        public ulong DeveloperId { get; set; }
        public string YaGptToken { get; set; } = string.Empty;
        public string YaGptFolder { get; set; } = string.Empty;
    }
}
