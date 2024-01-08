namespace Kuana.Bot.Config
{
    internal interface ICfgManager
    {
        bool CreateFile();
        BotConfig GetData();
    }
}