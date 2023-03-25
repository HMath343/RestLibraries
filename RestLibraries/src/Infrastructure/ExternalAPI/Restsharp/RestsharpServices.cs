namespace RestLibraries.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using RestLibraries.Infrastructure.ExternalAPI;
using RestLibraries.Application.Interface;

public static class RestsharpServices
{
    public static IServiceCollection AddRestsharpServices(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new APIEndpointsOptions();
        configuration.GetSection(APIEndpointsOptions.APIEndpointsOptionsName)
            .Bind(options);
        services.Configure<APIEndpointsOptions>(configuration.GetSection(APIEndpointsOptions.APIEndpointsOptionsName));

        services.AddTransient<IForexAPIRestsharp, LocalForexAPIRestsharpClient>();
            
        return services;
    }
}