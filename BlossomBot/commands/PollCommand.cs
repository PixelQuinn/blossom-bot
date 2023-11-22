using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace BlossomBot
{
    public class PollCommands : BaseCommandModule
    {
        [Command("poll")]
        [Description("Create a poll with multiple choices.")]
        public async Task PollCommand(CommandContext ctx, [RemainingText] string pollQuestion)
        {
            // Check if the user provided a question and choices
            if (string.IsNullOrWhiteSpace(pollQuestion))
            {
                await ctx.Channel.SendMessageAsync("Please provide a poll question and choices.");
                return;
            }

            // Split the input into the question and choices
            string[] pollOptions = pollQuestion.Split('|').Select(option => option.Trim()).ToArray();

            // Check if there are at least two choices
            if (pollOptions.Length < 2)
            {
                await ctx.Channel.SendMessageAsync("Please provide at least two poll choices, separated by '|'.");
                return;
            }

            // Build the poll message
            string pollMessage = $"**Poll: {pollOptions[0]}**\n";
            for (int i = 1; i < pollOptions.Length; i++)
            {
                pollMessage += $":regional_indicator_{(char)('a' + i - 1)}: {pollOptions[i]}\n";
            }

            // Send the poll message
            var poll = await ctx.Channel.SendMessageAsync(pollMessage);

            // Add reactions to the poll message for each choice
            for (int i = 1; i < pollOptions.Length; i++)
            {
                await poll.CreateReactionAsync(DSharpPlus.Entities.DiscordEmoji.FromName(ctx.Client, $":regional_indicator_{(char)('a' + i - 1)}:"));
            }
        }
    }
}
