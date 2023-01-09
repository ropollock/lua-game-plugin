using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Lou.Services {
    public class HttpService : IHttpService {
        
        private static readonly HttpClient m_client = new HttpClient();
        
        [Inject]
        public void Initialize() {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public  Task<HttpResponseMessage> Post(string address, Dictionary<string, string> contents) {
            return m_client.PostAsync(address, new FormUrlEncodedContent(contents));
        }

        public Task<HttpResponseMessage> Post(string address, HttpContent contents) {
            return m_client.PostAsync(address, contents);
        }

        public Task<HttpResponseMessage> Post(string address, string contents, string mediaType = "application/json", Dictionary<string, string> headers = null) {
            var stringContent = new StringContent(contents, Encoding.UTF8, mediaType);
            m_client.DefaultRequestHeaders.Clear();
            if (headers != null) {
                foreach (var header in headers) {
                    if (header.Key == "Authorization") {
                        m_client.DefaultRequestHeaders.Authorization = 
                            new AuthenticationHeaderValue("Basic", 
                                Convert.ToBase64String(Encoding.ASCII.GetBytes(header.Value)));
                    }
                    else {
                        stringContent.Headers.TryAddWithoutValidation(header.Key, header.Value);                        
                    }
                }
            }
            
            return m_client.PostAsync(address, stringContent);
        }

        public Task<HttpResponseMessage> Get(string address) {
            return m_client.GetAsync(address);
        }

        public HttpResponseMessage SyncPost(string address, Dictionary<string, string> contents) {
            return Post(address, contents).Result;
        }

        public HttpResponseMessage SyncGet(string address) {
            return Get(address).Result;
        }
    }
}