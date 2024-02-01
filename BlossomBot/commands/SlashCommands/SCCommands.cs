using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace BlossomBot.commands.SlashCommands
{
    public class SCCommands : ApplicationCommandModule
    {
        [SlashCommand("test", "This is my first slash command.")]
        public async Task MyFirstSlashCommand(InteractionContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Hello World!");
        }
    }
}
