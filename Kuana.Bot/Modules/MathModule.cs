
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
            var exp = new Expression(expression);
            await RespondAsync(expression + " " + exp.calculate());
        }
    }
}
