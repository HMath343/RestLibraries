namespace RestLibraries.Infrastructure.ExternalAPI;

using RestLibraries.Application.Interface;

public class LocalForexAPIRefitClient : IForexAPIRefit
{
    private readonly ILocalForexAPIClient _LocalForexAPI;

    public LocalForexAPIRefitClient(ILocalForexAPIClient api)
    {
        _LocalForexAPI = api;
    }

    public async Task<decimal> GetRates(string currency, string targetCurrency)
    {
        try
        {
            var result = await _LocalForexAPI.GetRates(currency, targetCurrency);
            if(result.IsSuccessStatusCode)
                return result.Content.rates.EURGBP.rate;   

            throw result.Error;          
        } 
        catch (Exception ex)
        {
            Console.WriteLine($"{nameof(LocalForexAPIRefitClient)} : {ex.Message} / {ex.InnerException.Message} / {ex.InnerException.StackTrace}");
            throw ex;
        }
    }
}