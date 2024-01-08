using Discord;
using Discord.Interactions;

namespace Kuana.Bot.Modules
{
    public class ModuleBase : InteractionModuleBase<SocketInteractionContext>
    {
        protected Dictionary<TypeRespond, RespondMeta> RespondMetas = new()
        {
            [TypeRespond.None] = new RespondMeta() { Color = new Color(18, 85, 171) },
            [TypeRespond.Info] = new RespondMeta() { Color = new Color(27, 208, 206) },
            [TypeRespond.Warning] = new RespondMeta() { Color = new Color(194, 178, 25) },
            [TypeRespond.Error] = new RespondMeta() { Color = new Color(179, 42, 15) },
            [TypeRespond.Love] = new RespondMeta() { Color = new Color(186, 23, 179) },
        };

        protected async Task RespondEmbed(string msg, RespondMeta meta)
        {
            var embed = new EmbedBuilder().WithDescription(msg);

            if (meta.Color is not null)
                embed.WithColor((Color)meta.Color);

            if (meta.ImageUrl is not null)
                embed.WithAuthor(new EmbedAuthorBuilder().WithUrl(meta.ImageUrl));

            await RespondAsync(embeds: new Embed[1] { embed.Build() });
        }

        protected Task RespondEmbed(string msg, TypeRespond type = TypeRespond.None)
            => RespondEmbed(msg, RespondMetas[type]);

        protected Task RespondError(string msg)
            => RespondEmbed(msg, TypeRespond.Error);

        protected Task RespondError(Exception exception)
             => RespondAsync(exception.Message);
    }

    public class RespondMeta()
    {
        public Color? Color { get; set; }
        public string? ImageUrl { get; set; }
    }

    public enum TypeRespond
    {
        None,
        Info,
        Warning,
        Error,
        Love,
    }
}
