using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Lou.Services;
using NLua;
using Zenject;

namespace Lou.Controllers {
    public sealed class HttpController : AbstractController {
        [Inject]
        private IHttpService _httpService;

        [Inject]
        private LouContext _context;

        private const string GET = "Http.GET";
        private const string POST = "Http.POST";

        private async void GetRequest(LouRequest request) {
            // Prepare our response.
            var data = new object[2];
            // Data[0] should be the response body.
            // Data[1] should be an error message on a failure, or null on a success.
            try {
                var requestParams = request.RequestParams;
                string address = (string) requestParams[0];

                var httpResponse = await _httpService.Get(address);
                data[0] = await httpResponse.Content.ReadAsStringAsync();
                if (!httpResponse.IsSuccessStatusCode) {
                    data[1] = httpResponse.StatusCode.ToString();
                }
                httpResponse.Dispose();
            }
            catch (Exception e) {
                data[1] = ControllerUtil.GetInnerException(e);
            }

            var response = ControllerUtil.CreateLuaResponse(request, data);
            _context.ResponseQueue.Enqueue(response);
        }

        private object[] DirectGetRequest(LouDirectRequest directRequest) {
            var data = new object[2];
            // Data[0] should be the response body.
            // Data[1] should be an error message on a failure, or null on a success.
            try
            {
                var requestParams = directRequest.RequestParams;
                string address = (string) requestParams[0];

                var httpResponse = _httpService.SyncGet(address);
                data[0] = httpResponse.Content.ReadAsStringAsync();
                if (!httpResponse.IsSuccessStatusCode)
                {
                    data[1] = httpResponse.StatusCode.ToString();
                }
            }
            catch (WebException e)
            {
                data[1] = ControllerUtil.GetInnerException(e);
            }
            catch (HttpRequestException e)
            {
                data[1] = ControllerUtil.GetInnerException(e);
            }
            catch (Exception e)
            {
                data[1] = ControllerUtil.GetInnerException(e);
            }

            return data;
        }

        private async void PostRequest(LouRequest request) {
            // Prepare our response.
            var data = new object[2];
            // Data[0] should be the response body.
            // Data[1] should be an error message on a failure, or null on a success.
            try
            {
                var requestParams = request.RequestParams;
                string address = (string) requestParams[0];

                // We need to parse the POST data from Lua.
                LuaTable postData = (LuaTable) requestParams[1];
                var contents = postData.ToDictionary();
                // We're ready to send the request.
                var httpResponse = await _httpService.Post(address, contents);
                data[0] = await httpResponse.Content.ReadAsStringAsync();
                if (!httpResponse.IsSuccessStatusCode)
                {
                    data[1] = httpResponse.StatusCode.ToString();
                }

                postData.Dispose();
            }
            catch (WebException e)
            {
                data[1] = ControllerUtil.GetInnerException(e);
            }
            catch (HttpRequestException e)
            {
                data[1] = ControllerUtil.GetInnerException(e);
            }
            catch (Exception e)
            {
                data[1] = ControllerUtil.GetInnerException(e);
            }

            var response = ControllerUtil.CreateLuaResponse(request, data);
            _context.ResponseQueue.Enqueue(response);
        }

        private object[] DirectPostRequest(LouDirectRequest directRequest) {
            // Prepare our response.
            var data = new object[2];
            // Data[0] should be the response body.
            // Data[1] should be an error message on a failure, or null on a success.
            try {
                var requestParams = directRequest.RequestParams;
                string address = (string) requestParams[0];

                // We need to parse the POST data from Lua.
                LuaTable postData = (LuaTable) requestParams[1];
                var contents = postData.ToDictionary();
                // We're ready to send the request.
                var httpResponse = _httpService.SyncPost(address, contents);
                data[0] = httpResponse.Content.ReadAsStringAsync();
                if (!httpResponse.IsSuccessStatusCode) {
                    data[1] = httpResponse.StatusCode.ToString();
                }
            }
            catch (WebException e) {
                data[1] = ControllerUtil.GetInnerException(e);
            }
            catch (HttpRequestException e) {
                data[1] = ControllerUtil.GetInnerException(e);
            }
            catch (Exception e)
            {
                data[1] = ControllerUtil.GetInnerException(e);
            }

            return data;
        }

        public override Routes GetRoutes() {
            var requestRoutes = new Dictionary<string, Action<LouRequest>> {
                {GET, GetRequest},
                {POST, PostRequest}
            };

            var directRequestRoutes = new Dictionary<string, Func<LouDirectRequest, object[]>> {
                {GET, DirectGetRequest},
                {POST, DirectPostRequest}
            };

            return new Routes {
                RequestRoutes = requestRoutes,
                DirectRequestRoutes = directRequestRoutes
            };
        }
    }
}