namespace BenchMark.RestLibraries.Runner;

using BenchMark.RestLibraries.Runner.Models;

public class FilmPayload
{

    public int count { get; set; }
    public int? next { get; set; }
    public int? previous { get; set; }
    public List<Film> results { get; set; }
    
}