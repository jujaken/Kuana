
using Discord.Interactions;
using org.mariuszgromada.math.mxparser;

namespace Kuana.Bot.Modules
{
    [Group("math", "sexymatica )0)")]
    public class MathModule : ModuleBase
    {
        [SlashCommand("solve-expression", "solve the expression")]
        public async Task SolveExpression(string expression)
        {
            var res = new Expression(expression).calculate();

            if (double.IsNaN(res))
            {
                await RespondAsync("boo, i dont know: `" + expression + "`");
                return;
            }

            await RespondAsync(expression + " = " + res);
        }
    }
}
