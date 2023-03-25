namespace RestLibraries.Application.Interface;

public interface IForexAPIRefit
{
    public Task<decimal> GetRates(string currency, string targetCurrency);
}