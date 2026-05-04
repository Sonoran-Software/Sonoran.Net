namespace Sonoran;

public sealed partial class SonoranClient
{
    public Task<SonoranResponse> getCommunityChannelsV2(int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Get, $"v2/servers/{resolvedServerId}/channels", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getConnectedUsersV2(int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Get, $"v2/servers/{resolvedServerId}/connected-users", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getConnectedUserV2(int roomId, string identity, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        AssertPositiveInteger(roomId, nameof(roomId));
        return RequestAsync(HttpMethod.Get, $"v2/servers/{resolvedServerId}/rooms/{roomId}/users/{EncodePathSegment(identity)}", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setUserChannelsV2(int roomId, string identity, object? options = null, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        AssertPositiveInteger(roomId, nameof(roomId));
        return RequestAsync(PatchMethod, $"v2/servers/{resolvedServerId}/rooms/{roomId}/users/{EncodePathSegment(identity)}/channels", body: options ?? new { }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setUserDisplayNameV2(SetUserDisplayNameV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(PatchMethod, $"v2/servers/{resolvedServerId}/users/display-name", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> approveMembersV2(IReadOnlyList<string> accIds, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Post, $"v2/servers/{resolvedServerId}/members/approve", body: new { accIds }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> kickMembersV2(IReadOnlyList<string> accIds, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Post, $"v2/servers/{resolvedServerId}/members/kick", body: new { accIds }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> banMembersV2(IReadOnlyList<string> accIds, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Post, $"v2/servers/{resolvedServerId}/members/ban", body: new { accIds }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setMemberDisplayNamesV2(IReadOnlyList<MemberDisplayNameV2Change> accNicknames, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(PatchMethod, $"v2/servers/{resolvedServerId}/members/display-names", body: new { accNicknames }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setMemberPermissionsV2(IReadOnlyList<MemberPermissionV2Change> userPerms, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(PatchMethod, $"v2/servers/{resolvedServerId}/members/permissions", body: new { userPerms }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getServerSubscriptionFromIpV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/server-subscriptions/by-ip", authenticated: false, cancellationToken: cancellationToken);

    public Task<SonoranResponse> setServerIpV2(SetServerIpV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Post, $"v2/servers/{resolvedServerId}/server-ip", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setInGameSpeakerLocationsV2(IReadOnlyList<object?> locations, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Put, $"v2/servers/{resolvedServerId}/speakers", body: new { locations }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> playToneV2(PlayToneV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Post, $"v2/servers/{resolvedServerId}/tones/play", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }
}
