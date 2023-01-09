using System;
using System.Collections.Generic;
using log4net;
using Lou.Services;
using NLua;
using Zenject;

namespace Lou.Controllers {
    public class PageController : AbstractController {

        private static readonly ILog _logger = LogManager.GetLogger(typeof(PageController));
        
        [Inject]
        private IHttpService _httpService;
        
        [Inject]
        private LouContext _context;

        private async void SendPage(LouRequest request) {
            _logger.Info($"Sending page for request {request.ResponseTarget}");
            // Prepare response.
            var data = new object[2];
            // Data[0] should be the response body.
            // Data[1] should be an error message on a failure, or null on a success.
            try {
                _logger.Debug($"request data {string.Join(", ", request.Data)}");
                var requestParams = request.RequestParams;
                string webhookUri = (string)requestParams[0];
                
                // Parse LuaTable into request
                _logger.Info($"Parsing gm page request.");
                LuaTable postData = (LuaTable) requestParams[1];
                var gmPageRequest = GMPageRequest.LoadGMPageRequest(postData);
                _logger.Debug($"GM page request.\r\n{gmPageRequest}");
                // Perform post
                
                var cardJSON = TeamsMessageCard.BuildJSON(gmPageRequest);
                _logger.Debug($"Card JSON \r\n{cardJSON}");
                _logger.Info($"Posting gm page request to webhook.");
                var httpResponse = await _httpService.Post(webhookUri, cardJSON);
                _logger.Debug($"Got response : {httpResponse}");
                _logger.Info("Reading http response from webhook.");
                data[0] = await httpResponse.Content.ReadAsStringAsync();
                if (!httpResponse.IsSuccessStatusCode) {
                    data[1] = httpResponse.StatusCode.ToString();
                }
                httpResponse.Dispose();
            }
            catch (Exception e) {
                data[1] = ControllerUtil.GetInnerException(e);
            }

            _logger.Info($"Creating LuaResponse from gm page request response.");
            var response = ControllerUtil.CreateLuaResponse(request, data);
            _logger.Info($"Enqueuing response from gm page request.");
            _logger.Debug($"LuaResponse : {response.LuaResponseToString()}");
            _context.ResponseQueue.Enqueue(response);
        }
        
        public override Routes GetRoutes() {
            var requestRoutes = new Dictionary<string, Action<LouRequest>> {
                {"Page.Send", SendPage}
            };

            return new Routes {
                RequestRoutes = requestRoutes
            };
        }
    }
}