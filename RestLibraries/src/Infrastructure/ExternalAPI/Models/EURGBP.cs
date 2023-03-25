namespace RestLibraries.Infrastructure.ExternalAPI.Models;

public record EURGBP
{
    public decimal rate { get; set; }
    public int timestamp { get; set; }
}