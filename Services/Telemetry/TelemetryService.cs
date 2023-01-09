using System;
using System.Collections.Concurrent;
using Hocon;
using log4net;
using NLua;
using Quartz;
using Zenject;

namespace Lou.Services {
    public class TelemetryService : ITelemetryService {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(TelemetryService));

        [Inject]
        private IScheduler _scheduler;

        [Inject]
        private Config _config;
        public bool Enabled { get; set; }

        public bool Verbose { get; set; }
        
        public int MaxEvents { get; set; }
        
        public long MaxWait { get; set; }
        
        public ConcurrentQueue<TelemetryEvent> TelemetryQueue { get; } = new ConcurrentQueue<TelemetryEvent>();
        public DateTime? EnqueuedTime { get; set; }

        private IJobDetail _reporterJob;

        private ITrigger _reporterTrigger;

        [Inject]
        public void Initialize() {
            Enabled = _config.GetBoolean("telemetry.enabled");
            Verbose = _config.GetBoolean("telemetry.verbose");
            MaxEvents = _config.GetInt("telemetry.maxEvents", 5);
            MaxWait = _config.GetLong("telemetry.maxWait", 5000);
            _reporterJob = JobBuilder.Create<TelemetryReporterJob>()
                .WithIdentity("reporterJob", "telemetry")
                .Build();

            _reporterTrigger = TriggerBuilder.Create()
                .WithIdentity("reporterTrigger", "telemetry")
                .StartNow()
                .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMilliseconds(250)).RepeatForever())
                .Build();
        }

        public void SendEvent(LuaTable eventTable) {
            if (!Enabled) return;
            if (eventTable == null) {
                _logger.Warn("Failed to Enqueue null telemetry event.");
                return;
            }

            if (EnqueuedTime == null) {
                EnqueuedTime = DateTime.Now;
                VerboseDebug($"Setting EnqueuedTime to {EnqueuedTime}");
            }

            VerboseDebug($"Enqueuing telemetry event: {eventTable["type"]}");
            TelemetryEvent e = TelemetryEventUtil.ConvertGameEvent(eventTable);
            TelemetryQueue.Enqueue(e);
            eventTable.Dispose();
        }

        public void StartTelemetryReporter() {
            if (!Enabled) {
                _logger.Warn("Unable to start telemetry reporter because telemetry is not enabled.");
                return;
            }

            if (!_scheduler.CheckExists(_reporterJob.Key).Result) {
                _logger.Info("Scheduling telemetry reporter job.");
                _scheduler.ScheduleJob(_reporterJob, _reporterTrigger);
            }
            else {
                _logger.Info("Resuming telemetry reporter job.");
                _scheduler.ResumeJob(_reporterJob.Key);
            }
        }

        public void StopTelemetryReporter() {
            if (_scheduler.CheckExists(_reporterJob.Key).Result) {
                _logger.Info("Stopping telemetry reporter job.");
                _scheduler.PauseJob(_reporterJob.Key);
            }
            else {
                _logger.Warn("Telemetry reporter job cannot be stopped because it does not exist.");
            }
        }

        private void VerboseDebug(string msg) {
            if (Verbose) {
                _logger.Debug(msg);
            }
        }
    }
}