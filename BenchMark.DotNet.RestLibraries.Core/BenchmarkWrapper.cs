using RestSharp;
using Refit;
using BenchmarkDotNet.Attributes;

using BenchMark.RestLibraries.Runner;
using BenchMark.RestLibraries.Runner.Models;

[MemoryDiagnoser]
public class BenchmarkWrapper
{
    private const int ID = 1;

    private RestClient _restsharpClient { get; set; }
    private IAPIClient _refitClient { get; set; }

    public BenchmarkWrapper()
    {
        _restsharpClient = new RestClient("http://localhost:80");
        _refitClient = RestService.For<IAPIClient>("http://localhost:80");
    }

    [Benchmark]
    public RestClient RestsharpClientInstanciation()
    {
        return new RestClient("http://localhost:80");
    }

    [Benchmark]
    public IAPIClient RefitClientInstanciation()
    {
        return RestService.For<IAPIClient>("http://localhost:80");
    }

    [Benchmark]
    public async Task<List<Film>> RestsharpGetFilms()
    {
        var request = new RestRequest($"films");
        var result = await _restsharpClient.ExecuteGetAsync<FilmPayload>(request);
        return result.Data.results;
    }

    [Benchmark]
    public async Task<List<Film>> RefitGetFilms()
    {
        var root = await _refitClient.GetFilms();
        return root.results;
    }

    [Benchmark]
    public async Task<Film> RestsharpGetFilmById()
    {
        var request = new RestRequest($"films/{ID}");
        var result = await _restsharpClient.ExecuteGetAsync<Film>(request);
        return result.Data;
    }

    [Benchmark]
    public async Task<Film> RefitGetFilmById()
    {
        var film = await _refitClient.GetFilm(ID);
        return film;
    }
}