using System;
using System.Collections.Generic;
using Hocon;
using log4net.Core;
using Lou.Controllers;
using Lou.Controllers.Bug;
using Lou.Controllers.Debug;
using Lou.Controllers.Logging;
using Lou.Controllers.Physics;
using Lou.Controllers.Telemetry;
using Lou.Services;
using Quartz;
using Quartz.Impl;
using Zenject;

namespace Lou {
    public class LouPluginInstaller : Installer {
        public override void InstallBindings() {
            Container.Bind<LouContext>().FromInstance(LouPlugin.Context);
            var config = ConfigurationFactory.FromFile($"{AppDomain.CurrentDomain.BaseDirectory}..\\mods\\Ultima\\plugins\\reference.conf");
            Container.Bind<Config>().FromInstance(config);
            Container.Bind<AppDomain>().FromInstance(AppDomain.CurrentDomain);
            Container.Bind<App>().ToSelf().AsSingle();
            Container.Bind<IRouter>().To<Router>().AsSingle();
            Container.Bind<DiscordRoles>().FromInstance(new DiscordRoles {
                Roles = new Dictionary<string, ulong>() {
                    {"Admin", 369038333193486336},
                    {"Staff Member", 369023395528179713},
                    {"Counselor", 613088640079364132},
                    {"Community PR Manager", 590542704195272716},
                    {"Moderator", 369023369200271372},
                    {"Asset Modder", 475753077555724319},
                    {"Ambassador", 611204178190270465},
                    {"Legendary Patron", 562484946506219520},
                    {"Elder Patron", 562484913765482496},
                    {"Grandmaster Patron", 562484883822215168},
                    {"Adept Patron", 562484821524217857},
                    {"Expert Patron", 562484795267874866},
                    {"Journeyman Patron", 562484688812244992},
                    {"Apprentice Patron", 562482256308994048}
                }
            });
            SetupLogging();
            InstallServices();
            InstallControllers();
            InstallJobs();
        }

        private void SetupLogging() {
            var levelMap = new LevelMap();
            levelMap.Add(Level.All);
            levelMap.Add(Level.Error);
            levelMap.Add(Level.Warn);
            levelMap.Add(Level.Info);
            levelMap.Add(Level.Debug);
            levelMap.Add(Level.Off);
            Container.Bind<LevelMap>().FromInstance(levelMap);
        }

        private void InstallServices() {
            Container.Bind<IConfigService>().To<ConfigService>().AsSingle();
            Container.Bind<IHttpService>().To<HttpService>().AsSingle();
            Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
            Container.BindInterfacesTo<DiscordService>().AsTransient();
            Container.Bind<ITelemetryService>().To<TelemetryService>().AsSingle();
            Container.Bind<IBugService>().To<DevOpsBugService>().AsSingle();
        }

        private void InstallControllers() {
            Container.Bind<IController>().To<ConfigController>().AsSingle();
            Container.Bind<IController>().To<HttpController>().AsSingle();
            Container.Bind<IController>().To<PageController>().AsSingle();
            Container.Bind<IController>().To<DiscordController>().AsSingle();
            Container.Bind<IController>().To<PhysicsController>().AsSingle();
            Container.Bind<IController>().To<LogController>().AsSingle();
            Container.Bind<IController>().To<TelemetryController>().AsSingle();
            Container.Bind<IController>().To<BugController>().AsSingle();
            Container.Bind<IController>().To<DebugController>().AsSingle();
        }

        private void InstallJobs() {
            var factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler().Result;
            scheduler.JobFactory = new JobFactory();
            Container.Bind<IScheduler>().FromInstance(scheduler);
            Container.Bind<TelemetryReporterJob>().ToSelf().AsSingle();
        }
    }
}