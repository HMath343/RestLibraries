namespace RestLibraries.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Refit;
using RestLibraries.Infrastructure.ExternalAPI;
using RestLibraries.Application.Interface;

public static class RefitServices
{
    public static IServiceCollection AddRefitServices(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new APIEndpointsOptions();
        configuration.GetSection(APIEndpointsOptions.APIEndpointsOptionsName)
            .Bind(options);

        services.AddTransient<IForexAPIRefit, LocalForexAPIRefitClient>();
        services.AddSingleton<ISingletonRestsharpClient, SingletonRestsharpClient>();
        services.AddRefitClient<ILocalForexAPIClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(options.LocalForexAPIEndpoint));
            
        return services;
    }
}