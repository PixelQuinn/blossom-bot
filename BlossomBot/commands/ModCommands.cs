using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace BlossomBot.commands
{
    public class ModCommands : BaseCommandModule
    {
        [Command("purge")]
        [Description("Deletes a specified number of messages in the channel.")]
        [RequirePermissions(DSharpPlus.Permissions.ManageMessages)]
        public async Task PurgeCommand(CommandContext ctx, int numberOfMessagesToDelete)
        {
            // Ensure the bot has the necessary permissions to manage messages
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(DSharpPlus.Permissions.ManageMessages))
            {
                await ctx.RespondAsync("You don't have the required permissions to use this command.");
                return;
            }

            // Delete the specified number of messages
            var messages = await ctx.Channel.GetMessagesAsync(numberOfMessagesToDelete + 1); // +1 to include the command message
            await ctx.Channel.DeleteMessagesAsync(messages);

            // Notify that the messages have been deleted
            var confirmationMessage = await ctx.RespondAsync($"Deleted {numberOfMessagesToDelete} messages.");
            await Task.Delay(5000); // Delay for 5 seconds
            await confirmationMessage.DeleteAsync(); // Delete the confirmation message after 5 seconds
        }

        [Command("kick")]
        [Description("Kicks a user from the server.")]
        [RequirePermissions(DSharpPlus.Permissions.KickMembers)]
        public async Task KickCommand(CommandContext ctx, DiscordMember member, string reason = "No reason provided.")
        {
            // Ensure the bot has the necessary permissions to kick members
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(DSharpPlus.Permissions.KickMembers))
            {
                await ctx.RespondAsync("You don't have the required permissions to use this command.");
                return;
            }

            // Kick the specified user
            await member.RemoveAsync(reason);

            // Notify that the user has been kicked
            await ctx.RespondAsync($"Kicked {member.Username}#{member.Discriminator} for: {reason}");
        }

    }
}
