using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Lou.Services {
    public interface IDiscordService {
        /**
         * Connects a bot using a token.
         * Provides a callback for ready event.
         */
        void Connect(string token, Func<Task> readyCallback);

        /**
         * Disconnects the current client.
         */
        void Disconnect();

        /**
         * Listens to a specific channel that the currently connected bot has access to.
         * Pushes message events back to a callback.
         * Disregards message events from webhooks and bots.
         */
        void Listen(ulong channelId, Func<SocketMessage, Task> callback);
    }
}