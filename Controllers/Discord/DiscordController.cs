using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Gameplay.PublicLua;
using log4net;
using Lou.Services;
using Zenject;

namespace Lou.Controllers {
    public class DiscordController : AbstractController {

        private static readonly ILog _logger = LogManager.GetLogger(typeof(DiscordController));
        
        [Inject]
        private IDiscordService _discordService;

        [Inject]
        private LouContext _context;

        [Inject]
        private DiscordRoles _discordRoles;

        private void Connect(LouRequest request) {
            _logger.Info("Handling request to connect to discord.");
            string token = (string) request.RequestParams[0];

            _discordService.Connect(token, () => {
                _logger.Info($"Creating LuaResponse for discord client connection ready.");
                var data = new object[1];
                data[0] = "success";
                var response = ControllerUtil.CreateLuaResponse(request, data);
                _logger.Info($"Enqueuing response.");
                _logger.Debug($"LuaResponse : {response.LuaResponseToString()}");
                _context.ResponseQueue.Enqueue(response);
                return Task.CompletedTask;
            });
        }

        private void Listen(LouRequest request) {
            _logger.Info("Handling request to listen to discord channel.");
            ulong channel = ulong.Parse((string)request.RequestParams[0]);
            _logger.Debug($"Listening on channel id: {channel}");
            _discordService.Listen(channel, (msg) => {
                _logger.Info($"Creating LuaResponse for discord message received.");
                var data = new object[4];
                _logger.Debug($"Attempting to get guild user for {msg.Author.Username}, {msg.Author.Discriminator}, {msg.Author.Id}");

                SocketGuildUser guildUser = null;
                try {
                    guildUser = (SocketGuildUser) msg.Author;
                }
                catch (Exception e) {
                    _logger.Warn($"Unable to cast {msg.Author.Username} as a guild user.");
                }

                if (guildUser != null && guildUser.Nickname != null) {
                    _logger.Debug($"Found nickname for {msg.Author.Username}, {msg.Author.Discriminator}, {msg.Author.Id}");
                    data[0] = guildUser.Nickname;
                }
                else {
                    _logger.Debug($"No nickname was found for {msg.Author.Username}, {msg.Author.Discriminator}, {msg.Author.Id}");
                    data[0] = msg.Author.Username;
                }
                
                data[1] = msg.Content;
                data[2] = $"{msg.Timestamp.Date:u}";
                
                if (guildUser != null) {
                    try
                    {
                        LuaTableProxy rolesTable = new LuaTableProxy();
                        List<ulong> roleIds = guildUser.Roles.Select(r => r.Id).ToList();

                        List<string> relevantRoles = _discordRoles.FindRoles(roleIds);
                        for (int i = 0; i < relevantRoles.Count; i++) {
                            rolesTable.TableData.Add(LuaTableItem.Create(i + 1), LuaTableItem.Create(relevantRoles[i]));
                        }
                        data[3] = rolesTable;
                    }
                    catch (Exception e)
                    {
                        _logger.Warn($"Unable to find guild user roles.");
                    }
                } 
                
                // Create response
                var response = ControllerUtil.CreateLuaResponse(request, data);
                _logger.Info($"Enqueuing response.");
                _logger.Debug($"LuaResponse : {response.LuaResponseToString()}");
                _context.ResponseQueue.Enqueue(response);
                return Task.CompletedTask;
            });
        }

        public override void Shutdown() {
            _discordService.Disconnect();
        }

        public override Routes GetRoutes() {
            var requestRoutes = new Dictionary<string, Action<LouRequest>> {
                {"Discord.Connect", Connect},
                {"Discord.Listen", Listen}
            };

            return new Routes {
                RequestRoutes = requestRoutes
            };
        }
    }
}