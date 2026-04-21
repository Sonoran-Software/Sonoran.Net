namespace Sonoran;

public sealed class SonoranCmsClient
{
    private readonly SonoranClient _client;

    internal SonoranCmsClient(SonoranClient client)
    {
        _client = client;
    }

    public Task<SonoranResponse> getCommunityV2(CancellationToken cancellationToken = default) =>
        _client.getCommunityV2(cancellationToken);

    public Task<SonoranResponse> getSubVersionV2(CancellationToken cancellationToken = default) =>
        _client.getSubVersionV2(cancellationToken);

    public Task<SonoranResponse> lookupCommunityV2(object? query = null, CancellationToken cancellationToken = default) =>
        _client.lookupCommunityV2(query, cancellationToken);

    public Task<SonoranResponse> getDepartmentsV2(CancellationToken cancellationToken = default) =>
        _client.getDepartmentsV2(cancellationToken);

    public Task<SonoranResponse> getProfileFieldsV2(CancellationToken cancellationToken = default) =>
        _client.getProfileFieldsV2(cancellationToken);

    public Task<SonoranResponse> getClockInTypesV2(CancellationToken cancellationToken = default) =>
        _client.getClockInTypesV2(cancellationToken);

    public Task<SonoranResponse> getCustomLogTypesV2(CancellationToken cancellationToken = default) =>
        _client.getCustomLogTypesV2(cancellationToken);

    public Task<SonoranResponse> getPromotionFlowsV2(CancellationToken cancellationToken = default) =>
        _client.getPromotionFlowsV2(cancellationToken);

    public Task<SonoranResponse> triggerPromotionFlowsV2(object request, CancellationToken cancellationToken = default) =>
        _client.triggerPromotionFlowsV2(request, cancellationToken);

    public Task<SonoranResponse> undoRankChangeV2(string undoId, object? request = null, CancellationToken cancellationToken = default) =>
        _client.undoRankChangeV2(undoId, request, cancellationToken);

    public Task<SonoranResponse> createShortUrlV2(object request, CancellationToken cancellationToken = default) =>
        _client.createShortUrlV2(request, cancellationToken);

    public Task<SonoranResponse> getAccountsV2(object? query = null, CancellationToken cancellationToken = default) =>
        _client.getAccountsV2(query, cancellationToken);

    public Task<SonoranResponse> searchAccountsV2(object? query = null, CancellationToken cancellationToken = default) =>
        _client.searchAccountsV2(query, cancellationToken);

    public Task<SonoranResponse> getAccountV2(string accountId, CancellationToken cancellationToken = default) =>
        _client.getAccountV2(accountId, cancellationToken);

    public Task<SonoranResponse> getAccountRanksV2(string accountId, CancellationToken cancellationToken = default) =>
        _client.getAccountRanksV2(accountId, cancellationToken);

    public Task<SonoranResponse> getAccountIdentifiersV2(string accountId, CancellationToken cancellationToken = default) =>
        _client.getAccountIdentifiersV2(accountId, cancellationToken);

    public Task<SonoranResponse> registerAccountIdentifiersV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        _client.registerAccountIdentifiersV2(accountId, request, cancellationToken);

    public Task<SonoranResponse> setAccountNameV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        _client.setAccountNameV2(accountId, request, cancellationToken);

    public Task<SonoranResponse> setAccountRanksV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        _client.setAccountRanksV2(accountId, request, cancellationToken);

    public Task<SonoranResponse> editProfileFieldsV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        _client.editProfileFieldsV2(accountId, request, cancellationToken);

    public Task<SonoranResponse> clockAccountV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        _client.clockAccountV2(accountId, request, cancellationToken);

    public Task<SonoranResponse> getCurrentClockInV2(string accountId, CancellationToken cancellationToken = default) =>
        _client.getCurrentClockInV2(accountId, cancellationToken);

    public Task<SonoranResponse> getLatestActivityV2(string accountId, object? query = null, CancellationToken cancellationToken = default) =>
        _client.getLatestActivityV2(accountId, query, cancellationToken);

    public Task<SonoranResponse> forceSyncV2(string accountId, object? request = null, CancellationToken cancellationToken = default) =>
        _client.forceSyncV2(accountId, request, cancellationToken);

    public Task<SonoranResponse> getServersV2(CancellationToken cancellationToken = default) =>
        _client.getCommunityServersV2(cancellationToken);

    public Task<SonoranResponse> setServersV2(object request, CancellationToken cancellationToken = default) =>
        _client.setServersV2(request, cancellationToken);

    public Task<SonoranResponse> addServersV2(object request, CancellationToken cancellationToken = default) =>
        _client.addServersV2(request, cancellationToken);

    public Task<SonoranResponse> getAceConfigV2(int serverId, CancellationToken cancellationToken = default) =>
        _client.getAceConfigV2(serverId, cancellationToken);

    public Task<SonoranResponse> setAceConfigV2(int serverId, object request, CancellationToken cancellationToken = default) =>
        _client.setAceConfigV2(serverId, request, cancellationToken);

