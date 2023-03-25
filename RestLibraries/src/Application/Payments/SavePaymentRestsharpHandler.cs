
namespace RestLibraries.Application.Payments;

using MediatR;

using RestLibraries.Application.Interface;
using RestLibraries.Domain.Entities;
using RestLibraries.Application.Payments.Commands;

public record AddPaymentRestsharpCommand : AddPaymentCommand {}

public class SavePaymentRateRestsharpHandler : IRequestHandler<AddPaymentRestsharpCommand, Payment> 
{
    private readonly IForexAPIRestsharp _forexAPIRestsharp;

    public SavePaymentRateRestsharpHandler(IForexAPIRestsharp forexAPIRestsharp)
    {
        _forexAPIRestsharp = forexAPIRestsharp;
    }

    public async Task<Payment> Handle(AddPaymentRestsharpCommand command, CancellationToken cancellationToken)
    {
        decimal rate = 1;
        if(command.Currency != command.TargetCurrency)
        {
            rate = await _forexAPIRestsharp.GetRates(command.Currency, command.TargetCurrency);
        }

        return new Payment()
        {
            Reference = command.Reference,
            Amount = command.Amount,
            Currency = command.Currency,
            TargetCurrency = command.TargetCurrency,
            Rate = rate
        };
    }
}