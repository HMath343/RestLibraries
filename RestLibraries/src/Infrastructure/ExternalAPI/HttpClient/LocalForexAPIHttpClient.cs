namespace RestLibraries.Infrastructure.ExternalAPI;

using RestLibraries.Infrastructure.ExternalAPI.Models;
using RestLibraries.Application.Interface;
using System.Net.Http.Json;

public class LocalForexAPIHttpClient : IForexAPIHttpClient
{
    private static HttpClient _client;

    public LocalForexAPIHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<decimal> GetRates(string currency, string targetCurrency)
    {
        try
        {
            var uri = $"{_client.BaseAddress.OriginalString}/rates?currencyA={currency}&currencyB={targetCurrency}";
            var result = await _client.GetFromJsonAsync<Root>(new Uri(uri));
            if(result != null)
                return result.rates.EURGBP.rate;  

            throw new Exception("Currency hasn't been found");  
        } 
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"{nameof(LocalForexAPIHttpClient)} : {ex.Message} / {ex.InnerException.Message} / {ex.InnerException.StackTrace}");
            throw ex;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{nameof(LocalForexAPIHttpClient)} : {ex.Message} / {ex.InnerException.Message} / {ex.InnerException.StackTrace}");
            throw ex;
        }
    }
}