using System;
using System.Collections.Generic; // Added for Dictionary
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Timers;

namespace BlossomBot
{
    // Class for reminder-related commands, inheriting from BaseCommandModule
    public class ReminderCommands : BaseCommandModule
    {
        // Dictionary to store timers for each user
        private static readonly Dictionary<ulong, Timer> Timers = new Dictionary<ulong, Timer>();

        // Command to set a reminder for a specified time
        [Command("remind")]
        [Description("Sets a reminder for a specified time and sends a notification.")]
        public async Task RemindCommand(CommandContext ctx, string time, [RemainingText] string reminder)
        {
            // Parse the time string to a TimeSpan
            if (!TimeSpan.TryParse(time, out TimeSpan reminderTime))
            {
                // Send an error message for invalid time format
                await ctx.Channel.SendMessageAsync("Invalid time format. Please use HH:mm:ss.");
                return;
            }

            // Calculate the total time in milliseconds until the reminder
            double totalMilliseconds = reminderTime.TotalMilliseconds;

            // Create a timer for the user and start it
            Timer timer = new Timer(totalMilliseconds);
            timer.Elapsed += async (sender, e) => await SendReminder(ctx, reminder);
            timer.AutoReset = false; // Set to false to trigger only once
            timer.Start();

            // Store the timer for later reference
            Timers[ctx.User.Id] = timer;

            // Send a confirmation message
            await ctx.Channel.SendMessageAsync($"Reminder set for {reminderTime:g}.");
        }

        // Method to send the reminder message
        private async Task SendReminder(CommandContext ctx, string reminder)
        {
            // Send the reminder message mentioning the user
            await ctx.Channel.SendMessageAsync($"Reminder for {ctx.User.Mention}: {reminder}");

            // Remove the timer after it has been triggered
            Timers.Remove(ctx.User.Id);
        }
    }
}
