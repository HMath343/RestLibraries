namespace RestLibraries.Infrastructure.ExternalAPI;

using Microsoft.Extensions.Options;
using RestLibraries.Application.Interface;
using RestLibraries.Infrastructure.ExternalAPI.Models;

using RestSharp;

public class LocalForexAPIRestsharpClient : IForexAPIRestsharp
{
    //private readonly RestClient _client;

    private readonly ISingletonRestsharpClient _client; 

    public LocalForexAPIRestsharpClient(ISingletonRestsharpClient client)
    {
        _client = client;
    }
    
    public async Task<decimal> GetRates(string currency, string targetCurrency)
    {
        try
        {
            RestRequest request = new RestRequest($"rates?currencyA={currency}&currencyB={targetCurrency}");
            var response = await _client.ExecuteAsync<Root>(request);
            if(response.IsSuccessful)
            {
                return response.Data.rates.EURGBP.rate;
            }
            throw response.ErrorException;
        } 
        catch (Exception ex)
        {
            Console.WriteLine($"{nameof(LocalForexAPIRestsharpClient)} : {ex.Message} / {ex.InnerException.Message} / {ex.InnerException.StackTrace}");
            throw ex;
        }
    }
}