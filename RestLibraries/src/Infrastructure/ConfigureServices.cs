namespace RestLibraries.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitServices(configuration);
        services.AddRestsharpServices(configuration);
        services.AddHttpClientServices(configuration);
        
        return services;
    }
}