namespace RestLibraries.Application.Interface;

public interface IForexAPIRestsharp
{
    public Task<decimal> GetRates(string currency, string targetCurrency);
}