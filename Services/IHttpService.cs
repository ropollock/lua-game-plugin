using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lou.Services {
    public interface IHttpService {

        Task<HttpResponseMessage> Post(string address, Dictionary<string, string> contents);
        
        Task<HttpResponseMessage> Post(string address, HttpContent contents);
        
        Task<HttpResponseMessage> Post(string address, string contents, string mediaType = "application/json", Dictionary<string, string> headers = null);
        
        Task<HttpResponseMessage> Get(string address);
        
        HttpResponseMessage SyncPost(string address, Dictionary<string, string> contents);
        
        HttpResponseMessage SyncGet(string address);
        
    }
}