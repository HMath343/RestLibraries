namespace RestLibraries.BenchMarkTests;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

public class BenchmarkWebApplication : WebApplicationFactory<Program>
{
    public BenchmarkWebApplication()
    {
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(EnvironmentsExtension.Benchmark);
        base.ConfigureWebHost(builder);
    }
}