namespace RestLibraries.Infrastructure.ExternalAPI;

using System.Text.Json;
using RestLibraries.Infrastructure.ExternalAPI.Models;
using RestLibraries.Application.Interface;

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
            var rawResult = await _client.GetStringAsync(new Uri(uri));
           
            if(!string.IsNullOrEmpty(rawResult))
            {
                var result = JsonSerializer.Deserialize<Root>(rawResult);
                return result.rates.EURGBP.rate;        
            }
            throw new Exception("Currency hasn't been found");  
        } 
        catch (Exception ex)
        {
            Console.WriteLine($"{nameof(LocalForexAPIHttpClient)} : {ex.Message} / {ex.InnerException.Message} / {ex.InnerException.StackTrace}");
            throw ex;
        }
    }
}