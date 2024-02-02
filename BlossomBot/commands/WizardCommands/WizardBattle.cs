using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

public class WizardRPSCommands : BaseCommandModule
{
    private readonly Dictionary<string, string> wizardChoices = new Dictionary<string, string>
    {
        { "f", "fireball" },
        { "l", "lightning strike" },
        { "i", "ice shard" }
    };

    [Command("wizardrps")]
    [Description("Play wizard-themed rock-paper-scissors with the bot.")]
    public async Task WizardRPSCommand(CommandContext ctx, [Description("Your choice: f (fireball), l (lightning strike), i (ice shard)")] string playerChoice)
    {
        // Ensure the player's choice is valid
        if (!wizardChoices.TryGetValue(playerChoice.ToLower(), out string fullPlayerChoice))
        {
            await ctx.RespondAsync("Invalid choice. Please choose from f (fireball), l (lightning strike), or i (ice shard).");
            return;
        }

        // Bot randomly selects a choice
        Random random = new Random();
        string botChoiceKey = wizardChoices.Keys.ElementAt(random.Next(wizardChoices.Count));
        string botChoice = wizardChoices[botChoiceKey];

        // Determine the winner
        string result = GetRPSResult(fullPlayerChoice, botChoice);

        // Display the results
        await ctx.RespondAsync($"You chose: {fullPlayerChoice}\nBot chose: {botChoice}\nResult: {result}");
    }

    private string GetRPSResult(string playerChoice, string botChoice)
    {
        if (playerChoice == botChoice)
        {
            return "It's a tie!";
        }
        else if ((playerChoice == "fireball" && botChoice == "ice shard") ||
                 (playerChoice == "lightning strike" && botChoice == "fireball") ||
                 (playerChoice == "ice shard" && botChoice == "lightning strike"))
        {
            return "You win!";
        }
        else
        {
            return "Bot wins!";
        }
    }
}
