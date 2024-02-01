﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;

namespace BlossomBot.commands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ModCommandAttribute : Attribute { }
    public class ModCommands : BaseCommandModule
    {
        [Command("purge")]
        [Description("Deletes a specified number of messages in the channel.")]
        [RequirePermissions(DSharpPlus.Permissions.ManageMessages)]
        [ModCommand]
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
        [ModCommand]
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

        [Command("ban")]
        [Description("Bans a user from the server.")]
        [RequirePermissions(DSharpPlus.Permissions.BanMembers)]
        [ModCommand]
        public async Task BanCommand(CommandContext ctx, DiscordMember member, string reason = "No reason provided.")
        {
            // Ensure the bot has the necessary permissions to ban members
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(DSharpPlus.Permissions.BanMembers))
            {
                await ctx.RespondAsync("You don't have the required permissions to use this command.");
                return;
            }

            // Ban the specified user
            await member.BanAsync(0, reason);

            // Notify that the user has been banned
            await ctx.RespondAsync($"Banned {member.Username}#{member.Discriminator} for: {reason}");
        }

        [Command("giverole")]
        [Description("Gives a role to a user.")]
        [RequirePermissions(DSharpPlus.Permissions.ManageRoles)]
        [ModCommand]
        public async Task GiveRoleCommand(CommandContext ctx, DiscordMember member, DiscordRole role)
        {
            // Ensure the bot has the necessary permissions to manage roles
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(DSharpPlus.Permissions.ManageRoles))
            {
                await ctx.RespondAsync("You don't have the required permissions to use this command.");
                return;
            }

            // Give the specified role to the user
            await member.GrantRoleAsync(role);

            // Notify that the role has been given
            await ctx.RespondAsync($"Gave the role {role.Name} to {member.Username}#{member.Discriminator}.");
        }

        [Command("takerole")]
        [Description("Removes a role from a user.")]
        [RequirePermissions(DSharpPlus.Permissions.ManageRoles)]
        [ModCommand]
        public async Task TakeRoleCommand(CommandContext ctx, DiscordMember member, DiscordRole role)
        {
            // Ensure the bot has the necessary permissions to manage roles
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(DSharpPlus.Permissions.ManageRoles))
            {
                await ctx.RespondAsync("You don't have the required permissions to use this command.");
                return;
            }

            // Take the specified role from the user
            await member.RevokeRoleAsync(role);

            // Notify that the role has been taken
            await ctx.RespondAsync($"Took the role {role.Name} from {member.Username}#{member.Discriminator}.");
        }

    }
}
