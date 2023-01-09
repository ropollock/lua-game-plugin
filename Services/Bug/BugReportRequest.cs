using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace Lou.Services {
    public class BugReportRequest {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("labels")]
        public string Labels { get; set; }

        [JsonProperty("confidential")]
        public bool Confidential { get; set; }

        public static BugReportRequest FromBugReport(BugReport bugReport) {
            return new BugReportRequest {
                Title = bugReport.Title,
                Description = bugReport.Description,
                Labels = bugReport.Labels,
                Confidential = bugReport.Confidential
            };
        }
    }
}