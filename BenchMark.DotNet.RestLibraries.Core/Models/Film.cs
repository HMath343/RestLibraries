namespace BenchMark.RestLibraries.Runner.Models;

using System.Text.Json.Serialization;

public class Film
{
    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>The title.</value>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the episode identifier.
    /// </summary>
    /// <value>The episode identifier.</value>
    [JsonPropertyName("episode_id")]
    public int EpisodeId { get; set; }

    /// <summary>
    /// Gets or sets the opening crawl.
    /// </summary>
    /// <value>The opening crawl.</value>
    [JsonPropertyName("opening_crawl")]
    public string OpeningCrawl { get; set; }

    /// <summary>
    /// Gets or sets the director.
    /// </summary>
    /// <value>The director.</value>
    [JsonPropertyName("director")]
    public string Director { get; set; }

    /// <summary>
    /// Gets or sets the producer.
    /// </summary>
    /// <value>The producer.</value>
    [JsonPropertyName("producer")]
    public string Producer { get; set; }

    /// <summary>
    /// Gets or sets the release date.
    /// </summary>
    /// <value>The release date.</value>
    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }

    ///<summary>
    /// Gets or sets the species.
    /// </summary>
    /// <value>The species.</value>
    public ICollection<string> Species { get; set; }

    /// <summary>
    /// Gets or sets the starships URLs.
    /// </summary>
    /// <value>The starships.</value>
    public ICollection<string> Starships { get; set; }

    /// <summary>
    /// Gets or sets the vehicles URLs.
    /// </summary>
    /// <value>The vehicles.</value>
    public ICollection<string> Vehicles { get; set; }

    /// <summary>
    /// Gets or sets the characters URLs.
    /// </summary>
    /// <value>The characters.</value>
    public ICollection<string> Characters { get; set; }

    /// <summary>
    /// Gets or sets the planets URLs.
    /// </summary>
    /// <value>The planets.</value>
    public ICollection<string> Planets { get; set; }

}