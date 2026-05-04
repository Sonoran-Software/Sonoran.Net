using Newtonsoft.Json;

namespace Sonoran;

public sealed record GetCharactersV2Query
{
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonProperty("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
}

public sealed record SetSelectedCharacterV2Request
{
    public string CharacterId { get; init; } = string.Empty;
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonProperty("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
}

public sealed record GetCharacterLinksV2Query
{
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonProperty("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
}

public sealed record CharacterLinkTargetV2Request
{
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonProperty("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
}
