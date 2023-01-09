using System.Collections.Generic;
using System.Linq;
using Gameplay.Plugin;

namespace Lou {
    public class LouRequest : LuaRequest {
        
        public ulong ResponseTarget {
            get {
                double dbl = (double) Data[0];
                return (ulong) dbl;
            }
        }

        public string ResponseEvent => (string) Data[1];
        public List<object> RequestParams =>  Data.Length > 2 ? Data.Skip(2).ToList() : new List<object>();
        
    }
}