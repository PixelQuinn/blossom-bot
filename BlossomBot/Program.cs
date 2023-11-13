// Import necessary libraries
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlossomBot.config;

// Define the namespace for your bot
namespace BlossomBot
{
    // Main class for your bot
    internal class Program
    {
        // Properties for Discord client and CommandsNext extension
        private static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands {  get; set; }

        // Main method, the entry point for your bot
        static async void Main(string[] args)
        {
            // Create an instance of JSONReader to read configuration from JSON
            var jsonReader = new JSONReader();

            // Read the JSON configuration
            await jsonReader.ReadJSON();

            // Configure Discord client with the obtained token
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            // Initialize the Discord client
            Client = new DiscordClient(discordConfig);

            // Register the Ready event handler
            Client.Ready += Client_Ready;
        }

        // Event handler for the Ready event of the Discord client
        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
