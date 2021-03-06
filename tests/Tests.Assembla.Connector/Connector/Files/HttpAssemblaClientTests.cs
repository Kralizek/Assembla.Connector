﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Kralizek.Assembla;
using Kralizek.Assembla.Connector;
using Kralizek.Assembla.Connector.Files;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;
using WorldDomination.Net.Http;

namespace Tests.Assembla.Connector.Files
{
    [TestFixture]
    public class HttpAssemblaClientTests 
    {
        private readonly Mock<ILogger<HttpAssemblaClient>> mockLogger;

        public HttpAssemblaClientTests()
        {
            mockLogger = new Mock<ILogger<HttpAssemblaClient>>();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore };
        }

        [Test]
        public async Task GetAsync_should_return_the_requested_file()
        {
            var file = new File { Id = "documentId", SpaceId = "spaceIdOrWikiName" };
            string json = JsonConvert.SerializeObject(file);

            var options = new[]
            {
                new HttpMessageOptions
                {
                    RequestUri = new Uri("https://api.assembla.com/v1/spaces/spaceIdOrWikiName/documents/documentId"),
                    HttpMethod = HttpMethod.Get,
                    HttpResponseMessage = FakeHttpMessageHandler.GetStringHttpResponseMessage(json)
                }
            };
            var messageHandler = new FakeHttpMessageHandler(options);

            var authenticator = new TestAuthenticator(messageHandler);

            var client = new HttpAssemblaClient(authenticator, mockLogger.Object);

            await client.Files.GetAsync(file.SpaceId, file.Id);

            options[0].NumberOfTimesCalled.ShouldBe(1);
        }

        [Test]
        public async Task GetAllAsync_should_attach_paging_parameters()
        {
            var file = new File { Id = "documentId", SpaceId = "spaceIdOrWikiName" };
            string json = JsonConvert.SerializeObject(new[] { file });

            var options = new[]
{
                new HttpMessageOptions
                {
                    RequestUri = new Uri("https://api.assembla.com/v1/spaces/spaceIdOrWikiName/documents?page=2&per_page=10"),
                    HttpMethod = HttpMethod.Get,
                    HttpResponseMessage = FakeHttpMessageHandler.GetStringHttpResponseMessage(json)
                }
            };
            var messageHandler = new FakeHttpMessageHandler(options);

            var authenticator = new TestAuthenticator(messageHandler);

            var client = new HttpAssemblaClient(authenticator, mockLogger.Object);

            await client.Files.GetAllAsync(file.SpaceId, page: 2, pageSize: 10);

            options[0].NumberOfTimesCalled.ShouldBe(1);
        }

    }
}
