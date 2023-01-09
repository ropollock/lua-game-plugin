using System;
using System.Collections.Concurrent;
using NLua;

namespace Lou.Services {
    public interface ITelemetryService {
        void SendEvent(LuaTable eventTable);
        void StartTelemetryReporter();
        void StopTelemetryReporter();
        bool Enabled { get; set; }
        bool Verbose { get; set; }
        int MaxEvents { get; set; }
        long MaxWait { get; set; }
        ConcurrentQueue<TelemetryEvent> TelemetryQueue { get; } 
        DateTime? EnqueuedTime { get; set; }
    }
}