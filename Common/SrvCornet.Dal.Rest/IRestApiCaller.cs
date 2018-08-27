using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SrvCornet.Dal.Rest
{
    public interface IRestApiCaller
    {
        Task<string> GetAsync(string url, Dictionary<string, string> headers = null, Dictionary<string, string> query = null);
        Task<string> PostAsync(string path, Dictionary<string, string> headers = null, Dictionary<string, string> queries = null, object obj = null);
        Task<string> PutAsync(string path, Dictionary<string, string> headers = null, Dictionary<string, string> queries = null, object obj = null);
        Task<string> DeleteAsync(string path, Dictionary<string, string> headers = null, Dictionary<string, string> queries = null, object obj = null);
    }
}
