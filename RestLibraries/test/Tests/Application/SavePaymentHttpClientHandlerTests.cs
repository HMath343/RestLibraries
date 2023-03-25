namespace RestLibraries.Tests.Domain.Entities;

using RestLibraries.Application.Payments;
using FluentAssertions;
using RestLibraries.Application.Interface;

public class SavePaymentHttpHandlerTests
{
    private const decimal Amount = 10;
    private const string Currency = "EUR";
    private const string TargetCurrency = "GBP";
    
    [Fact]
    public async void Handle_DoNotCallForexAPI_WhenCurrencyAreEqual()
    {
        var httpClientAPIMock = new Mock<IForexAPIHttpClient>();
        var handler = new SavePaymentHttpClientHandler(httpClientAPIMock.Object);

        var command = new Faker<AddPaymentHttpClientCommand>()
            .RuleFor(x => x.Reference, f => f.Random.String2(20))
            .RuleFor(x => x.Amount, Amount)
            .RuleFor(x => x.Currency, Currency)
            .RuleFor(x => x.TargetCurrency, Currency)
            .Generate();

        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        httpClientAPIMock.Verify(x => x.GetRates(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        result.Reference.Should().Be(command.Reference);
        result.Amount.Should().Be(command.Amount);
        result.Currency.Should().Be(command.Currency);
        result.TargetCurrency.Should().Be(command.TargetCurrency);
        result.Rate.Should().Be(1);
    }

    [Fact]
    public async void Handle_CallForexAPI_WhenCurrencyAreNotEqual()
    {
        var httpClientAPIMock = new Mock<IForexAPIHttpClient>();

        httpClientAPIMock.Setup(x => x.GetRates(Currency,TargetCurrency))
            .ReturnsAsync(0.8M);

        var handler = new SavePaymentHttpClientHandler(httpClientAPIMock.Object);

        var command = new Faker<AddPaymentHttpClientCommand>()
            .RuleFor(x => x.Reference, f => f.Random.String2(20))
            .RuleFor(x => x.Amount, Amount)
            .RuleFor(x => x.Currency, Currency)
            .RuleFor(x => x.TargetCurrency, TargetCurrency)
            .Generate();
            
        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        httpClientAPIMock.Verify(x => x.GetRates(Currency, TargetCurrency), Times.Once);
        result.Reference.Should().Be(command.Reference);
        result.Amount.Should().Be(command.Amount);
        result.Currency.Should().Be(command.Currency);
        result.TargetCurrency.Should().Be(command.TargetCurrency);
        result.Rate.Should().Be(0.8M);
    }
}