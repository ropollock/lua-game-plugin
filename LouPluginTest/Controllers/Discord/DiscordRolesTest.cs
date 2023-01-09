using System.Collections.Generic;
using Lou.Controllers;
using NUnit.Framework;

namespace LouPluginTest.Controllers.Discord {
    [TestFixture]
    public class DiscordRolesTest : LouPluginTest {

        private DiscordRoles _discordRoles;
        
        [OneTimeSetUp]
        public void Init() {
            _discordRoles = TestDIManager.Container.Resolve<DiscordRoles>();
        }

        [Test]
        public void CanFindRoles() {
            var testId = _discordRoles.Roles["Test Role"];
            var adminId = _discordRoles.Roles["Admin"];
            var roles = _discordRoles.FindRoles(new List<ulong> {testId, adminId});
            Assert.That(roles.Count == 2);
            Assert.That(roles.Contains("Test Role"));
            Assert.That(roles.Contains("Admin"));
        }

        [Test]
        public void CanFindNoRoles() {
            var roles = _discordRoles.FindRoles(new List<ulong> {2345});
            Assert.That(roles.Count == 0);
        }

        [Test]
        public void HasNoRoles() {
            var roles = _discordRoles.FindRoles(new List<ulong>());
            Assert.That(roles.Count == 0);
        }
        
        [Test]
        public void CanHandleNullRoles() {
            var roles = _discordRoles.FindRoles(null);
            Assert.That(roles.Count == 0);
        }
    }
}