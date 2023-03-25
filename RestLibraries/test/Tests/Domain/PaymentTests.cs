namespace RestLibraries.Tests.Domain.Entities;

using RestLibraries.Domain.Entities;
using FluentAssertions;

public class PaymentTests
{
    [Fact]
    public void Ctor_SetsExpectedProperties()
    {
        var payment = new Payment();
        payment.PaymentDate.Should().BeIn(DateTimeKind.Utc);
    }

    [Fact]
    public void GetAmount_Return_CorrectValue()
    {
        var FakePayment = new Faker<Payment>()
            .RuleFor(p => p.Amount, 10)
            .RuleFor(p => p.Rate, 0.8M);
        
        var payment = FakePayment.Generate();
        var amount = payment.GetAmount();
        
        amount.Should().Be(8.0M);
    }
}