using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Core;
using Zenject;

namespace Lou.Controllers.Logging
{
    public class LogController : AbstractController
    {

        private static readonly ILog _logger = LogManager.GetLogger(typeof(LogController));

        [Inject]
        private LevelMap _levelMap;

        private object[] RequestLogLevel(LouDirectRequest request)
        {
            var requestParams = request.RequestParams;
            string level = (string)requestParams[0];
            SetLogLevel(_levelMap[level]);
            return new object[] {};
        }
        private void SetLogLevel(Level logLevel) {
            _logger.Info($"Setting log level to {logLevel}.");
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Threshold = logLevel;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
        }
        
        private object[] RequestLogInfo(LouDirectRequest request)
        {
            var requestParams = request.RequestParams;
            string msg = (string)requestParams[0];
            _logger.Info(msg);
            return new object[] {};
        }
        
        private object[] RequestLogDebug(LouDirectRequest request)
        {
            var requestParams = request.RequestParams;
            string msg = (string)requestParams[0];
            _logger.Debug(msg);
            return new object[] {};
        }
        
        private object[] RequestLogWarn(LouDirectRequest request)
        {
            var requestParams = request.RequestParams;
            string msg = (string)requestParams[0];
            _logger.Warn(msg);
            return new object[] {};
        }
        
        private object[] RequestLogError(LouDirectRequest request)
        {
            var requestParams = request.RequestParams;
            string msg = (string)requestParams[0];
            _logger.Error(msg);
            return new object[] {};
        }
        
        public override Routes GetRoutes() {
            
            var directRequestRoutes = new Dictionary<string, Func<LouDirectRequest, object[]>> {
                {"Log.SetLevel", RequestLogLevel},
                {"Log.Debug", RequestLogDebug},
                {"Log.Info", RequestLogInfo},
                {"Log.Warn", RequestLogWarn},
                {"Log.Error", RequestLogError}
            };

            return new Routes {
                DirectRequestRoutes = directRequestRoutes
            };
        }
    }
}