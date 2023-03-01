using BenchMark.RestLibraries.API;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/films", () => APIEndpoint.GetAllFilms());

app.MapGet("/films/{id}", (int id) => APIEndpoint.GetFilmById(id));
app.Run();
