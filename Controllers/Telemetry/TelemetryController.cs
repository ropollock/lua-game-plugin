using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Lou.Services;
using NLua;
using Zenject;

namespace Lou.Controllers.Telemetry {
    public class TelemetryController : AbstractController {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(TelemetryController));

        [Inject]
        private ITelemetryService _telemetryService;

        public override void Start() {
            _telemetryService.StartTelemetryReporter();
        }

        public override void Shutdown() {
            _telemetryService.StopTelemetryReporter();
        }

        private object[] RequestSendEvent(LouDirectRequest request) {
            try {
                _telemetryService.SendEvent(request.RequestParams.First() as LuaTable);
            }
            catch (Exception e) {
                _logger.Error("Unable to send telemetry event.", e);
            }

            return new object[] {true};
        }

        public override Routes GetRoutes() {
            var directRequestRoutes = new Dictionary<string, Func<LouDirectRequest, object[]>> {
                {"Telemetry.Event", RequestSendEvent}
            };

            return new Routes {
                DirectRequestRoutes = directRequestRoutes
            };
        }
    }
}