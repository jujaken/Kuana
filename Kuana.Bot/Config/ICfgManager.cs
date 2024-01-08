namespace Kuana.Bot.Config
{
    public interface ICfgManager
    {
        bool CreateFile();
        BotConfig GetData();
    }
}