    public Task<SonoranResponse> setServerTypeV2(int serverId, object request, CancellationToken cancellationToken = default) =>
        _client.setServerTypeV2(serverId, request, cancellationToken);

    public Task<SonoranResponse> verifyWhitelistV2(int serverId, object request, CancellationToken cancellationToken = default) =>
        _client.verifyWhitelistV2(serverId, request, cancellationToken);

    public Task<SonoranResponse> getWhitelistV2(int serverId, CancellationToken cancellationToken = default) =>
        _client.getWhitelistV2(serverId, cancellationToken);

    public Task<SonoranResponse> createActivityV2(int serverId, object? request = null, CancellationToken cancellationToken = default) =>
        _client.createActivityV2(serverId, request, cancellationToken);

    public Task<SonoranResponse> startActivityV2(int serverId, object? request = null, CancellationToken cancellationToken = default) =>
        _client.startActivityV2(serverId, request, cancellationToken);

    public Task<SonoranResponse> rsvpEventV2(string eventId, object request, CancellationToken cancellationToken = default) =>
        _client.rsvpEventV2(eventId, request, cancellationToken);

    public Task<SonoranResponse> changeFormStageV2(string formId, object request, CancellationToken cancellationToken = default) =>
        _client.changeFormStageV2(formId, request, cancellationToken);

    public Task<SonoranResponse> getFormSubmissionsV2(string templateId, object? query = null, CancellationToken cancellationToken = default) =>
        _client.getFormSubmissionsV2(templateId, query, cancellationToken);

    public Task<SonoranResponse> getFormLockV2(string templateId, CancellationToken cancellationToken = default) =>
        _client.getFormLockV2(templateId, cancellationToken);

    public Task<SonoranResponse> setFormLockV2(string templateId, object request, CancellationToken cancellationToken = default) =>
        _client.setFormLockV2(templateId, request, cancellationToken);

    public Task<SonoranResponse> getSubmissionV2(string submissionId, CancellationToken cancellationToken = default) =>
        _client.getSubmissionV2(submissionId, cancellationToken);

    public Task<SonoranResponse> getRosterV2(string rosterId, object? query = null, CancellationToken cancellationToken = default) =>
        _client.getRosterV2(rosterId, query, cancellationToken);

    public Task<SonoranResponse> getDisciplinaryPointsV2(string accountId, CancellationToken cancellationToken = default) =>
        _client.getDisciplinaryPointsV2(accountId, cancellationToken);

    public Task<SonoranResponse> getDisciplinaryRecordsV2(string accountId, CancellationToken cancellationToken = default) =>
        _client.getDisciplinaryRecordsV2(accountId, cancellationToken);

    public Task<SonoranResponse> addDisciplinaryRecordV2(string accountId, object request, CancellationToken cancellationToken = default) =>
        _client.addDisciplinaryRecordV2(accountId, request, cancellationToken);

    public Task<SonoranResponse> setDisciplinaryRecordPointsV2(string recordId, object request, CancellationToken cancellationToken = default) =>
        _client.setDisciplinaryRecordPointsV2(recordId, request, cancellationToken);

    public Task<SonoranResponse> setDisciplinaryRecordReasonV2(string recordId, object request, CancellationToken cancellationToken = default) =>
        _client.setDisciplinaryRecordReasonV2(recordId, request, cancellationToken);

    public Task<SonoranResponse> setDisciplinaryRecordStatusV2(string recordId, object request, CancellationToken cancellationToken = default) =>
        _client.setDisciplinaryRecordStatusV2(recordId, request, cancellationToken);

    public Task<SonoranResponse> getOnlinePlayersV2(object? query = null, CancellationToken cancellationToken = default) =>
        _client.getOnlinePlayersV2(query, cancellationToken);

    public Task<SonoranResponse> getPlayerQueueV2(object? query = null, CancellationToken cancellationToken = default) =>
        _client.getPlayerQueueV2(query, cancellationToken);

    public Task<SonoranResponse> addErlcRecordV2(object request, CancellationToken cancellationToken = default) =>
        _client.addErlcRecordV2(request, cancellationToken);

    public Task<SonoranResponse> executeErlcCommandV2(object request, CancellationToken cancellationToken = default) =>
        _client.executeErlcCommandV2(request, cancellationToken);

    public Task<SonoranResponse> lockTeamV2(object request, CancellationToken cancellationToken = default) =>
        _client.lockTeamV2(request, cancellationToken);

    public Task<SonoranResponse> unlockTeamV2(object request, CancellationToken cancellationToken = default) =>
        _client.unlockTeamV2(request, cancellationToken);

    public Task<SonoranResponse> getCurrentSessionV2(int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.getCurrentSessionV2(serverId, cancellationToken);

    public Task<SonoranResponse> startSessionV2(object request, CancellationToken cancellationToken = default) =>
        _client.startSessionV2(request, cancellationToken);

    public Task<SonoranResponse> stopSessionV2(object request, CancellationToken cancellationToken = default) =>
        _client.stopSessionV2(request, cancellationToken);

    public Task<SonoranResponse> cancelSessionV2(object request, CancellationToken cancellationToken = default) =>
        _client.cancelSessionV2(request, cancellationToken);
}
