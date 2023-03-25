namespace RestLibraries.BenchMarkTests;

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

[Collection("Performance")]
public class BenchMarkRestsharpTests
{
    [Fact]
    [Trait("Category", "Restsharp")]
    public void Benchmark()
    {
        BenchmarkRunner.Run<BenchMarkRestsharp>(ManualConfig
            .Create(DefaultConfig.Instance)
            .AddJob(Job.ShortRun));
    }
}