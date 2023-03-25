namespace RestLibraries.IntegrationTests;

using Refit;
using RestLibraries.IntegrationTests.Helpers;

public class IntegrationTestFixture : IClassFixture<IntegrationTestWebApplication>
{
    private readonly IntegrationTestWebApplication _webApplication;

    public IRestlibrariesClient RestLibrariesClient;

    public IntegrationTestFixture()
    {
        _webApplication = new IntegrationTestWebApplication();
        RestLibrariesClient = RestService.For<IRestlibrariesClient>(_webApplication.CreateClient());
    }
}