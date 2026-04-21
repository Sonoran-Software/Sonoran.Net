namespace Sonoran;

public sealed partial class SonoranClient
{
    public Task<SonoranResponse> getCommunityV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getSubVersionV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/sub-version", cancellationToken: cancellationToken);

    public Task<SonoranResponse> lookupCommunityV2(object? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/lookup", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> getDepartmentsV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/departments", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getProfileFieldsV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/profile-fields", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getClockInTypesV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/clockin-types", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getCustomLogTypesV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/custom-log-types", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getPromotionFlowsV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/promotion-flows", cancellationToken: cancellationToken);

    public Task<SonoranResponse> triggerPromotionFlowsV2(object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/community/promotion-flows/trigger", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> undoRankChangeV2(string undoId, object? request = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, $"v2/community/rank-changes/{EncodePathSegment(undoId)}/undo", body: request ?? new { }, cancellationToken: cancellationToken);

    public Task<SonoranResponse> createShortUrlV2(object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/community/short-urls", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getAccountsV2(object? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/accounts", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> searchAccountsV2(object? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/accounts/search", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> getAccountV2(string accountId, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/community/accounts/{EncodePathSegment(accountId)}", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getAccountRanksV2(string accountId, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/community/accounts/{EncodePathSegment(accountId)}/ranks", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getAccountIdentifiersV2(string accountId, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/community/accounts/{EncodePathSegment(accountId)}/identifiers", cancellationToken: cancellationToken);

    public Task<SonoranResponse> registerAccountIdentifiersV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, $"v2/community/accounts/{EncodePathSegment(accountId)}/identifiers", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> setAccountNameV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Patch, $"v2/community/accounts/{EncodePathSegment(accountId)}/name", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> setAccountRanksV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Patch, $"v2/community/accounts/{EncodePathSegment(accountId)}/ranks", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> editProfileFieldsV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Patch, $"v2/community/accounts/{EncodePathSegment(accountId)}/profile-fields", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> clockAccountV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, $"v2/community/accounts/{EncodePathSegment(accountId)}/clock", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getCurrentClockInV2(string accountId, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/community/accounts/{EncodePathSegment(accountId)}/clock/current", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getLatestActivityV2(string accountId, object? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/community/accounts/{EncodePathSegment(accountId)}/activity/latest", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> forceSyncV2(string accountId, object? request = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, $"v2/community/accounts/{EncodePathSegment(accountId)}/sync", body: request ?? new { }, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getCommunityServersV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/servers", cancellationToken: cancellationToken);

    public Task<SonoranResponse> setServersV2(object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Put, "v2/community/servers", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> addServersV2(object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/community/servers", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getAceConfigV2(int serverId, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(serverId, nameof(serverId));
        return RequestAsync(HttpMethod.Get, $"v2/community/servers/{serverId}/ace-config", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setAceConfigV2(int serverId, object request, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(serverId, nameof(serverId));
        return RequestAsync(HttpMethod.Patch, $"v2/community/servers/{serverId}/ace-config", body: request, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setServerTypeV2(int serverId, object request, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(serverId, nameof(serverId));
        return RequestAsync(HttpMethod.Patch, $"v2/community/servers/{serverId}/type", body: request, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> verifyWhitelistV2(int serverId, object request, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(serverId, nameof(serverId));
        return RequestAsync(HttpMethod.Post, $"v2/community/servers/{serverId}/whitelist/check", body: request, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getWhitelistV2(int serverId, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(serverId, nameof(serverId));
        return RequestAsync(HttpMethod.Get, $"v2/community/servers/{serverId}/whitelist", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> createActivityV2(int serverId, object? request = null, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(serverId, nameof(serverId));
        return RequestAsync(HttpMethod.Post, $"v2/community/servers/{serverId}/activity", body: request ?? new { }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> startActivityV2(int serverId, object? request = null, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(serverId, nameof(serverId));
        return RequestAsync(HttpMethod.Post, $"v2/community/servers/{serverId}/activity/start", body: request ?? new { }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> rsvpEventV2(string eventId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, $"v2/community/events/{EncodePathSegment(eventId)}/rsvps", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> changeFormStageV2(string formId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Patch, $"v2/community/forms/{EncodePathSegment(formId)}/stage", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getFormSubmissionsV2(string templateId, object? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/community/forms/{EncodePathSegment(templateId)}/submissions", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> getFormLockV2(string templateId, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/community/forms/{EncodePathSegment(templateId)}/lock", cancellationToken: cancellationToken);

    public Task<SonoranResponse> setFormLockV2(string templateId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Patch, $"v2/community/forms/{EncodePathSegment(templateId)}/lock", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getSubmissionV2(string submissionId, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/community/forms/submissions/{EncodePathSegment(submissionId)}", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getRosterV2(string rosterId, object? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/community/rosters/{EncodePathSegment(rosterId)}", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> getDisciplinaryPointsV2(string accountId, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/community/disciplinary/accounts/{EncodePathSegment(accountId)}/points", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getDisciplinaryRecordsV2(string accountId, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/community/disciplinary/accounts/{EncodePathSegment(accountId)}/records", cancellationToken: cancellationToken);

    public Task<SonoranResponse> addDisciplinaryRecordV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, $"v2/community/disciplinary/accounts/{EncodePathSegment(accountId)}/records", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> setDisciplinaryRecordPointsV2(string recordId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Patch, $"v2/community/disciplinary/records/{EncodePathSegment(recordId)}/points", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> setDisciplinaryRecordReasonV2(string recordId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Patch, $"v2/community/disciplinary/records/{EncodePathSegment(recordId)}/reason", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> setDisciplinaryRecordStatusV2(string recordId, object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Patch, $"v2/community/disciplinary/records/{EncodePathSegment(recordId)}/status", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getOnlinePlayersV2(object? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/erlc/players/online", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> getPlayerQueueV2(object? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/erlc/players/queue", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> addErlcRecordV2(object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/community/erlc/records", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> executeErlcCommandV2(object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/community/erlc/commands", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> lockTeamV2(object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/community/erlc/teams/lock", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> unlockTeamV2(object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/community/erlc/teams/unlock", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getCurrentSessionV2(int? serverId = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/community/sessions/current", query: ToQueryDictionary(new { serverId }), cancellationToken: cancellationToken);

    public Task<SonoranResponse> startSessionV2(object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, "v2/community/sessions", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> stopSessionV2(object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Patch, "v2/community/sessions", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> cancelSessionV2(object request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Delete, "v2/community/sessions", body: request, cancellationToken: cancellationToken);
}
