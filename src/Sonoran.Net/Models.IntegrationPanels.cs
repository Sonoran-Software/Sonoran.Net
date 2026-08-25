using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sonoran;

public sealed record IntegrationPanelDefinitionV2
{
    public int SchemaVersion { get; init; } = 1;
    public string? Key { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Icon { get; init; }
    public IReadOnlyList<string>? Surfaces { get; init; }
    public IReadOnlyList<Dictionary<string, object?>>? Sounds { get; init; }
    public IReadOnlyList<Dictionary<string, object?>> Body { get; init; } = [];

    // Preserve future top-level manifest options without waiting for an SDK release.
    [JsonExtensionData]
    public IDictionary<string, JToken>? AdditionalProperties { get; init; }
}

public sealed record GetIntegrationPanelActionsV2Query
{
    public int? ServerId { get; init; }
    public long? After { get; init; }
    public int? Limit { get; init; }
}

public sealed record AcknowledgeIntegrationPanelActionV2Request
{
    public int? ServerId { get; init; }
    public bool Success { get; init; }
    public string? Message { get; init; }
    public IReadOnlyDictionary<string, object?>? Result { get; init; }
}
