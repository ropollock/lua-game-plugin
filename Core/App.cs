using System;
using System.Collections.Generic;
using Hocon;
using log4net;
using Lou.Controllers;
using Quartz;
using Zenject;

namespace Lou {
    public sealed class App {
        
        private static readonly ILog _logger = LogManager.GetLogger(typeof(App));
        
        [Inject]
        private LouContext _context;

        [Inject]
        private IRouter _router;

        [Inject]
        private List<IController> _controllers;
        
        [Inject]
        private IScheduler _scheduler;

        [Inject]
        private Config _config;

        public bool Initialize() {
            _logger.Info($"Effective config: {_config.PrettyPrint(2)}");
            _logger.Info("Initializing LouPlugin...");
            try {
                _logger.Info("Initializing controllers...");
                _controllers.ForEach(controller => controller.Initialize());
                _logger.Info("Initializing router...");
                _router.Initialize();
            }
            catch (Exception e) {
                _logger.Error($"Exception occurred trying to initialize the plugin.\r\n {e.Message}");
                return false;
            }

            return true;
        }

        public void Request(LouRequest request) {
            try {
                bool verbose = _config.GetBoolean("logging.verboseRouter");
                if (verbose) {
                    _logger.Debug($"Routing request {request.EventId}");                    
                }
                
                var handler = _router.RouteRequest(request);
                if (handler != null) {
                    if (verbose) {
                        _logger.Debug($"Invoking handler for {request.EventId}");                        
                    }
                    
                    handler.Invoke(request);
                }
                else {
                    _logger.Error($"Unable to route {request.EventId}");
                }
            }
            catch (Exception e) {
                _logger.Error($"Unable to handle request: {request.EventId}\r\n {e.Message}");
            }
        }

        public object[] DirectRequest(object[] requestData) {
            var directRequest = new LouDirectRequest {Data = requestData};
            var response = new object[0];
            bool verbose = _config.GetBoolean("logging.verboseRouter");
            try {
                if (verbose) {
                    _logger.Debug($"Routing direct request {directRequest.EventId}");                    
                }
                
                var handler = _router.RouteDirectRequest(directRequest);
                if (handler != null) {
                    if (verbose) {
                        _logger.Debug($"Invoking handler for {directRequest.EventId}");                        
                    }
                    response = handler.Invoke(directRequest);                    
                }
                else {
                    _logger.Info($"Unable to route {directRequest.EventId}");
                }
            }
            catch (Exception e) {
                _logger.Error($"Unable to handle direct request: {directRequest.EventId}\r\n {e.Message}");
            }

            return response;
        }

        public void Start() {
            try {
                _logger.Info("Starting controllers...");
                _controllers.ForEach(controller => controller.Start());
                _logger.Info("Starting scheduler...");
                _scheduler.Start();
            }
            catch (Exception e) {
                _logger.Error($"Exception occurred trying to start all controllers.\r\n {e.Message}");
            }
        }

        public void Shutdown() {
            try {
                _logger.Info("Shutting down controllers...");
                _controllers.ForEach(controller => controller.Shutdown());
                _scheduler.Shutdown();
            }
            catch (Exception e) {
                _logger.Error($"Exception occurred trying to shutdown all controllers.\r\n {e.Message}");
            }
        }
    }
}