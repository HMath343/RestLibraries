namespace RestLibraries.Application.Interface;

public interface IForexAPIHttpClient
{
    public Task<decimal> GetRates(string currency, string targetCurrency);
}