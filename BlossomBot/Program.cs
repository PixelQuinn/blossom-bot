﻿// Import necessary libraries
using BlossomBot.commands;
using BlossomBot.commands.SlashCommands;
using BlossomBot.config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

// Define the namespace for your bot
namespace BlossomBot
{
    // Main class for your bot
    internal class Program
    {
        // Properties for Discord client and CommandsNext extension
        public static DiscordClient Client { get; set; }
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

            // Set default timeout for Commands that use interactivity
            Client.UseInteractivity(new DSharpPlus.Interactivity.InteractivityConfiguration
            {
                Timeout = System.TimeSpan.FromMinutes(2)
            });

            // Register the Ready event handler
            Client.Ready += Client_Ready;
            Client.MessageCreated += MessageCreatedHandler;

            // Create a configuration object for CommandsNext with the specified settings
            var commandsConfig = new CommandsNextConfiguration()
            {
                // Set the command prefix to the value stored in the jsonReader object
                StringPrefixes = new[] { jsonReader.prefix },

                // Enable the bot to respond to mentions as a command prefix
                EnableMentionPrefix = true,

                // Enable the bot to respond to commands in direct messages (DMs)
                EnableDms = true,

                // Disable the default help command provided by CommandsNext
                EnableDefaultHelp = false
            };

            // Set up CommandsNext with the specified configuration
            Commands = Client.UseCommandsNext(commandsConfig);

            // Register the commands defined in the BasicCommands class
            Commands.RegisterCommands<BasicCommands>();

            // Regist the commands in the ModCommands Class
            Commands.RegisterCommands<ModCommands>();

            // Register blossom's wizard powers
            Commands.RegisterCommands<WizardBlossomCommands>();

            // Register blossom's spellbook
            Commands.RegisterCommands<SpellbookCommands>();

            // Register the commands defined in the ReminderCommands class
            Commands.RegisterCommands<ReminderCommands>();


            // Register the commands defined in the PollCommands class
            Commands.RegisterCommands<PollCommands>();

            // Slash command register
            var slashCommandsConfiguration = Client.UseSlashCommands();
            slashCommandsConfiguration.RegisterCommands<SCCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task MessageCreatedHandler(DiscordClient sender, DSharpPlus.EventArgs.MessageCreateEventArgs e)
        {
            // Convert the message content to lowercase
            string messageContentLower = e.Message.Content.ToLower();

            // Check for the "hello" trigger
            if (messageContentLower.Contains("hello") && !e.Message.Author.IsBot)
            {
                // Respond with a greeting
                await e.Message.RespondAsync("Hello there!");
            }

            // Check for variations of "i cant"
            if (messageContentLower.Contains("i cant") || messageContentLower.Contains("i can't"))
            {
                // Respond with a specific message
                await e.Message.RespondAsync("That sounds like a skill issue...");
            }

            // Check for variations of "how"
            if (messageContentLower.Contains("how") && !e.Message.Author.IsBot)
            {
                // Respond with "git gud"
                await e.Message.RespondAsync("Git gud, I guess.");
            }

            // Check for variations of "uwu"
            if (messageContentLower.Contains("uwu") && !e.Message.Author.IsBot)
            {
                // Respond with "UwU"
                await e.Message.RespondAsync("UwU");
            }
        }




        // Event handler for the Ready event of the Discord client
        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
