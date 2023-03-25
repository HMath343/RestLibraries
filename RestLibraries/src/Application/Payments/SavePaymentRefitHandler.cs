
namespace RestLibraries.Application.Payments;

using MediatR;

using RestLibraries.Application.Interface;
using RestLibraries.Domain.Entities;
using RestLibraries.Application.Payments.Commands;

public record AddPaymentRefitCommand : AddPaymentCommand {}

public class SavePaymentRateRefitHandler : IRequestHandler<AddPaymentRefitCommand, Payment> 
{
    private readonly IForexAPIRefit _forexAPIRefit;

    public SavePaymentRateRefitHandler(IForexAPIRefit forexAPIRefit)
    {
        _forexAPIRefit =  forexAPIRefit;
    }

    public async Task<Payment> Handle(AddPaymentRefitCommand command, CancellationToken cancellationToken)
    {
        decimal rate = 1;
        if(command.Currency != command.TargetCurrency)
        {
            rate = await _forexAPIRefit.GetRates(command.Currency, command.TargetCurrency);
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