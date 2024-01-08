using System.Text.Json.Serialization;

namespace Kuana.Bot.Config
{
    [JsonSerializable(typeof(BotConfig))]
    internal partial class BotConfigJsonSerializerContext : JsonSerializerContext
    {
    }
}
