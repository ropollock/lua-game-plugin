using System;
using System.Collections.Generic;
using System.Linq;
using Hocon;
using log4net;
using Lou.Controllers;
using Zenject;

namespace Lou {
    public class Router : IRouter {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Router));

        [Inject]
        private List<IController> _controllers;

        [Inject]
        private Config _config;

        private Routes _routes;

        public void Initialize() {
            _routes = new Routes() {
                RequestRoutes = new Dictionary<string, Action<LouRequest>>(),
                DirectRequestRoutes = new Dictionary<string, Func<LouDirectRequest, object[]>>()
            };
            _controllers.ForEach(RegisterController);
            _logger.Info(LogRoutes());
        }

        private string LogRoutes() {
            return
                $"\r\n\tRequest Routes: \r\n\t\t{string.Join("\r\n\t\t", _routes.RequestRoutes.Keys)}\r\n\tDirect Request Routes: \r\n\t\t{string.Join("\r\n\t\t", _routes.DirectRequestRoutes.Keys)}";
        }

        public void RegisterController(IController controller) {
            var controllerRoutes = controller.GetRoutes();

            if (controllerRoutes.RequestRoutes != null) {
                _routes.RequestRoutes = _routes.RequestRoutes.Union(controllerRoutes.RequestRoutes)
                    .ToDictionary(k => k.Key, v => v.Value);
            }

            if (controllerRoutes.DirectRequestRoutes != null) {
                _routes.DirectRequestRoutes = _routes.DirectRequestRoutes.Union(controllerRoutes.DirectRequestRoutes)
                    .ToDictionary(k => k.Key, v => v.Value);
            }
        }

        public Action<LouRequest> RouteRequest(LouRequest request) {
            bool verbose = _config.GetBoolean("logging.verboseRouter");
            if (verbose) {
                _logger.Debug($"Looking for route to handle {request.EventId}");                
            }
            
            if (_routes.RequestRoutes.TryGetValue(request.EventId, out var handler)) {
                if (verbose) {
                    _logger.Debug($"Found handler for {request.EventId}");
                }

                return handler;
            }

            _logger.Error($"Unable to locate handler for event ID: {request.EventId}");
            return null;
        }

        public Func<LouDirectRequest, object[]> RouteDirectRequest(LouDirectRequest directRequest) {
            if (_routes.DirectRequestRoutes.TryGetValue(directRequest.EventId, out var handler)) {
                return handler;
            }

            _logger.Error($"Unable to locate handler for direct event ID: {directRequest.EventId}");
            return null;
        }
    }
}