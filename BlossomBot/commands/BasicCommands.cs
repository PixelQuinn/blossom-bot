using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlossomBot.commands;
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
                Title = $"Hello, {ctx.User.Username}!\nI am Blossombot.~",
                Color = DiscordColor.Green
            };

            await ctx.Channel.SendMessageAsync(embed: embed);
        }

        [Command("calculate")]
        public async Task CalculateCommand(CommandContext ctx, [RemainingText] string expression)
        {
            try
            {
                var result = EvaluateExpression(expression);

                var embed = new DiscordEmbedBuilder
                {
                    Title = $"Result of {expression}",
                    Description = result.ToString("F2"),
                    Color = DiscordColor.Orange
                };

                await ctx.Channel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                await ctx.Channel.SendMessageAsync($"Error: {ex.Message}");
            }
        }

        private float EvaluateExpression(string expression)
        {
            // You can use a library or implement your own expression evaluator here
            // For simplicity, this example uses DataTable.Compute method
            DataTable dataTable = new DataTable();
            var result = dataTable.Compute(expression, "");
            return Convert.ToSingle(result);
        }


        [Command("roll")]
        public async Task RollCommand(CommandContext ctx, string input = "1d6")
        {
            // Parse input string to get number of dice, sides, and modifiers
            var match = Regex.Match(input, @"^(\d+)d(\d+)([+-]\d+)?$");

            if (!match.Success)
            {
                await ctx.Channel.SendMessageAsync("Invalid input format. Please use the format !roll 1d6 or !roll 3d10 or !roll 2d6+5.");
                return;
            }

            int numberOfDice = int.Parse(match.Groups[1].Value);
            int sides = int.Parse(match.Groups[2].Value);

            // Validate input values
            if (numberOfDice <= 0 || sides <= 1)
            {
                await ctx.Channel.SendMessageAsync("Please provide valid values for the number of dice and sides.");
                return;
            }

            // Parse the modifier if present
            int modifier = 0;
            if (match.Groups[3].Success)
            {
                modifier = int.Parse(match.Groups[3].Value);
            }

            // Roll the dice and apply the modifier
            var random = new Random();
            var results = Enumerable.Range(0, numberOfDice)
                                    .Select(_ => random.Next(1, sides + 1))
                                    .ToList();

            int total = results.Sum() + modifier;

            // Format individual dice rolls
            string individualRolls = string.Join(", ", results);

            // Send a message to the channel with the results
            var embed = new DiscordEmbedBuilder
            {
                Title = $"{ctx.User.Username} rolled {input}",
                Description = $"Individual Rolls: {individualRolls}\nTotal: {total}",
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

        [Command("docs")]
        [Description("Provides a link to the bot's documentation.")]
        public async Task DocumentationCommand(CommandContext ctx)
        {
            // Replace the placeholder URL with your actual documentation URL
            string documentationUrl = "https://github.com/PixelQuinn/blossom-bot";

            await ctx.Channel.SendMessageAsync($"Check out the bot's documentation here: {documentationUrl}");
        }

        [Command("help")]
        public async Task Help(CommandContext ctx)
        {
            var prefix = "!"; // Change this to your bot's prefix

            var embed = new DiscordEmbedBuilder
            {
                Title = "Command List",
                Description = $"Here are the available commands. Use `{prefix}command` to execute a command:",
                Color = DiscordColor.Blue,
            };

            var commands = ctx.CommandsNext.RegisteredCommands.Values
                .Where(c => !c.IsHidden && !IsModCommand(c))
                .OrderBy(c => c.Name);

            foreach (var command in commands)
            {
                embed.AddField($"{prefix}{command.Name}", command.Description ?? "No description available.", inline: false);
            }

            await ctx.RespondAsync(embed: embed);
        }

        private bool IsModCommand(Command command)
        {
            return command.CustomAttributes.Any(attr => attr.GetType() == typeof(ModCommandAttribute));
        }

    }
}