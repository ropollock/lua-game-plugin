using System.Collections.Generic;
using System.Linq;
using Gameplay.Plugin;
using NLua;

namespace Lou {
    public static class Extensions {
        public static Dictionary<string, string> ToDictionary(this LuaTable luaTable) {
            return luaTable.Keys.Cast<object>().ToDictionary(key => key.ToString(), key => luaTable[key].ToString());
        }

        public static LouRequest ToLouRequest(this LuaRequest luaRequest) {
            return new LouRequest() {EventId = luaRequest.EventId, Data = luaRequest.Data};
        }

        public static string LuaResponseToString(this LuaResponse luaResponse) {
            return $"{{\"EventId\": {luaResponse.EventId}, \"DestinationObjectId\": {luaResponse.DestinationObjectId}, \"Data\": [{string.Join(", ", luaResponse.Data)}]}}";
        }
    }
}