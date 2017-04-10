using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Kralizek.Assembla.Connector.Files;
using Kralizek.Assembla.Connector.Milestones;
using Kralizek.Assembla.Connector.Spaces;
using Kralizek.Assembla.Connector.Tags;
using Kralizek.Assembla.Connector.Tickets;
using Kralizek.Assembla.Connector.Users;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kralizek.Assembla.Connector
{

    public partial class HttpAssemblaClient : IAssemblaClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore, DateFormatHandling = DateFormatHandling.IsoDateFormat };

        public HttpAssemblaClient(AssemblaAuthenticator authenticator, ILogger<HttpAssemblaClient> logger)
        {
            if (authenticator == null) throw new ArgumentNullException(nameof(authenticator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = CreateClient(authenticator);
        }

        private static HttpClient CreateClient(AssemblaAuthenticator authenticator)
        {
            return new HttpClient(authenticator) {BaseAddress = authenticator.ServiceUri};
        }

        public ISpaceConnector Spaces => this;

        public IMilestoneConnector Milestones => this;

        public ITicketConnector Tickets => this;

        public ITagConnector Tags => this;

        public IFileConnector Files => this;

        public IUserConnector Users => this;

        #region Http methods

        private string ComposeUrl(string url, IReadOnlyDictionary<string, string> query = null)
        {
            var queryPart = string.Empty;

            if (query != null)
            {
                queryPart = $"?{string.Join("&", query.Select(i => $"{i.Key}={i.Value}"))}";
            }

            return url + queryPart;
        }

        private async Task DeleteAsync(string url, IReadOnlyDictionary<string, string> query = null)
        {
            var requestUrl = ComposeUrl(url, query);

            using (var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl))
            {
                await LogRequest(request);

                using (var response = await _client.SendAsync(request))
                {
                    await LogResponse(response);

                    response.EnsureSuccessStatusCode();
                }
            }
        }

        private async Task<TResult> GetJsonAsync<TResult>(string url, IReadOnlyDictionary<string, string> query = null)
        {
            var requestUrl = ComposeUrl(url, query);

            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            {
                await LogRequest(request);
                using (var response = await _client.SendAsync(request))
                {
                    await LogResponse(response);

                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResult>(content);

                    return result;
                }
            }
        }

        private async Task<byte[]> GetRawAsync(string url, IReadOnlyDictionary<string, string> query = null)
        {
            var requestUrl = ComposeUrl(url, query);

            _logger.LogDebug($"GET: {requestUrl}");

            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            {
                await LogRequest(request);

                using (var response = await _client.SendAsync(request))
                {
                    await LogResponse(response);

                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsByteArrayAsync();

                    return content;
                }
            }
        }

        private async Task<TResult> PostAsync<TContent, TResult>(string url, TContent content, IReadOnlyDictionary<string, string> query = null)
        {
            var json = JsonConvert.SerializeObject(content, SerializerSettings);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var requestUrl = ComposeUrl(url, query);
            
            using (var request = new HttpRequestMessage(HttpMethod.Post, requestUrl) { Content = httpContent })
            {
                await LogRequest(request, includeContent: true);

                using (var response = await _client.SendAsync(request))
                {
                    await LogResponse(response);

                    response.EnsureSuccessStatusCode();

                    var incomingContent = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResult>(incomingContent);

                    return result;
                }
            }
        }

        private async Task<TResult> PostAsync<TResult>(string url, HttpContent content = null, IReadOnlyDictionary<string, string> query = null)
        {
            var requestUrl = ComposeUrl(url, query);

            using (var request = new HttpRequestMessage(HttpMethod.Post, requestUrl) {Content = content})
            {
                await LogRequest(request);

                using (var response = await _client.SendAsync(request))
                {
                    await LogResponse(response);

                    response.EnsureSuccessStatusCode();

                    var incomingContent = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResult>(incomingContent);

                    return result;
                }
            }
        }

        private async Task PutAsync(string url, HttpContent content, IReadOnlyDictionary<string, string> query = null)
        {
            var requestUrl = ComposeUrl(url, query);

            using (var request = new HttpRequestMessage(HttpMethod.Put, requestUrl) {Content = content})
            {
                await LogRequest(request);

                using (var response = await _client.SendAsync(request))
                {
                    await LogResponse(response);
                }
            }
        }

        private async Task PutAsync<TContent>(string url, TContent content, IReadOnlyDictionary<string, string> query = null)
        {
            var json = JsonConvert.SerializeObject(content, SerializerSettings);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var requestUrl = ComposeUrl(url, query);
            
            using (var request = new HttpRequestMessage(HttpMethod.Put, requestUrl) { Content = httpContent })
            {
                await LogRequest(request, includeContent: true);

                using (var response = await _client.SendAsync(request))
                {
                    await LogResponse(response);
                }
            }
        }

        private static readonly IReadOnlyDictionary<HttpMethod, EventId> HttpMethodEventIds = new Dictionary<HttpMethod, EventId>
        {
            [HttpMethod.Get] = new EventId(1001, HttpMethod.Get.Method),
            [HttpMethod.Post] = new EventId(1002, HttpMethod.Post.Method),
            [HttpMethod.Put] = new EventId(1003, HttpMethod.Put.Method),
            [HttpMethod.Delete] = new EventId(1004, HttpMethod.Delete.Method)
        };

        private async Task LogRequest(HttpRequestMessage request, bool includeContent = false)
        {
            var eventId = HttpMethodEventIds[request.Method];

            var state = new
            {
                method = request.Method.Method.ToUpper(),
                requestUri = request.RequestUri,
                content = includeContent ? await (request.Content?.ReadAsStringAsync() ?? Task.FromResult((string) null)) : null,
                contentType = request.Content?.GetType().Name
            };

            _logger.LogDebug(eventId, state, s => $"{s.method}: {s.requestUri} {(includeContent ? s.content : s.contentType)}");
        }

        private async Task LogResponse(HttpResponseMessage response)
        {
            var eventId = new EventId((int)response.StatusCode, response.ReasonPhrase);

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var errorMessage = (string)JObject.Parse(responseContent)["error"];

                    var state = new
                    {
                        method = response.RequestMessage.Method.Method.ToUpper(),
                        requestUri = response.RequestMessage.RequestUri,
                        status = response.StatusCode,
                        reasonPhrase = response.ReasonPhrase,
                        errorMessage
                    };

                    _logger.LogError(eventId, state, s => $"{s.method}: {s.requestUri.PathAndQuery} {s.status:D} '{s.reasonPhrase}' '{s.errorMessage}'");
                }
                catch (Exception ex)
                {
                    var state = new
                    {
                        method = response.RequestMessage.Method.Method.ToUpper(),
                        requestUri = response.RequestMessage.RequestUri,
                        status = response.StatusCode,
                        reasonPhrase = response.ReasonPhrase
                    };

                    _logger.LogError(eventId, state, ex, (s, e) => $"{s.method}: {s.requestUri.PathAndQuery} {s.status:D} '{s.reasonPhrase}' '{e.Message}'");
                }
            }
            else
            {
                var state = new
                {
                    method = response.RequestMessage.Method.Method.ToUpper(),
                    requestUri = response.RequestMessage.RequestUri,
                    status = response.StatusCode,
                    reasonPhrase = response.ReasonPhrase
                };

                _logger.LogDebug(eventId, state, s => $"{s.method}: {s.requestUri.PathAndQuery} {s.status:D} '{s.reasonPhrase}'");
            }
        }

        #endregion
    }
}
