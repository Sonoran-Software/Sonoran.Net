namespace Sonoran;

public sealed class SonoranClientOptions
{
    public string? apiKey { get; init; }

    public string? communityId { get; init; }

    public string apiUrl { get; init; } = "https://api.sonorancad.com";

    public int defaultServerId { get; init; } = 1;

    public TimeSpan timeout { get; init; } = TimeSpan.FromSeconds(30);

    public IReadOnlyDictionary<string, string>? headers { get; init; }
}
