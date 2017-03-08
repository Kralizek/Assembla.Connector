using System;
using System.Linq;
using System.Threading.Tasks;
using Assembla;
using Assembla.Spaces;
using Microsoft.Extensions.Logging;
using Assembla.Tags;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

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

            //await client.SampleTags();
            await client.SampleSpaces();


            Console.WriteLine("Sample completed");
            Console.ReadLine();
        }

        
    }

    public static class SamplesExtensions
    {
        public static async Task SampleTags(this IAssemblaClient client)
        {
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
        }

        public static async Task SampleSpaces(this IAssemblaClient client)
        {
            var newSpace = new Space {Name = "Test Space"};

            var createdSpace = await client.Spaces.CreateAsync(newSpace);

            createdSpace.ShouldNotBeNull();
            createdSpace.Name.ShouldBe(newSpace.Name);
            createdSpace.Id.ShouldNotBeNull();

            const string newName = "New Name";
            await client.Spaces.UpdateAsync(new Space {Name = newName, WikiName = createdSpace.WikiName});

            var updatedSpace = await client.Spaces.GetAsync(createdSpace.WikiName);

            updatedSpace.Name.ShouldBe(newName);
            updatedSpace.Id.ShouldBe(createdSpace.Id);

            //var copiedSpace = await client.Spaces.CopyAsync(createdSpace.WikiName, new Space {Name = "Copied Space", WikiName = "asd"});
            //copiedSpace.ShouldNotBeNull();

            var listAll = await client.Spaces.GetAllAsync();
            listAll.Count(c => c.Id == updatedSpace.Id).ShouldBe(1);
            //listAll.Count(c => c.Id == copiedSpace.Id).ShouldBe(1);

            await client.Spaces.DeleteAsync(updatedSpace.Id);
            //await client.Spaces.DeleteAsync(copiedSpace.Id);
        }
    }
}