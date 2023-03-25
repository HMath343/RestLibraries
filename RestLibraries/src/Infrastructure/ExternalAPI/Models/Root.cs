namespace RestLibraries.Infrastructure.ExternalAPI.Models;

public record Root
{
    public Rates rates { get; set; }
    public int code { get; set; }
}