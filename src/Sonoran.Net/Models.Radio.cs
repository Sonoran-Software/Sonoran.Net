namespace Sonoran;

public sealed record SetUserDisplayNameV2Request
{
    public int? ServerId { get; init; }
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

public sealed record SetServerIpV2Request
{
    public int? ServerId { get; init; }
    public int RoomId { get; init; }
    public int ServerPort { get; init; }
    public string? OverridePushUrl { get; init; }
    public string? PushUrl { get; init; }
    public string? Nickname { get; init; }
}

public sealed record PlayToneV2Request
{
    public int? ServerId { get; init; }
    public int RoomId { get; init; }
    public IReadOnlyList<object> Tones { get; init; } = [];
    public IReadOnlyList<object> PlayTo { get; init; } = [];
}
