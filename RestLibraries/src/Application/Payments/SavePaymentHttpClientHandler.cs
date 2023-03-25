
namespace RestLibraries.Application.Payments;

using MediatR;

using RestLibraries.Application.Interface;
using RestLibraries.Domain.Entities;
using RestLibraries.Application.Payments.Commands;

public record AddPaymentHttpClientCommand : AddPaymentCommand {}

public class SavePaymentHttpClientHandler : IRequestHandler<AddPaymentHttpClientCommand, Payment>
{
    private readonly IForexAPIHttpClient _forexHttpClient;

    public SavePaymentHttpClientHandler(IForexAPIHttpClient forexHttpClient)
    {
        _forexHttpClient = forexHttpClient;
    }

    public async Task<Payment> Handle(AddPaymentHttpClientCommand command, CancellationToken cancellationToken)
    {
        decimal rate = 1;
        if(command.Currency != command.TargetCurrency)
        {
            rate = await _forexHttpClient.GetRates(command.Currency, command.TargetCurrency);
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