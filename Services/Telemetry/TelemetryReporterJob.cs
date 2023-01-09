using System;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using Quartz;
using Zenject;

namespace Lou.Services {
    [DisallowConcurrentExecution]
    public class TelemetryReporterJob : IJob {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(TelemetryReporterJob));
        private static readonly ILog _reporter = LogManager.GetLogger("telemetry");

        [Inject]
        private ITelemetryService _telemetryService;

        [Inject]
        private LouContext _context;

        public Task Execute(IJobExecutionContext context) {
            if (ShouldWait()) {
                return Task.CompletedTask;
            }

            while (_telemetryService.TelemetryQueue.TryDequeue(out TelemetryEvent telemetryEvent)) {
                try {
                    if (telemetryEvent.locations != null) {
                        telemetryEvent.locations.ForEach(l => {
                            if (l.region == null) {
                                l.region = _context.RegionAddress;
                            }
                        });
                    }

                    _reporter.Info(JsonConvert.SerializeObject(telemetryEvent));
                    telemetryEvent = null;
                }
                catch (Exception e) {
                    _logger.Error("Unable to report telemetry event.", e);
                }
            }

            // Clear the enqueued time and complete the task.
            _telemetryService.EnqueuedTime = null;
            return Task.CompletedTask;
        }

        private bool ShouldWait() {
            var eventCount = _telemetryService.TelemetryQueue.Count;
            // If somehow there is no enqueued time set but we have events in the queue, process them now.
            if (_telemetryService.EnqueuedTime == null
                && eventCount > 0) {
                return false;
            }

            // If the time has not been set, keep waiting for events.
            if (_telemetryService.EnqueuedTime == null) {
                return true;
            }

            // If the event count or the max wait time conditions have not been met, keep waiting.
            return eventCount < _telemetryService.MaxEvents
                   && TimeSince() < _telemetryService.MaxWait;
        }

        private double TimeSince() {
            return DateTime.Now.Subtract(_telemetryService.EnqueuedTime.Value).TotalMilliseconds;
        }
    }
}