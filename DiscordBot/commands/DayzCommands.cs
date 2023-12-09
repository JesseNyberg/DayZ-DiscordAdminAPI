using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dayz_Discord_Bot.commands
{
    public class DayzCommands : ApplicationCommandModule
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string postDataUrl => $"http://{apiSettings.apiIp}:{apiSettings.apiPort}/api/postdata";
        private string discordPostDataUrl => $"http://{apiSettings.apiIp}:{apiSettings.apiPort}/api/discordpostdata";
        private string getDiscordDataUrl => $"http://{apiSettings.apiIp}:{apiSettings.apiPort}/api/discordgetdata";


        [SlashCommand("broadcast", "Broadcasts a message with a title and duration.")]
        public async Task BroadcastCommand(
            InteractionContext ctx,
            [Option("Title", "The title of the broadcast")] string argument1,
            [Option("Duration", "The duration of the broadcast in seconds")] string argument2, 
            [Option("Message", "The message to be broadcasted")] string argument3)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                                                                .WithContent("Processing your command..."));

            var payload = new
            {
                commandName = ctx.CommandName,
                argument1,
                argument2,
                argument3
            };

            string responseMessage = await SendPostRequest(payload);
            await ctx.Channel.SendMessageAsync(responseMessage);

            // Checking if the command was successful or not
            await Task.Delay(10000);
            string statusMessage = await CheckCommandStatus();
            await ctx.Channel.SendMessageAsync(statusMessage);

            string clearResponse = await ClearDiscordPostData();
        }

        [SlashCommand("teleport", "Teleports a player to another player's location.")]
        public async Task TeleportCommand(
            InteractionContext ctx,
            [Option("PlayerName", "The name of the player to teleport")] string argument1,
            [Option("OtherPlayerName", "The name of the other player (destination)")] string argument2)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                                                    .WithContent("Processing your command..."));

            var payload = new
            {
                commandName = ctx.CommandName,
                argument1,
                argument2
            };

            string responseMessage = await SendPostRequest(payload);
            await ctx.Channel.SendMessageAsync(responseMessage);

            // Checking if the command was successful or not
            await Task.Delay(10000);
            string statusMessage = await CheckCommandStatus();
            await ctx.Channel.SendMessageAsync(statusMessage);

            string clearResponse = await ClearDiscordPostData();
        }

        [SlashCommand("playerlist", "Retrieves a list of current players.")]
        public async Task PlayerListCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                                        .WithContent("Processing your command..."));

            var payload = new
            {
                commandName = ctx.CommandName
            };

            string responseMessage = await SendPostRequest(payload);
            await ctx.Channel.SendMessageAsync(responseMessage);

            // Checking if the command was successful or not
            await Task.Delay(10000);
            string statusMessage = await CheckCommandStatus();
            await ctx.Channel.SendMessageAsync(statusMessage);

            string clearResponse = await ClearDiscordPostData();
            await ClearJsonData();
        }

        [SlashCommand("teleportcoordinate", "Teleports a player to specific coordinates.")]
        public async Task TeleportCoordinateCommand(
            InteractionContext ctx,
            [Option("PlayerName", "The name of the player to teleport")] string argument1,
            [Option("Coordinates", "The coordinates to teleport the player to (X Y Z) without commas")] string argument2)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                            .WithContent("Processing your command..."));

            var payload = new
            {
                commandName = ctx.CommandName,
                argument1,
                argument2
            };

            string responseMessage = await SendPostRequest(payload);
            await ctx.Channel.SendMessageAsync(responseMessage);

            // Checking if the command was successful or not
            await Task.Delay(10000);
            string statusMessage = await CheckCommandStatus();
            await ctx.Channel.SendMessageAsync(statusMessage);

            string clearResponse = await ClearDiscordPostData();
        }

        [SlashCommand("kickplayer", "Kicks a player from the server.")]
        public async Task KickCommand(
            InteractionContext ctx,
            [Option("PlayerName", "The name of the player to kick")] string argument1,
            [Option("Reason", "The reason for kicking the player")] string argument2)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                            .WithContent("Processing your command..."));

            var payload = new
            {
                commandName = ctx.CommandName,
                argument1,
                argument2
            };

            string responseMessage = await SendPostRequest(payload);
            await ctx.Channel.SendMessageAsync(responseMessage);

            // Checking if the command was successful or not
            await Task.Delay(10000);
            string statusMessage = await CheckCommandStatus();
            await ctx.Channel.SendMessageAsync(statusMessage);

            string clearResponse = await ClearDiscordPostData();
        }

        [SlashCommand("banplayer", "Bans a player from the server.")]
        public async Task BanCommand(
            InteractionContext ctx,
            [Option("PlayerName", "The name of the player to ban")] string argument1,
            [Option("Reason", "The reason for banning the player")] string argument2)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                .WithContent("Processing your command..."));

            var payload = new
            {
                commandName = ctx.CommandName,
                argument1,
                argument2
            };

            string responseMessage = await SendPostRequest(payload);
            await ctx.Channel.SendMessageAsync(responseMessage);

            // Checking if the command was successful or not
            await Task.Delay(10000);
            string statusMessage = await CheckCommandStatus();
            await ctx.Channel.SendMessageAsync(statusMessage);

            string clearResponse = await ClearDiscordPostData();
        }

        [SlashCommand("unbanplayer", "Unbans a player from the server.")]
        public async Task UnbanCommand(
            InteractionContext ctx,
            [Option("PlayerName", "The name of the player to unban")] string argument1)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                    .WithContent("Processing your command..."));

            var payload = new
            {
                commandName = ctx.CommandName,
                argument1
            };

            string responseMessage = await SendPostRequest(payload);
            await ctx.Channel.SendMessageAsync(responseMessage);

            // Checking if the command was successful or not
            await Task.Delay(10000);
            string statusMessage = await CheckCommandStatus();
            await ctx.Channel.SendMessageAsync(statusMessage);

            string clearResponse = await ClearDiscordPostData();
        }

        [SlashCommand("healplayer", "Heals the targeted player.")]
        public async Task HealCommand(
            InteractionContext ctx,
            [Option("PlayerName", "The name of the player to heal")] string argument1)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                    .WithContent("Processing your command..."));

            var payload = new
            {
                commandName = ctx.CommandName,
                argument1
            };

            string responseMessage = await SendPostRequest(payload);
            await ctx.Channel.SendMessageAsync(responseMessage);

            // Checking if the command was successful or not
            await Task.Delay(10000);
            string statusMessage = await CheckCommandStatus();
            await ctx.Channel.SendMessageAsync(statusMessage);

            string clearResponse = await ClearDiscordPostData();
        }

        [SlashCommand("spawn", "Spawns items for a player.")]
        public async Task SpawnCommand(
            InteractionContext ctx,
            [Option("PlayerName", "The name of the player to spawn items for")] string argument1,
            [Option("ClassName", "The class name of the item to spawn")] string argument2,
            [Option("Amount", "The amount of items to spawn")] string argument3)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
        .WithContent("Processing your command..."));

            var payload = new
            {
                commandName = ctx.CommandName,
                argument1,
                argument2,
                argument3
            };

            string responseMessage = await SendPostRequest(payload);
            await ctx.Channel.SendMessageAsync(responseMessage);

            // Checking if the command was successful or not
            await Task.Delay(10000);
            string statusMessage = await CheckCommandStatus();
            await ctx.Channel.SendMessageAsync(statusMessage);

            string clearResponse = await ClearDiscordPostData();
        }

        private async Task<string> SendPostRequest(object payload)
        {
            try
            {
                string jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("api_key", apiSettings.apiKey);

                var response = await _httpClient.PostAsync(postDataUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    return "Command sent successfully.";
                }
                else
                {
                    return $"Failed to send command. Response status: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Error occurred: {ex.Message}";
            }
        }
        private async Task<string> CheckCommandStatus()
        {
            try
            {
                var response = await _httpClient.GetAsync(getDiscordDataUrl);
                if (response.IsSuccessStatusCode)
                {
                    string status = await response.Content.ReadAsStringAsync();
                    if (status.StartsWith("Players:"))
                    {
                        return status; 
                    }

                    switch (status.Trim())
                    {
                        case "OK":
                            return "The command was executed successfully.";
                        case "FAIL":
                            return "The command failed to execute.";
                        default:
                            return "The command's execution status is unknown.";
                    }
                }
                else
                {
                    return "Failed to retrieve command status.";
                }
            }
            catch (Exception ex)
            {
                return $"Error occurred while retrieving command status: {ex.Message}";
            }
        }

        private async Task<string> ClearDiscordPostData()
        {
            try
            {
                var content = new StringContent("", Encoding.UTF8, "text/plain");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("api_key", apiSettings.apiKey);

                var response = await _httpClient.PostAsync(discordPostDataUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    return "Discord data cleared successfully.";
                }
                else
                {
                    return "Failed to clear Discord data.";
                }
            }
            catch (Exception ex)
            {
                return $"Error occurred while clearing Discord data: {ex.Message}";
            }
        }

        private async Task ClearJsonData()
        {
            var resetPayload = new
            {
                commandName = "reset"
            };

            string jsonPayload = JsonConvert.SerializeObject(resetPayload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("api_key", apiSettings.apiKey);

            var response = await _httpClient.PostAsync(postDataUrl, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("JSON data reset successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to reset JSON data. Response status: {response.StatusCode}");
            }
        }

    }
}
