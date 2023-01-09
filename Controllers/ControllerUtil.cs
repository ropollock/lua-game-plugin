using System;
using Gameplay.Plugin;
using Gameplay.PublicLua;
using NLua;

namespace Lou.Controllers {
    public static class ControllerUtil {
        public static LuaResponse CreateLuaResponse(LouRequest request, object[] data) {
            var luaResponse = new LuaResponse {
                EventId = request.ResponseEvent,
                DestinationObjectId = request.ResponseTarget,
                Data = data
            };
            return luaResponse;
        }

        public static string GetInnerException(Exception e) {
            while (e.InnerException != null) {
                e = e.InnerException;
            }

            return e.Message;
        }

        public static Vector3 LuaToVector3(LuaTable table) {
            return new Vector3((float)(double)table["x"], (float)(double)table["y"], (float)(double)table["z"]);
        }
        
        public static Vector2 LuaToVector2(LuaTable table) {
            return new Vector2((float)(double)table["x"], (float)(double)table["y"]);
        }

        public static LuaTableProxy Vector3ToLua(Vector3 v) {
            var table = new LuaTableProxy();
            table.TableData.Add(LuaTableItem.Create("x"), LuaTableItem.Create(v.x));
            table.TableData.Add(LuaTableItem.Create("y"), LuaTableItem.Create(v.y));
            table.TableData.Add(LuaTableItem.Create("z"), LuaTableItem.Create(v.z));
            return table;
        }
    }
}