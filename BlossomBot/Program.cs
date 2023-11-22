// Import necessary libraries
using BlossomBot.config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
        static async Task Main(string[] args)
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

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<BasicCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        // Event handler for the Ready event of the Discord client
        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
