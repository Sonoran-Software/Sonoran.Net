using Newtonsoft.Json.Linq;

namespace Sonoran;

public sealed class SonoranResponse
{
    public bool success { get; init; }

    public JToken? data { get; init; }

    public object? reason { get; init; }
}
