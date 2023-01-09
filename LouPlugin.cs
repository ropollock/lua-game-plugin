using System;
using System.Collections.Concurrent;
using System.Reflection;
using Gameplay.Plugin;
using log4net;
using log4net.Config;

[assembly:AssemblyVersion("0.7.1")]

namespace Lou {
    public class LouPlugin : AbstractPlugin {
        
        private static readonly ILog _logger = LogManager.GetLogger(typeof(LouPlugin));
        
        public static LouContext Context;
        private App _app;

        public override void Init(ConcurrentQueue<LuaResponse> _responseQueue, string _clusterId,
            string _regionAddress) {
            log4net.GlobalContext.Properties["region"] = _regionAddress;
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"Base directory: {baseDir}");
            var log4netConfFileInfo = new System.IO.FileInfo($"{baseDir}..\\mods\\modname\\plugins\\log4net.conf");
            Console.WriteLine($"Plugin directory: {log4netConfFileInfo.Directory.FullName}");
            XmlConfigurator.Configure(new System.IO.FileInfo($"{log4netConfFileInfo.FullName}"));
            Context = new LouContext {
                ClusterID = _clusterId,
                RegionAddress = _regionAddress,
                ResponseQueue = _responseQueue,
                PluginName = GetPluginName()
            };

            DIManager.Initialize();
            _app = DIManager.Container.TryResolve<App>();
            if (_app == null) {
                isReadyForStartup = false;
                return;
            }

            isReadyForStartup = _app.Initialize();
        }

        public override void MessageFromLua(LuaRequest request) {
            _app.Request(request.ToLouRequest());
        }

        public override object[] DirectRequestFromLua(object[] _requestData) {
            return _app.DirectRequest(_requestData);
        }

        public override void Start() {
            _app.Start();
        }

        public override void Shutdown() {
            _app.Shutdown();
            base.Shutdown();
        }

        public override string GetPluginName() {
            return "LouPlugin";
        }
    }
}