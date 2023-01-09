using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using log4net;
using Zenject;

namespace Lou.Services {
    public class DiscordService : IDiscordService, IInitializable, IDisposable {
        
        private static readonly ILog _logger = LogManager.GetLogger(typeof(DiscordService));

        private DiscordSocketClient _discord;

        [Inject]
        public void Initialize() {
            _discord = new DiscordSocketClient();
            _discord.Log += Log;
        }

        private Task Log(LogMessage message) {
            _logger.Debug($"[DiscordService] {message.Message}");
            return Task.CompletedTask;
        }

        public void Connect(string token, Func<Task> readyCallback) {
            _discord.LoginAsync(TokenType.Bot, token);
            _discord.StartAsync();
            _discord.Ready += readyCallback;
        }

        public void Listen(ulong channelId, Func<SocketMessage, Task> callback) {
            _discord.MessageReceived += msg => {
                _logger.Debug($"[DiscordService] Discord message received on channel {msg.Channel.Name} ({msg.Channel.Id}), listening on channel ({channelId}).");
                if (msg.Channel.Id == channelId
                    && (msg.Source != MessageSource.Bot &&
                    msg.Source != MessageSource.Webhook)) {
                    _logger.Info($"[DiscordService] Listening to discord message from {msg.Channel.Name}.");                    
                    return callback.Invoke(msg);
                }

                return Task.CompletedTask;
            };
        }

        public void Disconnect() {
            _discord.StopAsync();
        }

        public void Dispose() {
            _discord.Dispose();
        }
    }
}