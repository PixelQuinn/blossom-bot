// Import necessary namespaces
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace BlossomBot
{
    // Define a class for basic commands, inheriting from BaseCommandModule
    public class BasicCommands : BaseCommandModule
    {
        // Define a command called "greet" that takes no additional parameters
        [Command("greet")]
        public async Task GreetCommand(CommandContext ctx)
        {
            // Send a message to the channel with a greeting mentioning the user's username
            await ctx.Channel.SendMessageAsync($"Hello, {ctx.User.Username}!");
        }

        // Define a command called "add" that takes two integer parameters
        [Command("add")]
        public async Task AddCommand(CommandContext ctx, int numberOne, int numberTwo)
        {
            // Send a message to the channel with the result of adding the two numbers
            await ctx.Channel.SendMessageAsync($"{numberOne} + {numberTwo} = {numberOne + numberTwo}");
        }

        // Define a command called "subtract" that takes two integer parameters
        [Command("subtract")]
        public async Task SubtractCommand(CommandContext ctx, int numberOne, int numberTwo)
        {
            // Send a message to the channel with the result of subtracting the second number from the first
            await ctx.Channel.SendMessageAsync($"{numberOne} - {numberTwo} = {numberOne - numberTwo}");
        }

        // Define a command called "multiply" that takes two integer parameters
        [Command("multiply")]
        public async Task MultiplyCommand(CommandContext ctx, int numberOne, int numberTwo)
        {
            // Send a message to the channel with the result of multiplying the two numbers
            await ctx.Channel.SendMessageAsync($"{numberOne} * {numberTwo} = {numberOne * numberTwo}");
        }

        // Define a command called "divide" that takes two integer parameters
        [Command("divide")]
        public async Task DivideCommand(CommandContext ctx, int numberOne, int numberTwo)
        {
            // Send a message to the channel with the result of dividing the first number by the second
            // Note: This code does not handle division by zero, which could result in an exception
            await ctx.Channel.SendMessageAsync($"{numberOne} / {numberTwo} = {numberOne / numberTwo}");
        }

        // Define a class for dice-related commands, inheriting from BaseCommandModule
        [Command("r1d6")]
        public async Task RollCommand(CommandContext ctx, int sides = 6)
        {
            // Send a message to the channel with the result of rolling a die with the specified number of sides
            await ctx.Channel.SendMessageAsync($"{ctx.User.Username} rolled a {new System.Random().Next(1, sides + 1)}");
        }
    }
}