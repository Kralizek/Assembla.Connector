using System;
using System.Collections.Generic;
using System.Text;
using Kralizek.Assembla.Connector;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kralizek.Assembla
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAssemblaWithUserAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AssemblaAuthenticatorOptions>(configuration);

            services.AddSingleton<AssemblaAuthenticator, UserAuthenticator>();

            services.AddSingleton<IAssemblaClient, HttpAssemblaClient>();

            return services;
        }
    }
}
