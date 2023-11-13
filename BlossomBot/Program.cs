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
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All
            };
        }
    }
}
