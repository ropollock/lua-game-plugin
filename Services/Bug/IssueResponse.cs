using Gameplay.PublicLua;
using Newtonsoft.Json;

namespace Lou.Services {
    public class IssueResponse {
        [JsonIgnore]
        public bool success;

        [JsonProperty("project_id")]
        public string projectId;

        public string id;
        public string iid;
        public string title;

        [JsonProperty("web_url")]
        public string webUrl;

        public static LuaTableProxy ToLua(IssueResponse issue) {
            var t = new LuaTableProxy();
            t.TableData.Add(LuaTableItem.Create("Success"), LuaTableItem.Create(issue.success));
            if (issue.projectId != null) {
                t.TableData.Add(LuaTableItem.Create("ProjectId"), LuaTableItem.Create(issue.projectId));
            }

            if (issue.iid != null) {
                t.TableData.Add(LuaTableItem.Create("Id"), LuaTableItem.Create(issue.iid));
            }
            else if (issue.id != null) {
                t.TableData.Add(LuaTableItem.Create("Id"), LuaTableItem.Create(issue.id));
            }

            if (issue.title != null) {
                t.TableData.Add(LuaTableItem.Create("Title"), LuaTableItem.Create(issue.title));
            }

            if (issue.webUrl != null) {
                t.TableData.Add(LuaTableItem.Create("WebUrl"), LuaTableItem.Create(issue.webUrl));
            }

            return t;
        }
    }
}