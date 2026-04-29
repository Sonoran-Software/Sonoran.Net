using System.Text.Json.Serialization;

namespace Sonoran;

public sealed record GetLoginPageV2Query
{
    public string? Url { get; init; }
    public string? CommunityId { get; init; }
}

public sealed record ApplyPermissionKeyV2Request
{
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonPropertyName("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
    public string PermissionKey { get; init; } = string.Empty;
}

public sealed record BanUserV2Request
{
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonPropertyName("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
    public bool? IsBan { get; init; }
    public bool? IsKick { get; init; }
}

public sealed record PenalCodeV2
{
    public string Code { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string? BondType { get; init; }
    public string? JailTime { get; init; }
    public decimal? BondAmount { get; init; }
}

public sealed record SetApiIdsV2Request
{
    public string? Username { get; init; }
    public string? AccountUuid { get; init; }
    public IReadOnlyList<string> ApiIds { get; init; } = [];
    public string? SessionId { get; init; }
    public bool? PushNew { get; init; }
}

public sealed record CreateRecordV2Request
{
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonPropertyName("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
    public string? User { get; init; }
    public bool? UseDictionary { get; init; }
    public int? RecordTypeId { get; init; }
    public Dictionary<string, string>? ReplaceValues { get; init; }
    public Dictionary<string, object?>? Record { get; init; }
}

public sealed record UpdateRecordV2Request
{
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonPropertyName("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
    public string? User { get; init; }
    public bool? UseDictionary { get; init; }
    public int? RecordTypeId { get; init; }
    public Dictionary<string, string>? ReplaceValues { get; init; }
    public Dictionary<string, object?>? Record { get; init; }
}

public sealed record SendRecordDraftV2Request
{
    public int RecordTypeId { get; init; }
    public Dictionary<string, string> ReplaceValues { get; init; } = [];
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonPropertyName("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
}

public sealed record LookupV2Request
{
    public string? NotifyAccountUuid { get; init; }
    [JsonIgnore]
    public string? NotifyCommunityUserId { get; init; }
    [JsonIgnore]
    public string? NotifyApiId { get; init; }
    [JsonPropertyName("notifyCommunityUserId")]
    public string? SerializedNotifyCommunityUserId => NotifyCommunityUserId ?? NotifyApiId;
    public long? NotifyRoblox { get; init; }
    public IReadOnlyList<int> Types { get; init; } = [];
    public string? First { get; init; }
    public string? Last { get; init; }
    public string? Mi { get; init; }
    public string? Plate { get; init; }
    public bool? Partial { get; init; }
    public int? Limit { get; init; }
    public int? Offset { get; init; }
}

public sealed record LookupByValueV2Request
{
    public string SearchType { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
    public IReadOnlyList<int> Types { get; init; } = [];
    public bool? Partial { get; init; }
    public int? Limit { get; init; }
    public int? Offset { get; init; }
    public string? NotifyAccountUuid { get; init; }
    [JsonIgnore]
    public string? NotifyCommunityUserId { get; init; }
    [JsonIgnore]
    public string? NotifyApiId { get; init; }
    [JsonPropertyName("notifyCommunityUserId")]
    public string? SerializedNotifyCommunityUserId => NotifyCommunityUserId ?? NotifyApiId;
    public long? NotifyRoblox { get; init; }
}

public sealed record LookupCustomV2Request
{
    public string Map { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
    public IReadOnlyList<int> Types { get; init; } = [];
    public bool? Partial { get; init; }
    public int? Limit { get; init; }
    public int? Offset { get; init; }
}

public sealed record GetAccountV2Query
{
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonPropertyName("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
    public string? Username { get; init; }
}

public sealed record GetAccountsV2Query
{
    public int? Limit { get; init; }
    public int? Offset { get; init; }
    public string? Status { get; init; }
    public string? Username { get; init; }
}

public sealed record CommunityLinkV2Request
{
    public string CommunityUserId { get; init; } = string.Empty;
}

public sealed record SetAccountPermissionsV2Request
{
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonPropertyName("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
    public IReadOnlyList<string>? Add { get; init; }
    public IReadOnlyList<string>? Remove { get; init; }
}

public sealed record CadServerV2
{
    public int Id { get; init; }
    public Dictionary<string, object?>? Config { get; init; }
}

public sealed record PostalV2
{
    public string Postal { get; init; } = string.Empty;
    public double? X { get; init; }
    public double? Y { get; init; }
    public bool? Hidden { get; init; }
    public bool? OneWay { get; init; }
}

public sealed record SendPhotoV2Request
{
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonPropertyName("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
    public string Url { get; init; } = string.Empty;
}
