using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.PublicLua;
using log4net;
using Lou.Services;
using NLua;
using Zenject;

namespace Lou.Controllers.Physics {
    public sealed class PhysicsController : AbstractController {
        
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PhysicsController));
        
        [Inject]
        private IPhysicsService _physics;

        [Inject]
        private LouContext _context;

        private async void InRect(LouRequest request) {
            var requestParams = request.RequestParams;
            var table = ((LuaTable) requestParams[0]);
            var result = await Task.Run(() => AsyncTestRect(table));
            var response = ControllerUtil.CreateLuaResponse(request, new object[] {result});
            _context.ResponseQueue.Enqueue(response);
            table.Dispose();
        }

        private object[] InRect(LouDirectRequest request) {
            var requestParams = request.RequestParams;
            var table = (LuaTable) requestParams[0];
            var result = TestRect(table);
            table.Dispose();
            return new object[] {result};
        }

        private async Task<bool> AsyncTestRect(LuaTable table) {
            return TestRect(table);
        }

        private bool TestRect(LuaTable table) {
            List<Vector3> vertices = new List<Vector3>();
            // Points are expected in Clockwise order. TL, TR, BR, BL
            var pts = (LuaTable) table["points"];
            for (int i = 1; i <= pts.Keys.Count; i++) {
                var pt = (LuaTable) pts[i];
                vertices.Add(ControllerUtil.LuaToVector2(pt));
            }
            var loc = ControllerUtil.LuaToVector2((LuaTable)table["loc"]);
            pts.Dispose();
            return _physics.PointInRectangle(vertices[0], vertices[1], vertices[3], vertices[2], loc);
        }

        private object[] RotatePoints(LouDirectRequest request) {
            var requestParams = request.RequestParams;
            var table = (LuaTable) requestParams[0];

            List<Vector3> vertices = new List<Vector3>();
            var pts = (LuaTable) table["points"];
            for (int i = 1; i <= pts.Keys.Count; i++) {
                var pt = (LuaTable) pts[i];
                vertices.Add(ControllerUtil.LuaToVector3(pt));
            }

            var pivot = ControllerUtil.LuaToVector3((LuaTable) table["center"]);
            var rotation = ControllerUtil.LuaToVector3((LuaTable) table["rotation"]);
            var transformedVertices = _physics.RotateAroundPoint(vertices, rotation, pivot);
            LuaTableProxy tableResponse = new LuaTableProxy();
            for (int i = 0; i < transformedVertices.Count; i++) {
                tableResponse.TableData.Add(LuaTableItem.Create(i + 1),
                    LuaTableItem.Create(ControllerUtil.Vector3ToLua(transformedVertices[i])));
            }
            table.Dispose();
            return new object[] {tableResponse};
        }

        public override Routes GetRoutes() {
            var requestRoutes = new Dictionary<string, Action<LouRequest>> {
                {"Physics.InRect", InRect}
            };

            var directRequestRoutes = new Dictionary<string, Func<LouDirectRequest, object[]>> {
                {"Physics.InRect", InRect},
                {"Physics.RotatePoints", RotatePoints}
            };

            return new Routes {
                RequestRoutes = requestRoutes,
                DirectRequestRoutes = directRequestRoutes
            };
        }
    }
}