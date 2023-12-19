using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace BlossomBot
{
    public class BasicCommands : BaseCommandModule
    {
        [Command("greet")]
        public async Task GreetCommand(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = $"Hello, {ctx.User.Username}!",
                Color = DiscordColor.Green
            };

            await ctx.Channel.SendMessageAsync(embed: embed);
        }

        [Command("add")]
        public async Task AddCommand(CommandContext ctx, int numberOne, int numberTwo)
        {
            var result = numberOne + numberTwo;

            var embed = new DiscordEmbedBuilder
            {
                Title = $"Result of {numberOne} + {numberTwo}",
                Description = result.ToString(),
                Color = DiscordColor.Orange
            };

            await ctx.Channel.SendMessageAsync(embed: embed);
        }

        [Command("subtract")]
        public async Task SubtractCommand(CommandContext ctx, int numberOne, int numberTwo)
        {
            var result = numberOne - numberTwo;

            var embed = new DiscordEmbedBuilder
            {
                Title = $"Result of {numberOne} - {numberTwo}",
                Description = result.ToString(),
                Color = DiscordColor.Orange
            };

            await ctx.Channel.SendMessageAsync(embed: embed);
        }

        [Command("multiply")]
        public async Task MultiplyCommand(CommandContext ctx, int numberOne, int numberTwo)
        {
            var result = numberOne * numberTwo;

            var embed = new DiscordEmbedBuilder
            {
                Title = $"Result of {numberOne} * {numberTwo}",
                Description = result.ToString(),
                Color = DiscordColor.Orange
            };

            await ctx.Channel.SendMessageAsync(embed: embed);
        }

        [Command("divide")]
        public async Task DivideCommand(CommandContext ctx, int numberOne, int numberTwo)
        {
            // Handle division by zero
            if (numberTwo == 0)
            {
                await ctx.Channel.SendMessageAsync("Cannot divide by zero.");
                return;
            }

            var result = (float)numberOne / numberTwo;

            var embed = new DiscordEmbedBuilder
            {
                Title = $"Result of {numberOne} / {numberTwo}",
                Description = result.ToString("F2"), // Format as a floating-point number with 2 decimal places
                Color = DiscordColor.Orange
            };

            await ctx.Channel.SendMessageAsync(embed: embed);
        }

        // Add embedded message to the existing roll command
        [Command("roll")]
        public async Task RollCommand(CommandContext ctx, int numberOfDice = 1, int sides = 6)
        {
            // Validate input values
            if (numberOfDice <= 0 || sides <= 1)
            {
                await ctx.Channel.SendMessageAsync("Please provide valid values for the number of dice and sides.");
                return;
            }

            // Roll the dice and generate results
            var random = new Random();
            var results = Enumerable.Range(0, numberOfDice)
                                    .Select(_ => random.Next(1, sides + 1))
                                    .ToList();

            // Send a message to the channel with the results
            var embed = new DiscordEmbedBuilder
            {
                Title = $"{ctx.User.Username} rolled {string.Join(", ", results)}",
                Color = DiscordColor.Teal
            };

            await ctx.Channel.SendMessageAsync(embed: embed);
        }

        [Command("flipcoin")]
        [Description("Flips a virtual coin and sends the result.")]
        public async Task FlipCoinCommand(CommandContext ctx)
        {
            // Generate a random result: 0 for heads, 1 for tails
            int result = new Random().Next(2);

            // Interpret the result and send a message

            // Determine the outcome based on the result
            string outcome = result == 0 ? "Heads" : "Tails";

            // Send a message to the channel indicating the outcome of the coin flip
            var embed = new DiscordEmbedBuilder
            {
                Title = $"The coin landed on: {outcome}!",
                Color = DiscordColor.Gold
            };

            await ctx.Channel.SendMessageAsync(embed: embed);
        }

        [Command("rng")]
        [Description("Generates and sends a random number within a specified range.")]
        public async Task RandomNumberCommand(CommandContext ctx, int minValue = 0, int maxValue = 100)
        {
            // Validate input to ensure maxValue is greater than or equal to minValue
            if (maxValue < minValue)
            {
                // If the provided range is invalid, send an error message and exit the method
                await ctx.Channel.SendMessageAsync("Invalid range. Max value must be greater than or equal to min value.");
                return;
            }

            // Generate a random number within the specified range (inclusive)
            int randomNumber = new Random().Next(minValue, maxValue + 1);

            // Send the random number as a message to the channel
            var embed = new DiscordEmbedBuilder
            {
                Title = $"Random number between {minValue} and {maxValue}",
                Description = randomNumber.ToString(),
                Color = DiscordColor.Purple
            };

            await ctx.Channel.SendMessageAsync(embed: embed);
        }

        [Command("documentation")]
        [Description("Provides a link to the bot's documentation.")]
        public async Task DocumentationCommand(CommandContext ctx)
        {
            // Replace the placeholder URL with your actual documentation URL
            string documentationUrl = "https://github.com/PixelQuinn/blossom-bot";

            await ctx.Channel.SendMessageAsync($"Check out the bot's documentation here: {documentationUrl}");
        }
    }
}