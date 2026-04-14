using System.Text.Json.Nodes;

namespace Sonoran;

public sealed class SonoranResponse
{
    public bool success { get; init; }

    public JsonNode? data { get; init; }

    public object? reason { get; init; }
}
