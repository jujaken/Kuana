using Kuana.Bot.Exceptions;
using System.Text.Json;

namespace Kuana.Bot.Config
{
    internal class CfgManager : ICfgManager
    {
        private readonly string cfgDir = "Config";
        private readonly string cfgFile = "Config/BotConfig.json";

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if file not exists</returns>
        public bool CreateFile()
        {
            if (File.Exists(cfgFile))
                return true;

            var json = JsonSerializer.Serialize(new BotConfig(), BotConfigJsonSerializerContext.Default.BotConfig);
            Directory.CreateDirectory(cfgDir);
            File.WriteAllText(cfgFile, json);

            return false;
        }

        public BotConfig GetData()
        {
            return JsonSerializer.Deserialize(File.ReadAllText(cfgFile), BotConfigJsonSerializerContext.Default.BotConfig)
                ?? throw new BotConfigException();
        }
    }
}
