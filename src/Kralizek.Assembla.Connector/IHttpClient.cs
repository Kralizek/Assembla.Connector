using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assembla
{
    public interface IHttpClient
    {
        Task<TResult> GetAsync<TResult>(string url, IReadOnlyDictionary<string, string> query = null);

        Task<TResult> PostAsync<TContent, TResult>(string url, TContent content, IReadOnlyDictionary<string, string> query = null);

        Task PutAsync<TContent>(string url, TContent content, IReadOnlyDictionary<string, string> query = null);

        Task DeleteAsync<TContent>(string url, IReadOnlyDictionary<string, string> query = null);
    }
}