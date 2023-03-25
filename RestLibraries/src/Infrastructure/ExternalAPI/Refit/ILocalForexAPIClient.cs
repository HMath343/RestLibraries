namespace RestLibraries.Infrastructure.ExternalAPI;

using RestLibraries.Infrastructure.ExternalAPI.Models;
using global::Refit;

public interface ILocalForexAPIClient
{
    [Get("/rates")]
    Task<IApiResponse<Root>> GetRates(string currencyA, string currencyB);
}