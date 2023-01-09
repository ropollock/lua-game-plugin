using System;
using System.Collections.Generic;

namespace Lou {
    public class Routes {
        public Dictionary<string, Func<LouDirectRequest, object[]>> DirectRequestRoutes { get; set; }
        public Dictionary<string, Action<LouRequest>> RequestRoutes { get; set; }
        
        public Routes() {
            RequestRoutes = new Dictionary<string, Action<LouRequest>>();
            DirectRequestRoutes = new Dictionary<string, Func<LouDirectRequest, object[]>>();
        }
    }
}