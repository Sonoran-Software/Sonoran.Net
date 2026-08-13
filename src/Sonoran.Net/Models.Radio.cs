namespace Sonoran;

public enum RadioMutableZoneType
{
    Geo,
    Degrade
}

public sealed record RadioZonePoint
{
    public double X { get; init; }
    public double Y { get; init; }
}

public sealed record RadioGeoZoneOptions
{
    public string Name { get; init; } = string.Empty;
    public double MinZ { get; init; }
    public double MaxZ { get; init; }
    public string ZoneType { get; init; } = "geo";
    public IReadOnlyList<int> TransmitChannels { get; init; } = [];
    public IReadOnlyList<int> ScanChannels { get; init; } = [];
    public IReadOnlyList<string> AcePerms { get; init; } = [];
}

public sealed record RadioGeoZone
{
    public IReadOnlyList<RadioZonePoint> Points { get; init; } = [];
    public RadioZonePoint? Center { get; init; }
    public double? Radius { get; init; }
    public RadioGeoZoneOptions Options { get; init; } = new();
}

public sealed record RadioDegradeZoneOptions
{
    public string Name { get; init; } = string.Empty;
    public double MinZ { get; init; }
    public double MaxZ { get; init; }
    public string ZoneType { get; init; } = "degrade";
    public double DegradeStrength { get; init; }
}

public sealed record RadioDegradeZone
{
    public IReadOnlyList<RadioZonePoint> Points { get; init; } = [];
    public RadioZonePoint? Center { get; init; }
    public double? Radius { get; init; }
    public RadioDegradeZoneOptions Options { get; init; } = new();
}

public sealed record SetUserDisplayNameV2Request
{
    public string? CommunityId { get; init; }
    public string AccId { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
}

public sealed record MemberDisplayNameV2Change
{
    public string AccId { get; init; } = string.Empty;
    public string Nickname { get; init; } = string.Empty;
}

public sealed record ProfilePermissionV2Change
{
    public int ProfileId { get; init; }
    public bool CanJoin { get; init; }
}

public sealed record MemberPermissionV2Change
{
    public string AccId { get; init; } = string.Empty;
    public int Perm { get; init; }
    public IReadOnlyList<ProfilePermissionV2Change>? ProfilePerms { get; init; }
}

public sealed record GetMembersV2Query
{
    public string? CommunityId { get; init; }
    public int? Page { get; init; }
    public int? PerPage { get; init; }
    public string? SortBy { get; init; }
    public bool? Descending { get; init; }
    public string? Status { get; init; }
    public string? Search { get; init; }
}

public sealed record GetTransmissionsV2Query
{
    public string? CommunityId { get; init; }
    public int? Page { get; init; }
    public int? PerPage { get; init; }
}

public sealed record SetServerIpV2Request
{
    public string? CommunityId { get; init; }
    public int ServerPort { get; init; }
    public string? OverridePushUrl { get; init; }
    public string? PushUrl { get; init; }
    public string? Nickname { get; init; }
}

public sealed record PlayToneV2Request
{
    public string? CommunityId { get; init; }
    public IReadOnlyList<object> Tones { get; init; } = [];
    public IReadOnlyList<object> PlayTo { get; init; } = [];
}
