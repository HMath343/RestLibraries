namespace RestLibraries.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using RestLibraries.Infrastructure.ExternalAPI;
using RestLibraries.Application.Interface;
using System;

public static class HttpClientServices
{
    public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new APIEndpointsOptions();
        configuration.GetSection(APIEndpointsOptions.APIEndpointsOptionsName)
            .Bind(options);

        services.AddHttpClient<IForexAPIHttpClient, LocalForexAPIHttpClient>()
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler()
                {
                    MaxConnectionsPerServer = 100,
                };
            })
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(options.LocalForexAPIEndpoint);
                httpClient.Timeout = TimeSpan.FromSeconds(60);
            });

        return services;
    }
}
