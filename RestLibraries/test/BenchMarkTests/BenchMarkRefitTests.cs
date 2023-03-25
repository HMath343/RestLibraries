namespace RestLibraries.BenchMarkTests;

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

[Collection("Performance")]
public class BenchMarkRefitTests
{
    [Fact]
    [Trait("Category", "Refit")]
    public void Benchmark()
    {
        BenchmarkRunner.Run<BenchMarkRefit>(ManualConfig
            .Create(DefaultConfig.Instance)
            .AddJob(Job.ShortRun));
    }
}