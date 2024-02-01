using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlossomBot
{
    public class PollCommands : BaseCommandModule
    {
        [Command("poll")]
        [Description("Creates a poll with the specified title and options.")]
        public async Task Poll(CommandContext ctx, string pollTitle, params string[] options)
        {
            var interactivity = Program.Client.GetInteractivity();
            var pollTime = TimeSpan.FromSeconds(10);

            DiscordEmoji[] emojiOptions = { DiscordEmoji.FromName(Program.Client, ":one:"),
                                            DiscordEmoji.FromName(Program.Client, ":two:"),
                                            DiscordEmoji.FromName(Program.Client, ":three:"),
                                            DiscordEmoji.FromName(Program.Client, ":four:") };

            if (options.Length > emojiOptions.Length)
            {
                await ctx.Channel.SendMessageAsync("Too many options provided. Maximum allowed: " + emojiOptions.Length);
                return;
            }

            string optionsDescription = string.Join("\n", options.Select((option, index) =>
                $"{emojiOptions[index]} | {option}"));

            var pollMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Red,
                Title = pollTitle,
                Description = optionsDescription
            };

            var sentPoll = await ctx.Channel.SendMessageAsync(embed: pollMessage);
            foreach (var emoji in emojiOptions.Take(options.Length))
            {
                await sentPoll.CreateReactionAsync(emoji);
            }

            var totalReactions = await interactivity.CollectReactionsAsync(sentPoll, pollTime);

            var counts = new int[emojiOptions.Length];
            foreach (var emoji in totalReactions)
            {
                for (int i = 0; i < emojiOptions.Length; i++)
                {
                    if (emoji.Emoji == emojiOptions[i])
                    {
                        counts[i]++;
                        break;
                    }
                }
            }

            int totalVotes = counts.Sum();

            // Find the winning option
            int maxVotes = counts.Max();
            int winningOptionIndex = Array.IndexOf(counts, maxVotes);
            string winningOption = options[winningOptionIndex];

            // Display simplified winning message as an embedded message
            var resultEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = "Poll Results",
                Description = $"The winner is {winningOption} with {maxVotes} votes!",
            };

            await ctx.Channel.SendMessageAsync(embed: resultEmbed);
        }
    }
}
