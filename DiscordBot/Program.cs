using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Dayz_Discord_Bot.commands;
using DSharpPlus.SlashCommands;

namespace Dayz_Discord_Bot
{
    public static class apiSettings
    {
        public static string apiIp { get; set; }
        public static string apiPort { get; set; }
        public static string apiKey { get; set; }
    }

    internal class Program
    {
        private static DiscordClient Client { get; set; }

        static async Task Main(string[] args)
        {
            string iniFilePath = "config.ini";

            if (!File.Exists(iniFilePath))
            {
                using (StreamWriter writer = new StreamWriter(iniFilePath))
                {
                    writer.WriteLine("[Bot]");
                    writer.WriteLine("Token=YOUR_BOT_TOKEN_HERE");
                    writer.WriteLine("API_IP=YOUR_API_IP_HERE");
                    writer.WriteLine("API_PORT=YOUR_API_PORT_HERE");
                    writer.WriteLine("API_KEY=YOUR_API_KEY_HERE");
                }

                Console.WriteLine("config.ini file created. Please enter your bot token in the file.");
                PauseBeforeExit();
                return;
            }

            var configuration = new ConfigurationBuilder()
                .AddIniFile(iniFilePath)
                .Build();

            string token = configuration["Bot:Token"];
            apiSettings.apiIp = configuration["Bot:API_IP"];
            apiSettings.apiPort = configuration["Bot:API_PORT"];
            apiSettings.apiKey = configuration["Bot:API_KEY"];


            if (string.IsNullOrWhiteSpace(token) || token == "YOUR_BOT_TOKEN_HERE")
            {
                Console.WriteLine("Please enter your bot token in the config.ini file.");
                PauseBeforeExit();
                return;
            }

            if (string.IsNullOrWhiteSpace(apiSettings.apiIp) || apiSettings.apiIp == "YOUR_API_IP_HERE")
            {
                Console.WriteLine("Please enter your API's IP in the config.ini file.");
                PauseBeforeExit();
                return;
            }

            if (string.IsNullOrWhiteSpace(apiSettings.apiPort) || apiSettings.apiPort == "YOUR_API_PORT_HERE")
            {
                Console.WriteLine("Please enter your API's port in the config.ini file.");
                PauseBeforeExit();
                return;
            }

            if (string.IsNullOrWhiteSpace(apiSettings.apiKey) || apiSettings.apiKey == "YOUR_API_KEY_HERE")
            {
                Console.WriteLine("Please enter your API key in the config.ini file.");
                PauseBeforeExit();
                return;
            }

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfig);

            Client.Ready += Client_Ready;

            var slashCommandsConfig = Client.UseSlashCommands();

            slashCommandsConfig.RegisterCommands<DayzCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }

        private static void PauseBeforeExit()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
