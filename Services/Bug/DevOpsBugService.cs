using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hocon;
using log4net;
using Lou.Util;
using Newtonsoft.Json;
using Zenject;

namespace Lou.Services {
    public class DevOpsBugService : IBugService {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(DevOpsBugService));

        [Inject]
        private Config _config;

        [Inject]
        private IHttpService _httpService;

        public bool Enabled { get; set; }

        private string _accessToken;
        private string _projectId;
        private string _apiUrl;

        [Inject]
        public void Initialize() {
            Enabled = _config.GetBoolean("devops.enabled");
            _accessToken = _config.GetString("devops.accessToken");
            _projectId = _config.GetString("devops.project");
            _apiUrl = _config.GetString("devops.baseApiUrl");

            if (string.IsNullOrEmpty(_accessToken)
                || string.IsNullOrEmpty(_projectId)
                || string.IsNullOrEmpty(_apiUrl)) {
                _logger.Warn("Missing or invalid configuration for gitlab bug reporting. Service is disabled.");
                Enabled = false;
            }
        }

        public async Task<IssueResponse> Report(BugReport bugReport) {
            if (!Enabled) {
                _logger.Warn("Bug reporting is not enabled.");
                return new IssueResponse {success = false};
            }

            if (!bugReport.IsValid()) {
                _logger.Warn($"Invalid bug report requested. {bugReport}");
                return new IssueResponse {success = false};
            }

            List<DevOpsWorkItemRequest> req = DevOpsBugReportRequest.FromBugReport(bugReport);

            string requestUrl = $"{_apiUrl}{_projectId}/_apis/wit/workitems/$bug?api-version=6.0";
            try {
                var headers = new Dictionary<string, string> {{"Authorization", $":{_accessToken}"}};
                var httpResponse = await _httpService.Post(requestUrl, 
                    JsonConvert.SerializeObject(req),
                    "application/json-patch+json",
                    headers
                    );
                if (!httpResponse.IsSuccessStatusCode) {
                    _logger.Error($"Failed to submit devops bug report with response {await httpResponse.Content.ReadAsStringAsync()}");
                    return new IssueResponse {success = false};
                }

                var responseStr = await httpResponse.Content.ReadAsStringAsync();
                _logger.Debug(responseStr);
                var response = JsonConvert.DeserializeObject<DevOpsWorkItemResponse>(await httpResponse.Content.ReadAsStringAsync());
                _logger.Info($"Successfully submitted devops bug report with title: {bugReport.Title}");
                return new IssueResponse {success = true, id = response.id.ToString(), webUrl = response.url};
            }
            catch (Exception e) {
                _logger.Error("Failed to submit devops bug report.", e);
                return new IssueResponse {success = false};
            }
        }
    }
}