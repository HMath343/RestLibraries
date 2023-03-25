namespace RestLibraries.BenchMarkTests;

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

[Collection("Performance")]
public class BenchMarkHttpClientTests
{
    [Fact]
    [Trait("Category", "HttpClient")]
    public void Benchmark()
    {
        BenchmarkRunner.Run<BenchMarkHttpClient>(ManualConfig
            .Create(DefaultConfig.Instance)
            .AddJob(Job.ShortRun));
    }
}