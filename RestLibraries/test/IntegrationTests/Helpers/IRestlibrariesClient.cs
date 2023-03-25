namespace RestLibraries.IntegrationTests.Helpers;

using RestLibraries.Domain.Entities;
using RestLibraries.Application.Payments.Commands;
using global::Refit;

public interface IRestlibrariesClient
{
    [Post("/refit/payment")]
    Task<IApiResponse<Payment>> RefitPayment(AddPaymentCommand data);

    [Post("/restsharp/payment")]
    Task<IApiResponse<Payment>> RestsharpPayment(AddPaymentCommand data);

    [Post("/httpclient/payment")]
    Task<IApiResponse<Payment>> HttpClientPayment(AddPaymentCommand data);
}