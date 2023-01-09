using Gameplay.PublicLua;
using Newtonsoft.Json;

namespace Lou.Services {
    public class DevOpsWorkItemResponse {

        public int id;

        public string url;
    }
    
    public class DevOpsWorkItem {
        [JsonProperty("System.Title")]
        public string Title { get; set; }

        [JsonProperty("System.Description")]
        public string Description { get; set; }

        [JsonProperty("System.WorkItemType")]
        public string Type { get; set; }
    }
}