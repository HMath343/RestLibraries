namespace RestLibraries.IntegrationTests.API;

using RestLibraries.Application.Payments.Commands;

public class SavePaymentRestsharpTests : IClassFixture<IntegrationTestFixture>
{
    public IntegrationTestFixture _fixture;
    private const decimal Amount = 10;
    private const string Currency = "EUR";
    private const string TargetCurrency = "GBP";

    public SavePaymentRestsharpTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void SavePaymentRestsharp_ReturnPayment_WhenCurrencyIsNotEqual()
    {
        var paymentData = new Faker<AddPaymentCommand>()
            .RuleFor(x => x.Reference, f => f.Random.String2(20))
            .RuleFor(x => x.Amount, Amount)
            .RuleFor(x => x.Currency, Currency)
            .RuleFor(x => x.TargetCurrency, TargetCurrency)
            .Generate();

        var response = await _fixture.RestLibrariesClient.RestsharpPayment(paymentData);

        response.IsSuccessStatusCode.Should().BeTrue();
        
        var payment = response.Content;
        payment.Should().NotBeNull();
        payment.Reference.Should().Be(paymentData.Reference);
        payment.Amount.Should().Be(paymentData.Amount);
        payment.Currency.Should().Be(paymentData.Currency);
        payment.TargetCurrency.Should().Be(paymentData.TargetCurrency);
        payment.Rate.Should().NotBe(1);
    }
}