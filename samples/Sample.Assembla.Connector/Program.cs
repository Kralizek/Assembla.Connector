using System;
using System.Threading.Tasks;
using Assembla;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Sample.Assembla.Connector.Samples;

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

            services.AddTransient<ISample, SpacesSimpleSample>();
            services.AddTransient<ISample, ToolSample>();
            services.AddTransient<ISample, SpacesCopySample>();
            services.AddTransient<ISample, TagsSample>();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetRequiredService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug)
                .AddDebug(LogLevel.Debug);

            var client = serviceProvider.GetRequiredService<IAssemblaClient>();

            var samples = serviceProvider.GetServices<ISample>();

            foreach (var sample in samples)
            {
                Console.WriteLine($"Sample: {sample.GetType().Name}");
                Console.WriteLine();

                try
                {
                    await sample.Execute(client);
                    Console.WriteLine("OK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"FAIL: {ex.Message}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Execution completed. Press ENTER to exit...");
            Console.ReadLine();
        }

        
    }
}