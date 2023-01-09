using System.Collections.Generic;
using Lou;
using Lou.Controllers;
using NUnit.Framework;

namespace LouPluginTest.Core {
    [TestFixture]
    public class RouterTest : LouPluginTest {
        private IRouter _router;

        private List<IController> _controllers;

        [OneTimeSetUp]
        public void Init() {
            _router = TestDIManager.Container.Resolve<IRouter>();
            _controllers = TestDIManager.Container.ResolveAll<IController>();
        }

        [Test]
        public void CanInitialize() {
            Assert.DoesNotThrow(() => _router.Initialize());
        }

        [Test]
        public void ControllersHaveRoutes() {
            Assert.That(_controllers, Is.Not.Null);
            _controllers.ForEach(controller => {
                Assert.That(controller.GetRoutes(), Is.Not.Null);
                var requests = controller.GetRoutes().RequestRoutes;
                var directRequests = controller.GetRoutes().DirectRequestRoutes;
                Assert.False(directRequests == null || requests == null);
                Assert.True(requests.Count + directRequests.Count > 0);
            });
        }

        [Test]
        public void CanRouteControllers() {
            _router.Initialize();
            _controllers.ForEach(controller => {
                var routes = controller.GetRoutes();
                foreach (var requestRoutesKey in routes.RequestRoutes.Keys) {
                    var handler = _router.RouteRequest(new LouRequest {EventId = requestRoutesKey});
                    Assert.That(handler, Is.Not.Null);
                }
                foreach (var directRequestRoutesKey in routes.DirectRequestRoutes.Keys) {
                    var handler = _router.RouteDirectRequest(new LouDirectRequest {Data = new object[1] {directRequestRoutesKey}});
                    Assert.That(handler, Is.Not.Null);
                }
            });
        }
    }
}