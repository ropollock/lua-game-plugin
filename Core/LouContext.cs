using System.Collections.Concurrent;
using Gameplay.Plugin;

namespace Lou {
    public struct LouContext {
        public ConcurrentQueue<LuaResponse> ResponseQueue { get; set; }
        public string ClusterID { get; set; }
        public string RegionAddress { get; set; }
        public string PluginName { get; set; }
    }
}