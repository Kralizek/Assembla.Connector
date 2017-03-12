using System;
using System.Net.Http;
using System.Threading.Tasks;
using Kralizek.Assembla;
using Kralizek.Assembla.Connector;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();

            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddOptions();

            services.AddAssemblaWithUserAuthentication(configuration.GetSection("Assembla"));

            services
                .AddTransient<ISample, SpacesSimpleSample>()
                .AddTransient<ISample, ToolSample>()
                .AddTransient<ISample, SpacesCopySample>()
                .AddTransient<ISample, TagsSample>()
                .AddTransient<ISample, TicketSample>()
                .AddTransient<ISample, TicketListSample>()
                .AddTransient<ISample, CustomFieldSample>()
                .AddTransient<ISample, MilestoneSample>()
                .AddTransient<ISample, TicketStatusSample>()
                .AddTransient<ISample, TicketCommentSample>()
                .AddTransient<ISample, TicketAssociationSample>()
                .AddTransient<ISample, FileSample>()
                .AddTransient<ISample, UserSample>()
            ;

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