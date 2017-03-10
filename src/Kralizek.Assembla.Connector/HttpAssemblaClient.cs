using Assembla.Milestones;
using Assembla.Spaces;
using Assembla.Tags;
using Assembla.Tickets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Assembla.Documents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assembla
{

    public partial class HttpAssemblaClient : IAssemblaClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore, DateFormatHandling = DateFormatHandling.IsoDateFormat };

        public HttpAssemblaClient(HttpClient client, ILogger<HttpAssemblaClient> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ISpaceConnector Spaces => this;

        public IMilestoneConnector Milestones => this;

        public ITicketConnector Tickets => this;

        public ITagConnector Tags => this;
        public IDocumentConnector Documents => this;

        #region Http methods

        private string ComposeUrl(string url, IReadOnlyDictionary<string, string> query = null)
        {
            string queryPart = string.Empty;

            if (query != null)
            {
                queryPart = $"?{string.Join("&", query.Select(i => $"{i.Key}={i.Value}"))}";
            }

            return url + queryPart;
        }

        private async Task DeleteAsync(string url, IReadOnlyDictionary<string, string> query = null)
        {
            string requestUrl = ComposeUrl(url, query);

            _logger.LogDebug($"DELETE: {requestUrl}");

            using (var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl))
            using (var response = await _client.SendAsync(request))
            {
                await LogResponse(response);

                response.EnsureSuccessStatusCode();
            }
        }

        private async Task<TResult> GetAsync<TResult>(string url, IReadOnlyDictionary<string, string> query = null)
        {
            string requestUrl = ComposeUrl(url, query);

            _logger.LogDebug($"GET: {requestUrl}");

            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            using (var response = await _client.SendAsync(request))
            {
                await LogResponse(response);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                TResult result = JsonConvert.DeserializeObject<TResult>(content);

                return result;
            }
        }

        private async Task<byte[]> GetRawAsync(string url, IReadOnlyDictionary<string, string> query = null)
        {
            string requestUrl = ComposeUrl(url, query);

            _logger.LogDebug($"GET: {requestUrl}");

            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            using (var response = await _client.SendAsync(request))
            {
                await LogResponse(response);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsByteArrayAsync();

                return content;
            }
        }

        private async Task<TResult> PostJsonAsync<TContent, TResult>(string url, TContent content, IReadOnlyDictionary<string, string> query = null)
        {
            string json = JsonConvert.SerializeObject(content, SerializerSettings);
            string requestUrl = ComposeUrl(url, query);

            _logger.LogDebug($"POST: {requestUrl} {json}");

            using (var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            })
            using (var response = await _client.SendAsync(request))
            {
                await LogResponse(response);

                response.EnsureSuccessStatusCode();

                string incomingContent = await response.Content.ReadAsStringAsync();

                TResult result = JsonConvert.DeserializeObject<TResult>(incomingContent);

                return result;
            }
        }

        private async Task<TResult> PostAsync<TResult>(string url, HttpContent content, IReadOnlyDictionary<string, string> query = null)
        {
            string requestUrl = ComposeUrl(url, query);

            _logger.LogDebug($"POST: {requestUrl} {content.GetType().Name}");

            using (var request = new HttpRequestMessage(HttpMethod.Post, requestUrl) { Content = content })
            using (var response = await _client.SendAsync(request))
            {
                await LogResponse(response);

                response.EnsureSuccessStatusCode();

                string incomingContent = await response.Content.ReadAsStringAsync();

                TResult result = JsonConvert.DeserializeObject<TResult>(incomingContent);

                return result;
            }
        }

        private async Task<TResult> PostCommandAsync<TResult>(string url, IReadOnlyDictionary<string, string> query = null)
        {
            string requestUrl = ComposeUrl(url, query);

            _logger.LogDebug($"POST: {requestUrl}");

            using (var request = new HttpRequestMessage(HttpMethod.Post, requestUrl))
            using (var response = await _client.SendAsync(request))
            {
                await LogResponse(response);

                response.EnsureSuccessStatusCode();

                string incomingContent = await response.Content.ReadAsStringAsync();

                TResult result = JsonConvert.DeserializeObject<TResult>(incomingContent);

                return result;
            }
        }

        private async Task PutAsync<TContent>(string url, TContent content, IReadOnlyDictionary<string, string> query = null)
        {
            string json = JsonConvert.SerializeObject(content, SerializerSettings);
            string requestUrl = ComposeUrl(url, query);

            _logger.LogDebug($"PUT: {requestUrl} {json}");

            using (var request = new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            })
            using (var response = await _client.SendAsync(request))
            {
                await LogResponse(response);
            }
        }

        private async Task LogResponse(HttpResponseMessage response)
        {
            var eventId = new EventId((int)response.StatusCode, response.ReasonPhrase);

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    string errorMessage = (string)JObject.Parse(responseContent)["error"];

                    var state = new
                    {
                        method = response.RequestMessage.Method.Method.ToUpper(),
                        requestUri = response.RequestMessage.RequestUri.PathAndQuery,
                        status = response.StatusCode,
                        reasonPhrase = response.ReasonPhrase,
                        errorMessage
                    };

                    _logger.LogError(eventId, state, s => $"{s.method}: {s.requestUri} {s.status:D} '{s.reasonPhrase}' '{s.errorMessage}'");
                }
                catch (Exception ex)
                {
                    var state = new
                    {
                        method = response.RequestMessage.Method.Method.ToUpper(),
                        requestUri = response.RequestMessage.RequestUri.PathAndQuery,
                        status = response.StatusCode,
                        reasonPhrase = response.ReasonPhrase
                    };

                    _logger.LogError(eventId, state, ex, (s, e) => $"{s.method}: {s.requestUri} {s.status:D} '{s.reasonPhrase}' '{e.Message}'");
                }
            }
            else
            {
                var state = new
                {
                    method = response.RequestMessage.Method.Method.ToUpper(),
                    requestUri = response.RequestMessage.RequestUri.PathAndQuery,
                    status = response.StatusCode,
                    reasonPhrase = response.ReasonPhrase
                };

                _logger.LogDebug(eventId, state, s => $"{s.method}: {s.requestUri} {s.status:D} '{s.reasonPhrase}'");
            }
        }


        #endregion
    }
}
