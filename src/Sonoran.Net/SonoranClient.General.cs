namespace Sonoran;

public sealed partial class SonoranClient
{
    public Task<SonoranResponse> getLoginPageV2(GetLoginPageV2Query? query = null, CancellationToken cancellationToken = default)
    {
        var merged = new GetLoginPageV2Query
        {
            Url = query?.Url,
            CommunityId = query?.CommunityId ?? Options.communityId
        };

        return RequestAsync(HttpMethod.Get, "v2/general/login-page", query: ToQueryDictionary(merged), authenticated: false, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> checkApiIdV2(string apiId, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/general/api-ids/{EncodePathSegment(apiId)}", cancellationToken: cancellationToken);

    public Task<SonoranResponse> applyPermissionKeyV2(ApplyPermissionKeyV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/general/permission-keys/applications", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> banUserV2(BanUserV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/general/account-bans", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> setPenalCodesV2(IReadOnlyList<PenalCodeV2> codes, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Put, "v2/general/penal-codes", body: new { codes }, cancellationToken: cancellationToken);

    public Task<SonoranResponse> setApiIdsV2(SetApiIdsV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Put, "v2/general/api-ids", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getTemplatesV2(int? recordTypeId = null, CancellationToken cancellationToken = default)
    {
        if (recordTypeId is not null)
        {
            AssertPositiveInteger(recordTypeId.Value, nameof(recordTypeId));
            return RequestAsync(HttpMethod.Get, $"v2/general/templates/{recordTypeId.Value}", cancellationToken: cancellationToken);
        }

        return RequestAsync(HttpMethod.Get, "v2/general/templates", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> createRecordV2(CreateRecordV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/general/records", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> updateRecordV2(int recordId, UpdateRecordV2Request request, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(recordId, nameof(recordId));
        return RequestAsync(HttpMethod.Patch, $"v2/general/records/{recordId}", body: request, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> removeRecordV2(int recordId, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(recordId, nameof(recordId));
        return RequestAsync(HttpMethod.Delete, $"v2/general/records/{recordId}", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> sendRecordDraftV2(SendRecordDraftV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/general/record-drafts", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> lookupV2(LookupV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/general/lookups", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> lookupByValueV2(LookupByValueV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/general/lookups/by-value", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> lookupCustomV2(LookupCustomV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/general/lookups/custom", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getAccountV2(GetAccountV2Query? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/general/accounts/account", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> getAccountsV2(GetAccountsV2Query? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/general/accounts", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> createCommunityLinkV2(CommunityLinkV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/general/links", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> checkCommunityLinkV2(CommunityLinkV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/general/links/check", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> setAccountPermissionsV2(SetAccountPermissionsV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Patch, "v2/general/accounts/permissions", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> heartbeatV2(int? serverId, int playerCount, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Post, $"v2/general/servers/{resolvedServerId}/heartbeat", body: new { playerCount }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getVersionV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/general/version", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getTurnCredentialsV2(GetTurnCredentialsV2Query? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/general/turn", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> getServersV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/general/servers", cancellationToken: cancellationToken);

    public Task<SonoranResponse> setServersV2(IReadOnlyList<CadServerV2> servers, bool deployMap = false, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Put, "v2/general/servers", body: new { servers, deployMap }, cancellationToken: cancellationToken);

    public Task<SonoranResponse> verifySecretV2(string secret, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/general/secrets/verify", body: new { secret }, cancellationToken: cancellationToken);

    public Task<SonoranResponse> authorizeStreetSignsV2(int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Post, $"v2/general/servers/{resolvedServerId}/street-sign-auth", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setPostalsV2(IReadOnlyList<PostalV2> postals, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Put, "v2/general/postals", body: new { postals }, cancellationToken: cancellationToken);

    public Task<SonoranResponse> sendPhotoV2(SendPhotoV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/general/photos", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getInfoV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/general/info", cancellationToken: cancellationToken);
}
