namespace RestLibraries.IntegrationTests;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

public class IntegrationTestWebApplication : WebApplicationFactory<Program>
{
    public IntegrationTestWebApplication()
    {
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {        
        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(EnvironmentsExtension.Integration);
        base.ConfigureWebHost(builder);
    }
}
