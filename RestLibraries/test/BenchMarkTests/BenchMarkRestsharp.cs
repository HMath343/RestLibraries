namespace RestLibraries.BenchMarkTests;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestLibraries.Application.Payments;
using BenchmarkDotNet.Attributes;
using System.Threading;
using RestLibraries.Domain.Entities;

[MemoryDiagnoser]
public class BenchMarkRestsharp
{
    public BenchmarkWebApplication _application;
    private AddPaymentRestsharpCommand _command;

    private IRequestHandler<AddPaymentRestsharpCommand, Payment> _handler;

    private const decimal Amount = 10;
    private const string Currency = "EUR";
    private const string TargetCurrency = "GBP";

    [GlobalSetup]
    public void SetUp()
    {
        _application = new BenchmarkWebApplication();
        _handler = _application.Services.GetService<IRequestHandler<AddPaymentRestsharpCommand, Payment>>();

        _command = new Faker<AddPaymentRestsharpCommand>()
            .RuleFor(x => x.Reference, f => f.Random.String2(20))
            .RuleFor(x => x.Amount, Amount)
            .RuleFor(x => x.Currency, Currency)
            .RuleFor(x => x.TargetCurrency, TargetCurrency)
            .Generate();
    }

    [Benchmark]
    public void BenchMark_Restsharp() => BenchMark_RestsharpHandler();

    public async void BenchMark_RestsharpHandler() 
    {
        var result = await _handler.Handle(_command, CancellationToken.None);
        return;
    }
    
    [GlobalCleanup]
    public void Cleanup() => _application.Dispose();

}