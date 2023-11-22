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
    }
}
