using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sonoran;

public sealed partial class SonoranClient
{
    public Task<SonoranResponse> getCommunityChannelsV2(string? communityId = null, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(communityId);
        return RequestAsync(HttpMethod.Get, $"v2/servers/{resolvedCommunityId}/channels", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getConnectedUsersV2(string? communityId = null, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(communityId);
        return RequestAsync(HttpMethod.Get, $"v2/servers/{resolvedCommunityId}/connected-users", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getMembersV2(GetMembersV2Query? query = null, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(query?.CommunityId);
        var queryData = query is null
            ? null
            : new
            {
                query.Page,
                query.PerPage,
                query.SortBy,
                query.Descending,
                query.Status,
                query.Search
            };
        return RequestAsync(HttpMethod.Get, $"v2/servers/{resolvedCommunityId}/members", query: ToQueryDictionary(queryData), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getConnectedUserV2(string identity, string? communityId = null, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(communityId);
        var roomId = ResolveRadioRoomId();
        return RequestAsync(HttpMethod.Get, $"v2/servers/{resolvedCommunityId}/rooms/{roomId}/users/{EncodePathSegment(identity)}", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setUserChannelsV2(string identity, object? options = null, string? communityId = null, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(communityId);
        var roomId = ResolveRadioRoomId();
        return RequestAsync(PatchMethod, $"v2/servers/{resolvedCommunityId}/rooms/{roomId}/users/{EncodePathSegment(identity)}/channels", body: options ?? new { }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setUserDisplayNameV2(SetUserDisplayNameV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(request.CommunityId);
        return RequestAsync(PatchMethod, $"v2/servers/{resolvedCommunityId}/users/display-name", body: WithoutKeys(request, nameof(request.CommunityId)), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> approveMembersV2(IReadOnlyList<string> accIds, string? communityId = null, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(communityId);
        return RequestAsync(HttpMethod.Post, $"v2/servers/{resolvedCommunityId}/members/approve", body: new { accIds }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> kickMembersV2(IReadOnlyList<string> accIds, string? communityId = null, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(communityId);
        return RequestAsync(HttpMethod.Post, $"v2/servers/{resolvedCommunityId}/members/kick", body: new { accIds }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> banMembersV2(IReadOnlyList<string> accIds, string? communityId = null, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(communityId);
        return RequestAsync(HttpMethod.Post, $"v2/servers/{resolvedCommunityId}/members/ban", body: new { accIds }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setMemberDisplayNamesV2(IReadOnlyList<MemberDisplayNameV2Change> accNicknames, string? communityId = null, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(communityId);
        return RequestAsync(PatchMethod, $"v2/servers/{resolvedCommunityId}/members/display-names", body: new { accNicknames }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setMemberPermissionsV2(IReadOnlyList<MemberPermissionV2Change> userPerms, string? communityId = null, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(communityId);
        return RequestAsync(PatchMethod, $"v2/servers/{resolvedCommunityId}/members/permissions", body: new { userPerms }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getServerSubscriptionFromIpV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/server-subscriptions/by-ip", authenticated: false, cancellationToken: cancellationToken);

    public Task<SonoranResponse> setServerIpV2(SetServerIpV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(request.CommunityId);
        var body = WithRadioRoomId(WithoutKeys(request, nameof(request.CommunityId)));
        return RequestAsync(HttpMethod.Post, $"v2/servers/{resolvedCommunityId}/server-ip", body: body, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setInGameSpeakerLocationsV2(IReadOnlyList<object?> locations, string? communityId = null, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(communityId);
        return RequestAsync(HttpMethod.Put, $"v2/servers/{resolvedCommunityId}/speakers", body: new { locations }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> playToneV2(PlayToneV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedCommunityId = ResolveRadioCommunityId(request.CommunityId);
        var body = WithRadioRoomId(WithoutKeys(request, nameof(request.CommunityId)));
        return RequestAsync(HttpMethod.Post, $"v2/servers/{resolvedCommunityId}/tones/play", body: body, cancellationToken: cancellationToken);
    }

    private object WithRadioRoomId(object value)
    {
        var node = new JObject
        {
            ["roomId"] = ResolveRadioRoomId()
        };
        foreach (var property in JObject.FromObject(value, JsonSerializer.Create(SerializerSettings)).Properties())
        {
            node.Add(property.Name, property.Value.DeepClone());
        }

        return node;
    }
}
