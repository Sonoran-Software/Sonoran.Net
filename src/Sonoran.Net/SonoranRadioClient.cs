namespace Sonoran;

public sealed class SonoranRadioClient
{
    private readonly SonoranClient _client;

    internal SonoranRadioClient(SonoranClient client)
    {
        _client = client;
    }

    public Task<SonoranResponse> getCommunityChannelsV2(string? communityId = null, CancellationToken cancellationToken = default) =>
        _client.getCommunityChannelsV2(communityId, cancellationToken);

    public Task<SonoranResponse> getConnectedUsersV2(string? communityId = null, CancellationToken cancellationToken = default) =>
        _client.getConnectedUsersV2(communityId, cancellationToken);

    public Task<SonoranResponse> getMembersV2(GetMembersV2Query? query = null, CancellationToken cancellationToken = default) =>
        _client.getMembersV2(query, cancellationToken);

    public Task<SonoranResponse> getConnectedUserV2(string identity, string? communityId = null, CancellationToken cancellationToken = default) =>
        _client.getConnectedUserV2(identity, communityId, cancellationToken);

    public Task<SonoranResponse> setUserChannelsV2(string identity, object? options = null, string? communityId = null, CancellationToken cancellationToken = default) =>
        _client.setUserChannelsV2(identity, options, communityId, cancellationToken);

    public Task<SonoranResponse> setUserDisplayNameV2(SetUserDisplayNameV2Request request, CancellationToken cancellationToken = default) =>
        _client.setUserDisplayNameV2(request, cancellationToken);

    public Task<SonoranResponse> approveMembersV2(IReadOnlyList<string> accIds, string? communityId = null, CancellationToken cancellationToken = default) =>
        _client.approveMembersV2(accIds, communityId, cancellationToken);

    public Task<SonoranResponse> kickMembersV2(IReadOnlyList<string> accIds, string? communityId = null, CancellationToken cancellationToken = default) =>
        _client.kickMembersV2(accIds, communityId, cancellationToken);

    public Task<SonoranResponse> banMembersV2(IReadOnlyList<string> accIds, string? communityId = null, CancellationToken cancellationToken = default) =>
        _client.banMembersV2(accIds, communityId, cancellationToken);

    public Task<SonoranResponse> setMemberDisplayNamesV2(IReadOnlyList<MemberDisplayNameV2Change> accNicknames, string? communityId = null, CancellationToken cancellationToken = default) =>
        _client.setMemberDisplayNamesV2(accNicknames, communityId, cancellationToken);

    public Task<SonoranResponse> setMemberPermissionsV2(IReadOnlyList<MemberPermissionV2Change> userPerms, string? communityId = null, CancellationToken cancellationToken = default) =>
        _client.setMemberPermissionsV2(userPerms, communityId, cancellationToken);

    public Task<SonoranResponse> getServerSubscriptionFromIpV2(CancellationToken cancellationToken = default) =>
        _client.getServerSubscriptionFromIpV2(cancellationToken);

    public Task<SonoranResponse> setServerIpV2(SetServerIpV2Request request, CancellationToken cancellationToken = default) =>
        _client.setServerIpV2(request, cancellationToken);

    public Task<SonoranResponse> setInGameSpeakerLocationsV2(IReadOnlyList<object?> locations, string? communityId = null, CancellationToken cancellationToken = default) =>
        _client.setInGameSpeakerLocationsV2(locations, communityId, cancellationToken);

    public Task<SonoranResponse> playToneV2(PlayToneV2Request request, CancellationToken cancellationToken = default) =>
        _client.playToneV2(request, cancellationToken);
}
