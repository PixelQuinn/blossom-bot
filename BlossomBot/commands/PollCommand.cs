using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

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

            // Build the poll message with an embedded message
            var embed = new DiscordEmbedBuilder
            {
                Title = $"Poll: {pollOptions[0]}",
                Description = string.Join("\n", pollOptions.Skip(1).Select((choice, index) => $":regional_indicator_{(char)('a' + index)}: {choice}")),
                Color = DiscordColor.Blue
            };

            // Send the embedded poll message
            var pollMessage = await ctx.Channel.SendMessageAsync(embed: embed);

            // Add reactions to the poll message for each choice
            for (int i = 1; i < pollOptions.Length; i++)
            {
                await pollMessage.CreateReactionAsync(DSharpPlus.Entities.DiscordEmoji.FromName(ctx.Client, $":regional_indicator_{(char)('a' + i - 1)}:"));
            }
        }
    }
}
