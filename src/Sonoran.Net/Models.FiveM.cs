using Newtonsoft.Json.Linq;

namespace Sonoran;

public sealed record FiveMConfigurationV2
{
    public long Revision { get; init; }
    public int SchemaVersion { get; init; }
    public string TemplateRevision { get; init; } = string.Empty;
    public int ServerId { get; init; }
    public FiveMConfigurationValuesV2 Values { get; init; } = new();
    public long? AppliedRevision { get; init; }
    public DateTimeOffset? AppliedAt { get; init; }
}

public sealed record FiveMConfigurationValuesV2
{
    public JObject Core { get; init; } = new();
    public JObject Plugins { get; init; } = new();
}

public sealed record FiveMConfigurationPushDataV2
{
    public int ServerId { get; init; }
    public long Revision { get; init; }
    public int SchemaVersion { get; init; }
    public string TemplateRevision { get; init; } = string.Empty;
}
