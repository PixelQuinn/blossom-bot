using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

public class EightBallCommands : BaseCommandModule
{
    private static readonly string[] Responses = {
        "Yes",
        "No",
        "Ask again later",
        "Cannot predict now",
        "Don't count on it",
        "Most likely",
        "Outlook not so good",
        "Reply hazy, try again"
    };

    [Command("8ball")]
    [Description("Ask the magic 8-ball a question.")]
    public async Task EightBallCommand(CommandContext ctx, [RemainingText] string question)
    {
        Random random = new Random();
        int index = random.Next(Responses.Length);
        string response = Responses[index];

        await ctx.RespondAsync($"🎱 **Question:** {question}\n**Answer:** {response}");
    }
}
