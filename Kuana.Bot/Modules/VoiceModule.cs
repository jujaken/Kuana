using Discord.Audio;
using Discord.Interactions;
using System.Diagnostics;

namespace Kuana.Bot.Modules
{
    [Group("voice", "kuana in voice~~")]
    public class VoiceModule : ModuleBase
    {
        [SlashCommand("connect", "connect kuana to voice channel", runMode: RunMode.Async)]
        public async Task GoVoice()
        {
            var channel = Context.Guild.VoiceChannels.FirstOrDefault(v => v.ConnectedUsers.Contains(Context.User));
            if (channel == null)
            {
                await RespondAsync("sorry, u r not a voice >_<");
                return;
            }

            await RespondAsync("k, im already with you ❤️");
            await SendAsync(await channel.ConnectAsync(), "music.opus");
        }

        private async Task SendAsync(IAudioClient client, string path)
        {
            // Create FFmpeg using the previous example
            using var ffmpeg = CreateStream(path);
            using var output = ffmpeg.StandardOutput.BaseStream;
            using var discord = client.CreatePCMStream(AudioApplication.Mixed);
            try { await output.CopyToAsync(discord); }
            finally { await discord.FlushAsync(); }
        }

        private Process CreateStream(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            })!;
        }
    }
}
