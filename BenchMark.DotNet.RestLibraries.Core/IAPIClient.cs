namespace BenchMark.RestLibraries.Runner;

using Refit;
using BenchMark.RestLibraries.Runner.Models;

public interface IAPIClient
{
    [Get("/films")]
    Task<FilmPayload> GetFilms();

    [Get("/films/{id}")]
    Task<Film> GetFilm(int id);
}
