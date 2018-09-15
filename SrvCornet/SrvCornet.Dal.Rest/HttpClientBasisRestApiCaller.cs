using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace SrvCornet.Dal.Rest
{
    public abstract class HttpClientBasisRestApiCaller : IRestApiCaller
    {
        public HttpClient client;
        public HttpClientBasisRestApiCaller()
        {
        }
        public abstract Task PreprocessResponse(HttpResponseMessage response);
        public async Task<string> GetAsync(string path, Dictionary<string, string> headers = null, Dictionary<string, string> queries = null)
        {
            var request = InitRequest(HttpMethod.Get, path, queries);
            SetRequestHeaders(request, headers);
            return await SendAsync(request);
        }
        public async Task<string> PostAsync(string path, Dictionary<string, string> headers = null, Dictionary<string, string> queries = null, object obj = null)
        {
            var request = InitRequest(HttpMethod.Post, path, queries);
            SetRequestHeaders(request, headers);
            SetRequestBody(request, obj);
            return await SendAsync(request);
        }
        public async Task<string> PutAsync(string path, Dictionary<string, string> headers = null, Dictionary<string, string> queries = null, object obj = null)
        {
            var request = InitRequest(HttpMethod.Put, path, queries);
            SetRequestHeaders(request, headers);
            SetRequestBody(request, obj);
            return await SendAsync(request);
        }
        public async Task<string> DeleteAsync(string path, Dictionary<string, string> headers = null, Dictionary<string, string> queries = null, object obj = null)
        {
            var request = InitRequest(HttpMethod.Delete, path, queries);
            SetRequestHeaders(request, headers);
            SetRequestBody(request, obj);
            return await SendAsync(request);
        }
        private void SetRequestBody(HttpRequestMessage request, object obj)
        {
            if (obj == null)
                return;
            request.Content = new StringContent(SerializeObject(obj), Encoding.UTF8);
        }

        protected abstract string SerializeObject(object obj);
        private async Task<string> SendAsync(HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);
            await PreprocessResponse(response);
            string bodyText = await response.Content.ReadAsStringAsync();
            return bodyText;
        }
        private HttpRequestMessage InitRequest(HttpMethod method, string path, Dictionary<string, string> queries)
        {
            var requestUri = QueryHelpers.AddQueryString(path, queries);
            HttpRequestMessage request = new HttpRequestMessage(method, requestUri);
            return request;
        }
        private void SetRequestHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
        {
            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);
            }
        }
    }
}
