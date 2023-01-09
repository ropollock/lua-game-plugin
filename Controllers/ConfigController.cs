using System;
using System.Collections.Generic;
using Lou.Services;
using NLua;
using Zenject;

namespace Lou.Controllers {
    public class ConfigController : AbstractController {

        [Inject]
        private IConfigService _configService;

        private object[] SetConfig(LouDirectRequest request) {
            var requestParams = request.RequestParams;
            string type = (string)requestParams[0];
            var config = (LuaTable)requestParams[1];
            _configService.SetConfig(type, config.ToDictionary());
            return new object[0];
        }
        
        public override Routes GetRoutes() {
            var directRequestRoutes = new Dictionary<string, Func<LouDirectRequest, object[]>> {
                {"Config.Set", SetConfig}
            };

            return new Routes {
                DirectRequestRoutes = directRequestRoutes
            };
        }
    }
}