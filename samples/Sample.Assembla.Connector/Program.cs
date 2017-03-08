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

namespace Sample.Assembla.Connector
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();

            Console.WriteLine("Hello World!");
        }

        static async Task MainAsync()
        {
            ILoggerFactory loggerFactory = new LoggerFactory();

            IHttpClient httpClient = new HttpClientImpl();

            IAssemblaClient client = new HttpAssemblaClient(httpClient, loggerFactory.CreateLogger<HttpAssemblaClient>());

            //var tags = await client.Tags.GetAllAsync("studentum");

            //foreach (var tag in tags)
            //{
            //    Console.WriteLine($"{tag.Id}\t{tag.Name}\t\t{tag.CreatedAt:yyyy-MM-dd}");
            //}

            //var aTag = await client.Tags.GetAsync("studentum", 615563);

            //Console.WriteLine($"{aTag.Id}\t{aTag.Name}\t\t{aTag.CreatedAt:yyyy-MM-dd}");

            var tag = new Tag { Name = "New Tag" };

            var createdTag = await client.Tags.CreateAsync("mont-blanc", tag);

            createdTag.ShouldNotBeNull();
            createdTag.Name.ShouldBe(tag.Name);

            const string newName = "New Name";
            await client.Tags.UpdateAsync("mont-blanc", new Tag { Id = createdTag.Id, Name = newName, State = TagState.Hidden });

            var updatedTag = await client.Tags.GetAsync("mont-blanc", createdTag.Id);

            updatedTag.Name.ShouldBe(newName);
            updatedTag.Id.ShouldBe(createdTag.Id);

            await client.Tags.DeleteAsync("mont-blanc", updatedTag.Id);

            Console.ReadLine();
        }
    }

    public class HttpClientImpl : IHttpClient
    {
        private readonly HttpClient client = CreateHttpClient();
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore, DateFormatHandling = DateFormatHandling.IsoDateFormat };
        
        static HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(@"https://api.assembla.com") };

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
            using (var request = new HttpRequestMessage(HttpMethod.Delete, ComposeUrl(url, query)))
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<TResult> GetAsync<TResult>(string url, IReadOnlyDictionary<string, string> query = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, ComposeUrl(url, query)))
            using (var response = await client.SendAsync(request))
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

            using (var request = new HttpRequestMessage(HttpMethod.Post, ComposeUrl(url, query)) {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            })
            using (var response = await client.SendAsync(request))
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

            using (var request = new HttpRequestMessage(HttpMethod.Put, ComposeUrl(url, query))
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            })
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }
}