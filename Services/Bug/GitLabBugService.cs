using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hocon;
using log4net;
using Newtonsoft.Json;
using Zenject;

namespace Lou.Services {
    public class GitLabBugService : IBugService {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(GitLabBugService));

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
            Enabled = _config.GetBoolean("gitlab.enabled");
            _accessToken = _config.GetString("gitlab.accessToken");
            _projectId = _config.GetString("gitlab.bugReportProjectId");
            _apiUrl = _config.GetString("gitlab.apiUrl");

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

            BugReportRequest req = BugReportRequest.FromBugReport(bugReport);
            req.Id = _projectId;
            string requestUrl = $"{_apiUrl}{_projectId}/issues";
            try {
                var httpResponse = await _httpService.Post(requestUrl, 
                    JsonConvert.SerializeObject(req),
                    "application/json",
                    new Dictionary<string, string> {{"PRIVATE-TOKEN", _accessToken}});
                if (!httpResponse.IsSuccessStatusCode) {
                    _logger.Error($"Failed to submit gitlab bug report with response {await httpResponse.Content.ReadAsStringAsync()}");
                    return new IssueResponse {success = false};
                }
                
                _logger.Info($"Successfully submitted gitlab bug report with title: {req.Title}");
                var issue = JsonConvert.DeserializeObject<IssueResponse>(await httpResponse.Content.ReadAsStringAsync());
                issue.success = true;
                return issue;
            }
            catch (Exception e) {
                _logger.Error("Failed to submit gitlab bug report.", e);
                return new IssueResponse {success = false};
            }
        }
    }
}