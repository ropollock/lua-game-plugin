using Lou;
using Lou.Services;
using NUnit.Framework;

namespace LouPluginTest.Services {
    [TestFixture]
    public class PhysicsTest : LouPluginTest {
        private IPhysicsService _physics;

        [OneTimeSetUp]
        public void Init() {
            _physics = TestDIManager.Container.Resolve<IPhysicsService>();
        }

        [Test]
        public void TestPointInBounds() {
            var tl = new Vector2 {x = 0.2f, y = 2.1f};
            var tr = new Vector2 {x = 2.01f, y = 2.5f};
            var br = new Vector2 {x = 2.01f, y = 0.1f};
            var bl = new Vector2 {x = 0f, y = 0f};
            var pt = new Vector2 {x = 1.3f, y = 1.2f};
            Assert.That(_physics.PointInRectangle(tl, tr, br, bl, pt));
        }
        
        [Test]
        public void TestPointInBounds2() {
            var tl = new Vector2 {x = 3.780403137207f, y = 1.0798943042755f};
            var tr = new Vector2 {x = 7.4303512573242f, y = 1.0798943042755f};
            var br = new Vector2 {x = 7.4303512573242f, y = 2.8070151805878f};
            var bl = new Vector2 {x = 3.780403137207f, y = 2.8070151805878f};
            var pt = new Vector2 {x = 5.8167462348938f, y = 1.8483906984329f};
            Assert.That(_physics.PointInRectangle(tl, tr, br, bl, pt));
        }

        [Test]
        public void TestPointInBounds3() {
            var tl = new Vector2 {x = 2.5880289077759f, y = 12.517186164856f};
            var tr = new Vector2 {x = 5.5880289077759f, y = 16.167133331299f};
            var br = new Vector2 {x = 0.86090803146362f, y = 19.167133331299f};
            var bl = new Vector2 {x = 3.860907793045f, y = 15.517186164856f};
            var pt = new Vector2 {x = 3.3436224460602f, y = 16.348304748535f};
            Assert.That(_physics.PointInRectangle(tl, tr, br, bl, pt));
        }
        
        [Test]
        public void TestPointInBounds4() {
            var tl = new Vector2 {x = -1257.44f, y = 368.6651f};
            var tr = new Vector2 {x = -1254.111f, y = 365.3368f};
            var br = new Vector2 {x = -1252.724f, y = 366.7242f};
            var bl = new Vector2 {x = -1256.052f, y = 370.0525f};
            var pt = new Vector2 {x = -1255.114f, y = 367.7275f};
            Assert.That(_physics.PointInRectangle(tl, tr, br, bl, pt));
        }

        [Test]
        public void TestPointOutOfBounds() {
            var tl = new Vector2 {x = 0f, y = 2f};
            var tr = new Vector2 {x = 2f, y = 2f};
            var br = new Vector2 {x = 2f, y = 0f};
            var bl = new Vector2 {x = 0f, y = 0f};
            var pt = new Vector2 {x = 7f, y = 5f};
            Assert.That(!_physics.PointInRectangle(tl, tr, br, bl, pt));
        }
    }
}