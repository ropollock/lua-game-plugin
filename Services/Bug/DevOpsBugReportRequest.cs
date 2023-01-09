using System.Collections.Generic;

namespace Lou.Services {
    public class DevOpsBugReportRequest {
        public static List<DevOpsWorkItemRequest> FromBugReport(BugReport bugReport) {
            var areaPathValue = "lou-server\\player-bug-reports";
            if (!bugReport.Confidential) {
                areaPathValue = "lou-server\\player-feedback";
            }

            return new List<DevOpsWorkItemRequest>() {
                new DevOpsWorkItemRequest() {op = "add", path = "/fields/System.Title", value = bugReport.Title},
                new DevOpsWorkItemRequest()
                    {op = "add", path = "/fields/System.Description", value = bugReport.Description},
                new DevOpsWorkItemRequest()
                    {op = "add", path = "/fields/System.AreaPath", value = areaPathValue}
            };
        }
    }

    public class DevOpsWorkItemRequest {
        public string op { get; set; }
        public string path { get; set; }
        public string from { get; set; }
        public string value { get; set; }
    }
}