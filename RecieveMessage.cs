using Discord;
using Discord.WebSocket;
using System.Diagnostics;

namespace FlowDiscord
{
    internal class RecieveMessage
    {
        private static DiscordSocketClient _client;

        public async Task StartAsync()
        {
            var socketConfig = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
            };

            _client = new DiscordSocketClient(socketConfig);

            var token = "BOT TOKEN";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.MessageReceived += MessageReceived;
            _client.Ready += () =>
            {
                MainWindow.CreateNewMessage("Discordに接続しました");
                return Task.CompletedTask;
            };

            await Task.Delay(-1);
        }

        private static Task MessageReceived(SocketMessage message)
        {
            if (message != null)
            {
                MainWindow.CreateNewMessage(message.Content);
            }

            return Task.CompletedTask;
        }
    }
}
