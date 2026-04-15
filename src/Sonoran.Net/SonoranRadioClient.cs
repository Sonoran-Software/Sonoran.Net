namespace Sonoran;

public sealed class SonoranRadioClient
{
    private readonly SonoranClient _client;

    internal SonoranRadioClient(SonoranClient client)
    {
        _client = client;
    }

    public Task<SonoranResponse> getCommunityChannelsV2(int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.getCommunityChannelsV2(serverId, cancellationToken);

    public Task<SonoranResponse> getConnectedUsersV2(int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.getConnectedUsersV2(serverId, cancellationToken);

    public Task<SonoranResponse> getConnectedUserV2(int roomId, string identity, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.getConnectedUserV2(roomId, identity, serverId, cancellationToken);

    public Task<SonoranResponse> setUserChannelsV2(int roomId, string identity, object? options = null, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.setUserChannelsV2(roomId, identity, options, serverId, cancellationToken);

    public Task<SonoranResponse> setUserDisplayNameV2(SetUserDisplayNameV2Request request, CancellationToken cancellationToken = default) =>
        _client.setUserDisplayNameV2(request, cancellationToken);

    public Task<SonoranResponse> approveMembersV2(IReadOnlyList<string> accIds, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.approveMembersV2(accIds, serverId, cancellationToken);

    public Task<SonoranResponse> kickMembersV2(IReadOnlyList<string> accIds, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.kickMembersV2(accIds, serverId, cancellationToken);

    public Task<SonoranResponse> banMembersV2(IReadOnlyList<string> accIds, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.banMembersV2(accIds, serverId, cancellationToken);

    public Task<SonoranResponse> setMemberDisplayNamesV2(IReadOnlyList<MemberDisplayNameV2Change> accNicknames, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.setMemberDisplayNamesV2(accNicknames, serverId, cancellationToken);

    public Task<SonoranResponse> setMemberPermissionsV2(IReadOnlyList<MemberPermissionV2Change> userPerms, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.setMemberPermissionsV2(userPerms, serverId, cancellationToken);

    public Task<SonoranResponse> getServerSubscriptionFromIpV2(CancellationToken cancellationToken = default) =>
        _client.getServerSubscriptionFromIpV2(cancellationToken);

    public Task<SonoranResponse> setServerIpV2(SetServerIpV2Request request, CancellationToken cancellationToken = default) =>
        _client.setServerIpV2(request, cancellationToken);

    public Task<SonoranResponse> setInGameSpeakerLocationsV2(IReadOnlyList<object?> locations, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.setInGameSpeakerLocationsV2(locations, serverId, cancellationToken);

    public Task<SonoranResponse> playToneV2(PlayToneV2Request request, CancellationToken cancellationToken = default) =>
        _client.playToneV2(request, cancellationToken);
}
