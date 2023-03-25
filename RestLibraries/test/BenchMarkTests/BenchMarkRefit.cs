namespace RestLibraries.BenchMarkTests;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestLibraries.Application.Payments;
using BenchmarkDotNet.Attributes;
using System.Threading;
using RestLibraries.Domain.Entities;

[MemoryDiagnoser]
public class BenchMarkRefit
{
    public BenchmarkWebApplication _application;
    private AddPaymentRefitCommand _command;
    private IRequestHandler<AddPaymentRefitCommand, Payment> _handler;
    private const decimal Amount = 10;
    private const string Currency = "EUR";
    private const string TargetCurrency = "GBP";

    [GlobalSetup]
    public void SetUp()
    {
        _application = new BenchmarkWebApplication();

        _command = new Faker<AddPaymentRefitCommand>()
            .RuleFor(x => x.Reference, f => f.Random.String2(20))
            .RuleFor(x => x.Amount, Amount)
            .RuleFor(x => x.Currency, Currency)
            .RuleFor(x => x.TargetCurrency, TargetCurrency)
            .Generate();

        _handler = _application.Services.GetService<IRequestHandler<AddPaymentRefitCommand, Payment>>();
    }

    [Benchmark]
    public void BenchMark_Refit() => BenchMark_RefitHandler();

    public async void BenchMark_RefitHandler() 
    {
        var result = await _handler.Handle(_command, CancellationToken.None);
        return;
    }
    
    [GlobalCleanup]
    public void Cleanup() => _application.Dispose();

}