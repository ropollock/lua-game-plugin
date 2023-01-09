using System.Collections.Generic;
using System.Linq;

namespace Lou.Controllers {
    public struct DiscordRoles {
        public Dictionary<string, ulong> Roles;

        public List<string> FindRoles(List<ulong> userRoles) {
            if(userRoles == null) return new List<string>();
            return Roles?.Where(r => userRoles.Contains(r.Value)).Select(r => r.Key).ToList();
        }
    }
}