namespace RestLibraries.Infrastructure.ExternalAPI;

using Microsoft.Extensions.Options;
using RestSharp;

public interface ISingletonRestsharpClient
{
    public Task<RestResponse<T>> ExecuteAsync<T>(RestRequest restRequest);
}

public class SingletonRestsharpClient : ISingletonRestsharpClient
{
    private readonly RestClient _client;

    public SingletonRestsharpClient(IOptions<APIEndpointsOptions> options)
    {
        var restClientOptions = new RestClientOptions(options.Value.LocalForexAPIEndpoint);
        _client = new RestClient(restClientOptions, useClientFactory : true);
    }
    
    public async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest restRequest)
    {
        var result = await _client.ExecuteAsync<T>(restRequest);
        return result;
    }
}