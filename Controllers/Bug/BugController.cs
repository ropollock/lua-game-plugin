using System;
using System.Collections.Generic;
using log4net;
using Lou.Services;
using NLua;
using Zenject;

namespace Lou.Controllers.Bug {
    public class BugController : AbstractController {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(BugController));

        [Inject]
        private LouContext _context;

        [Inject]
        private IBugService _bugService;

        private async void SubmitBugReport(LouRequest request) {
            BugReport bug = new BugReport();
            try {
                var requestTable = request.RequestParams[0] as LuaTable;
                bug.Title = requestTable["title"] as string;
                bug.Description = requestTable["description"] as string;
                bug.Labels = requestTable["labels"] as string;
                if (requestTable["confidential"] != null) {
                    bug.Confidential = requestTable["confidential"] is bool ? (bool) requestTable["confidential"] : true;
                }
                else {
                    bug.Confidential = true;
                }
            }
            catch (Exception e) {
                _logger.Error("Unable to parse bug request.");
            }

            IssueResponse res = new IssueResponse();
            if (!bug.IsValid()) {
                _logger.Error($"Bug report request was invalid: {bug}");
                res.success = false;
            }
            else {
                res = await _bugService.Report(bug);
            }
            
            var response = ControllerUtil.CreateLuaResponse(request, new object[] {IssueResponse.ToLua(res)});
            _context.ResponseQueue.Enqueue(response);
        }

        public override Routes GetRoutes() {
            var requestRoutes = new Dictionary<string, Action<LouRequest>> {
                {"Bug.Report", SubmitBugReport}
            };

            return new Routes {
                RequestRoutes = requestRoutes
            };
        }
    }
}