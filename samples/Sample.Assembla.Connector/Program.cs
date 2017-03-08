using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assembla;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Assembla.Tags;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Sample.Assembla.Connector
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();

            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddOptions();

            services.Configure<HttpClientSettings>(configuration.GetSection("Assembla"));

            services.AddTransient<IHttpClient, HttpClientImpl>();
            services.AddTransient<IAssemblaClient, HttpAssemblaClient>();


            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetRequiredService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug)
                .AddDebug(LogLevel.Debug);

            var client = serviceProvider.GetRequiredService<IAssemblaClient>();

            await Tags(client);

            Console.WriteLine("Sample completed");
            Console.ReadLine();
        }

        private static async Task Tags(IAssemblaClient client)
        {
            var tag = new Tag {Name = "New Tag"};

            var createdTag = await client.Tags.CreateAsync("mont-blanc", tag);

            createdTag.ShouldNotBeNull();
            createdTag.Name.ShouldBe(tag.Name);

            const string newName = "New Name";
            await client.Tags.UpdateAsync("mont-blanc", new Tag {Id = createdTag.Id, Name = newName, State = TagState.Hidden});

            var updatedTag = await client.Tags.GetAsync("mont-blanc", createdTag.Id);

            updatedTag.Name.ShouldBe(newName);
            updatedTag.Id.ShouldBe(createdTag.Id);

            await client.Tags.DeleteAsync("mont-blanc", updatedTag.Id);
        }
    }

    public class HttpClientSettings
    {
        public string ApiKey { get; set; }
        public string ApiSecretKey { get; set; }
    }

    public class HttpClientImpl : IHttpClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<HttpClientImpl> _logger;
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore, DateFormatHandling = DateFormatHandling.IsoDateFormat };

        public HttpClientImpl(IOptions<HttpClientSettings> settings, ILogger<HttpClientImpl> logger)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _client = CreateHttpClient(settings.Value);
            _logger = logger;
        }
        
        static HttpClient CreateHttpClient(HttpClientSettings settings)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(@"https://api.assembla.com") };
            client.DefaultRequestHeaders.Add("X-Api-Key", settings.ApiKey);
            client.DefaultRequestHeaders.Add("X-Api-Secret", settings.ApiSecretKey);

            return client;
        }

        private string ComposeUrl(string url, IReadOnlyDictionary<string, string> query = null)
        {
            string queryPart = string.Empty;

            if (query != null)
            {
                queryPart = $"?{string.Join("&", query.Select(i => $"{i.Key}={i.Value}"))}";
            }

            return url + queryPart;
        }

        public async Task DeleteAsync<TContent>(string url, IReadOnlyDictionary<string, string> query = null)
        {
            string requestUrl = ComposeUrl(url, query);

            _logger.LogDebug($"DELETE: {requestUrl}");

            using (var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl))
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<TResult> GetAsync<TResult>(string url, IReadOnlyDictionary<string, string> query = null)
        {
            string requestUrl = ComposeUrl(url, query);

            _logger.LogDebug($"GET: {requestUrl}");

            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                TResult result = JsonConvert.DeserializeObject<TResult>(content);

                return result;
            }
        }

        public async Task<TResult> PostAsync<TContent, TResult>(string url, TContent content, IReadOnlyDictionary<string, string> query = null)
        {
            string json = JsonConvert.SerializeObject(content, SerializerSettings);
            string requestUrl = ComposeUrl(url, query);

            _logger.LogDebug($"POST: {requestUrl} {json}");

            using (var request = new HttpRequestMessage(HttpMethod.Post, requestUrl) {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            })
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                string incomingContent = await response.Content.ReadAsStringAsync();

                TResult result = JsonConvert.DeserializeObject<TResult>(incomingContent);

                return result;
            }
        }

        public async Task PutAsync<TContent>(string url, TContent content, IReadOnlyDictionary<string, string> query = null)
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
                response.EnsureSuccessStatusCode();
            }
        }
    }
}