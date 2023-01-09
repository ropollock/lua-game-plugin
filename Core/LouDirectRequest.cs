using System.Collections.Generic;
using System.Linq;

namespace Lou {
    public struct LouDirectRequest {
        public object[] Data { get; set; }
        public string EventId => (string) Data[0];
        public List<object> RequestParams => Data.Length > 1 ? Data.Skip(1).ToList() : new List<object>();
    }
}