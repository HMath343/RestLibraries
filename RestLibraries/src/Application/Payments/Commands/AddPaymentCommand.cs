namespace RestLibraries.Application.Payments.Commands;

using MediatR;
using RestLibraries.Domain.Entities;

public record AddPaymentCommand : IRequest<Payment>
{
    public string Reference { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; }
    public string TargetCurrency { get; init; }
}