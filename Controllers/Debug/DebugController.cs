using System;
using System.Collections.Generic;
using System.Diagnostics;
using log4net;

namespace Lou.Controllers.Debug {
    
    public sealed class DebugController : AbstractController {
        
        private static readonly ILog _logger = LogManager.GetLogger(typeof(DebugController));

        public override void Start() {
        }

        public override void Shutdown() {
        }

        private object[] ForceCollectGarbage(LouDirectRequest request) {
            _logger.Warn("Forcing garbage collection!");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            _logger.Warn("Garbage collection completed in " + elapsedTime);
            
            return new object[] {true};
        }
        
        private object[] GetTotalMemory(LouDirectRequest request) {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            long totalMemory = currentProcess.WorkingSet64;
            totalMemory /= (1024 * 1024);
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            _logger.Info("Got total memory in " + elapsedTime + " Total (MB): " + totalMemory);
            
            return new object[] {totalMemory};
        }
        
        public override Routes GetRoutes() {
            var requestRoutes = new Dictionary<string, Action<LouRequest>>();

            var directRequestRoutes = new Dictionary<string, Func<LouDirectRequest, object[]>> {
                {"Debug.ForceGarbageCollect", ForceCollectGarbage},
                {"Debug.GetTotalMemory", GetTotalMemory}
            };

            return new Routes {
                RequestRoutes = requestRoutes,
                DirectRequestRoutes = directRequestRoutes
            };
        }
    }
}