using Newtonsoft.Json;

namespace Sonoran;

public sealed record GetUnitsV2Query
{
    public int? ServerId { get; init; }
    public bool? IncludeOffline { get; init; }
    public bool? OnlyUnits { get; init; }
    public int? Limit { get; init; }
    public int? Offset { get; init; }
}

public sealed record GetCallsV2Query
{
    public int? ServerId { get; init; }
    public int? ClosedLimit { get; init; }
    public int? ClosedOffset { get; init; }
    public string? Type { get; init; }
}

public sealed record UnitLocationUpdateV2
{
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonProperty("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
    public string? Location { get; init; }
    public double? X { get; init; }
    public double? Y { get; init; }
    public double? Z { get; init; }
    public float? Heading { get; init; }
}

public sealed record UpdateUnitLocationsV2Request
{
    public int? ServerId { get; init; }
    public IReadOnlyList<UnitLocationUpdateV2> Updates { get; init; } = [];
}

public sealed record SetUnitPanicV2Request
{
    public int? ServerId { get; init; }
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonProperty("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    [JsonIgnore]
    public IReadOnlyList<string>? CommunityUserIds { get; init; }
    [JsonIgnore]
    public IReadOnlyList<string>? ApiIds { get; init; }
    [JsonProperty("communityUserIds")]
    public IReadOnlyList<string>? SerializedCommunityUserIds => CommunityUserIds ?? ApiIds;
    public long? Roblox { get; init; }
    public IReadOnlyList<int>? IdentIds { get; init; }
    public bool IsPanic { get; init; }
}

public sealed record SetUnitStatusV2Request
{
    public int? ServerId { get; init; }
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonProperty("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    [JsonIgnore]
    public IReadOnlyList<string>? CommunityUserIds { get; init; }
    [JsonIgnore]
    public IReadOnlyList<string>? ApiIds { get; init; }
    [JsonProperty("communityUserIds")]
    public IReadOnlyList<string>? SerializedCommunityUserIds => CommunityUserIds ?? ApiIds;
    public long? Roblox { get; init; }
    public IReadOnlyList<int>? IdentIds { get; init; }
    public int Status { get; init; }
}

public sealed record KickUnitV2Request
{
    public int? ServerId { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonProperty("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    public long? Roblox { get; init; }
    public string Reason { get; init; } = string.Empty;
}

public sealed record GetAccountUnitsV2Query
{
    public int? ServerId { get; init; }
    public string AccountUuid { get; init; } = string.Empty;
    public bool? OnlyOnline { get; init; }
    public bool? OnlyUnits { get; init; }
    public int? Limit { get; init; }
    public int? Offset { get; init; }
}

public sealed record IdentifierV2Request
{
    public string? UnitNum { get; init; }
    public string? Department { get; init; }
    public string? Subdivision { get; init; }
    public string? Callsign { get; init; }
    public string? Title { get; init; }
    public string? Type { get; init; }
    public bool? IsPrimary { get; init; }
    public Dictionary<string, object?>? Data { get; init; }
}

public sealed record AddIdentifiersToGroupV2Request
{
    public int? ServerId { get; init; }
    public string GroupName { get; init; } = string.Empty;
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonProperty("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    [JsonIgnore]
    public IReadOnlyList<string>? CommunityUserIds { get; init; }
    [JsonIgnore]
    public IReadOnlyList<string>? ApiIds { get; init; }
    [JsonProperty("communityUserIds")]
    public IReadOnlyList<string>? SerializedCommunityUserIds => CommunityUserIds ?? ApiIds;
    public long? Roblox { get; init; }
    public IReadOnlyList<int>? IdentIds { get; init; }
}

public sealed record CreateEmergencyCallV2Request
{
    public int? ServerId { get; init; }
    public bool IsEmergency { get; init; }
    public string Caller { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Dictionary<string, string>? MetaData { get; init; }
    public int? DeleteAfterMinutes { get; init; }
}

public sealed record DispatchCallNoteV2
{
    [JsonProperty("time")]
    public string? Time { get; init; }

    [JsonProperty("label")]
    public string? Label { get; init; }

    [JsonProperty("type")]
    public string? Type { get; init; }

    [JsonProperty("content")]
    public string? Content { get; init; }
}

public sealed record CreateDispatchCallV2Request
{
    public int? ServerId { get; init; }
    public int Origin { get; init; }
    public int Status { get; init; }
    public int Priority { get; init; }
    public string Block { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Postal { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IReadOnlyList<DispatchCallNoteV2> Notes { get; init; } = [];
    [JsonIgnore]
    public IReadOnlyList<string>? CommunityUserIds { get; init; }
    public IReadOnlyList<string>? Accounts { get; init; }
    [JsonIgnore]
    public IReadOnlyList<string>? ApiIds { get; init; }
    [JsonProperty("communityUserIds")]
    public IReadOnlyList<string>? SerializedCommunityUserIds => CommunityUserIds ?? ApiIds;
    public long? Roblox { get; init; }
    public Dictionary<string, string>? MetaData { get; init; }
    public int? DeleteAfterMinutes { get; init; }
}

public sealed record UpdateDispatchCallV2Request
{
    public int? ServerId { get; init; }
    public int? Origin { get; init; }
    public int? Status { get; init; }
    public int? Priority { get; init; }
    public string? Block { get; init; }
    public string? Address { get; init; }
    public string? Postal { get; init; }
    public string? Title { get; init; }
    public string? Code { get; init; }
    public string? Description { get; init; }
    public int? Primary { get; init; }
    public bool? TrackPrimary { get; init; }
    public Dictionary<string, string>? MetaData { get; init; }
}

public sealed record DispatchAttachmentV2Request
{
    public int? ServerId { get; init; }
    public string? AccountUuid { get; init; }
    [JsonIgnore]
    public string? CommunityUserId { get; init; }
    [JsonIgnore]
    public string? ApiId { get; init; }
    [JsonProperty("communityUserId")]
    public string? SerializedCommunityUserId => CommunityUserId ?? ApiId;
    [JsonIgnore]
    public IReadOnlyList<string>? CommunityUserIds { get; init; }
    [JsonIgnore]
    public IReadOnlyList<string>? ApiIds { get; init; }
    [JsonProperty("communityUserIds")]
    public IReadOnlyList<string>? SerializedCommunityUserIds => CommunityUserIds ?? ApiIds;
    public long? Roblox { get; init; }
    public IReadOnlyList<string>? Accounts { get; init; }
    public IReadOnlyList<int>? IdentIds { get; init; }
}

public sealed record AddDispatchNoteV2Request
{
    public int? ServerId { get; init; }
    public string Note { get; init; } = string.Empty;
    public string? NoteType { get; init; }
    public string? Label { get; init; }
}

public sealed record UpdateStreetSignsV2Request
{
    public int? ServerId { get; init; }
    public IReadOnlyList<int> Ids { get; init; } = [];
    public string? Text1 { get; init; }
    public string? Text2 { get; init; }
    public string? Text3 { get; init; }
}

public sealed record AvailableCalloutV2
{
    [JsonProperty("id")]
    public string? Id { get; init; }

    [JsonProperty("data")]
    public AvailableCalloutDataV2? Data { get; init; }
}

public sealed record AvailableCalloutDataV2
{
    [JsonProperty("PedActionOnNoActionFound")]
    public string? PedActionOnNoActionFound { get; init; }

    [JsonProperty("PedActionMinimumTimeoutInMs")]
    public int? PedActionMinimumTimeoutInMs { get; init; }

    [JsonProperty("PedChanceToFleeFromPlayer")]
    public int? PedChanceToFleeFromPlayer { get; init; }

    [JsonProperty("PedChanceToObtainWeapons")]
    public int? PedChanceToObtainWeapons { get; init; }

    [JsonProperty("CalloutName")]
    public string? CalloutName { get; init; }

    [JsonProperty("CalloutDescriptions")]
    public IReadOnlyList<string>? CalloutDescriptions { get; init; }

    [JsonProperty("PedChanceToAttackPlayer")]
    public int? PedChanceToAttackPlayer { get; init; }

    [JsonProperty("PedActionMaximumTimeoutInMs")]
    public int? PedActionMaximumTimeoutInMs { get; init; }

    [JsonProperty("Enabled")]
    public bool? Enabled { get; init; }

    [JsonProperty("CalloutLocations")]
    public IReadOnlyList<AvailableCalloutLocationV2>? CalloutLocations { get; init; }

    [JsonProperty("PedChanceToSurrender")]
    public int? PedChanceToSurrender { get; init; }

    [JsonProperty("PedWeaponData")]
    public IReadOnlyList<string>? PedWeaponData { get; init; }

    [JsonProperty("CalloutUnitsRequired")]
    public AvailableCalloutUnitsRequiredV2? CalloutUnitsRequired { get; init; }
}

public sealed record AvailableCalloutLocationV2
{
    [JsonProperty("x")]
    public double X { get; init; }

    [JsonProperty("y")]
    public double Y { get; init; }

    [JsonProperty("z")]
    public double Z { get; init; }
}

public sealed record AvailableCalloutUnitsRequiredV2
{
    [JsonProperty("towRequired")]
    public bool? TowRequired { get; init; }

    [JsonProperty("fireRequired")]
    public bool? FireRequired { get; init; }

    [JsonProperty("description")]
    public string? Description { get; init; }

    [JsonProperty("policeRequired")]
    public bool? PoliceRequired { get; init; }

    [JsonProperty("ambulanceRequired")]
    public bool? AmbulanceRequired { get; init; }
}

public sealed record PagerNatureWordV2
{
    public string Label { get; init; } = string.Empty;
    public int Weight { get; init; }
}

public sealed record PagerNodeV2
{
    public string Label { get; init; } = string.Empty;
    public string? Department { get; init; }
    public string? Subdivision { get; init; }
    public IReadOnlyList<string>? Tones { get; init; }
}

public sealed record SetPagerConfigV2Request
{
    public int? ServerId { get; init; }
    public IReadOnlyList<PagerNatureWordV2> NatureWords { get; init; } = [];
    public int MaxAddresses { get; init; }
    public int MaxBodyLength { get; init; }
    public IReadOnlyList<PagerNodeV2>? Nodes { get; init; }
}

public sealed record StationConfigV2
{
    public IReadOnlyList<StationLocationV2>? Locations { get; init; }
    public IReadOnlyList<string>? Tones { get; init; }
    [JsonProperty("unitColors")]
    public IReadOnlyList<string>? UnitColors { get; init; }
}

public sealed record StationLocationV2
{
    public string Name { get; init; } = string.Empty;
    public BlipCoordinatesV2 Coordinates { get; init; } = new();
    public IReadOnlyList<string>? Doors { get; init; }
    public string? Icon { get; init; }
}

public sealed record BlipCoordinatesV2
{
    public double X { get; init; }
    public double Y { get; init; }
    public double? Z { get; init; }
    public double? W { get; init; }
}

public sealed record BlipDisplayDataV2
{
    public string? Title { get; init; }
    public string? Text { get; init; }
}

public sealed record CreateBlipV2Request
{
    public int? ServerId { get; init; }
    public BlipCoordinatesV2 Coordinates { get; init; } = new();
    public string SubType { get; init; } = string.Empty;
    public string? Icon { get; init; }
    public string? Color { get; init; }
    public string? Tooltip { get; init; }
    public IReadOnlyList<BlipDisplayDataV2>? Data { get; init; }
    public double? Radius { get; init; }
}

public sealed record UpdateBlipV2Request
{
    public int? ServerId { get; init; }
    public BlipCoordinatesV2? Coordinates { get; init; }
    public string? SubType { get; init; }
    public string? Icon { get; init; }
    public string? Color { get; init; }
    public string? Tooltip { get; init; }
    public IReadOnlyList<BlipDisplayDataV2>? Data { get; init; }
    public double? Radius { get; init; }
}
