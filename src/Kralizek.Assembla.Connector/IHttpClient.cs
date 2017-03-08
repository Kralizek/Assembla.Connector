using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assembla
{
    public interface IHttpClient
    {
        Task<TResult> GetAsync<TResult>(string url, IReadOnlyDictionary<string, string> query = null);

        Task<TResult> PostAsync<TContent, TResult>(string url, TContent content, IReadOnlyDictionary<string, string> query = null);

        Task<TResult> PostAsync<TResult>(string url, IReadOnlyDictionary<string, string> query = null);

        Task PutAsync<TContent>(string url, TContent content, IReadOnlyDictionary<string, string> query = null);

        Task DeleteAsync(string url, IReadOnlyDictionary<string, string> query = null);
    }

}