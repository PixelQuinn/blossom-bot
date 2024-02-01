﻿using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;
using System.Linq;
using OpenAI_API;
using System.Threading.Tasks;

namespace BlossomBot.commands.SlashCommands
{
    public class SCCommands : ApplicationCommandModule
    {
        [SlashCommand("image", "Google image search.")]
        public async Task GoogleImageSearch(InteractionContext ctx, [Option("search", "Your search query")] string search)
        {
            await ctx.DeferAsync();

            string apiKey = "insert api key here";
            string cseId = "insert custom search engine id";

            var customSearchService = new CustomSearchAPIService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "Blossombot"
            });

            var listRequest = customSearchService.Cse.List();
            listRequest.Cx = cseId;
            listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            listRequest.Q = search;

            var searchRequest = await listRequest.ExecuteAsync();
            var results = searchRequest.Items;

            if (results == null || !results.Any())
            {
                await ctx.EditResponseAsync(new DSharpPlus.Entities.DiscordWebhookBuilder().WithContent("No results found."));
                return;
            }

            var firstResult = results.First();
            var embed = new DiscordEmbedBuilder()
            {
                Title = "Results for Search: " + search,
                ImageUrl = firstResult.Link,
                Color = DiscordColor.Azure
            };
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
        }

        [SlashCommand("chat-gpt", "Ask ChatGpt a question")]
        public async Task ChatGPT(InteractionContext ctx, [Option("query", "What you want to ask ChatGPT")] string query)
        {
            await ctx.DeferAsync();

            var api = new OpenAIAPI("API key here");

            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("Type in a query");

            chat.AppendUserInput(query);

            string response = await chat.GetResponseFromChatbotAsync();

            var outputEmbed = new DiscordEmbedBuilder()
            {
                Title = "Results to: " + query,
                Description = response,
                Color = DiscordColor.Green
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(outputEmbed));
        }
    }
}
