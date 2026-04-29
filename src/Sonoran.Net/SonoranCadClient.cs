namespace Sonoran;

public sealed class SonoranCadClient
{
    private readonly SonoranClient _client;

    internal SonoranCadClient(SonoranClient client)
    {
        _client = client;
    }

    public Task<SonoranResponse> getLoginPageV2(GetLoginPageV2Query? query = null, CancellationToken cancellationToken = default) =>
        _client.getLoginPageV2(query, cancellationToken);

    public Task<SonoranResponse> checkApiIdV2(string apiId, CancellationToken cancellationToken = default) =>
        _client.checkApiIdV2(apiId, cancellationToken);

    public Task<SonoranResponse> applyPermissionKeyV2(ApplyPermissionKeyV2Request request, CancellationToken cancellationToken = default) =>
        _client.applyPermissionKeyV2(request, cancellationToken);

    public Task<SonoranResponse> banUserV2(BanUserV2Request request, CancellationToken cancellationToken = default) =>
        _client.banUserV2(request, cancellationToken);

    public Task<SonoranResponse> setPenalCodesV2(IReadOnlyList<PenalCodeV2> codes, CancellationToken cancellationToken = default) =>
        _client.setPenalCodesV2(codes, cancellationToken);

    public Task<SonoranResponse> setApiIdsV2(SetApiIdsV2Request request, CancellationToken cancellationToken = default) =>
        _client.setApiIdsV2(request, cancellationToken);

    public Task<SonoranResponse> getTemplatesV2(int? recordTypeId = null, CancellationToken cancellationToken = default) =>
        _client.getTemplatesV2(recordTypeId, cancellationToken);

    public Task<SonoranResponse> createRecordV2(CreateRecordV2Request request, CancellationToken cancellationToken = default) =>
        _client.createRecordV2(request, cancellationToken);

    public Task<SonoranResponse> updateRecordV2(int recordId, UpdateRecordV2Request request, CancellationToken cancellationToken = default) =>
        _client.updateRecordV2(recordId, request, cancellationToken);

    public Task<SonoranResponse> removeRecordV2(int recordId, CancellationToken cancellationToken = default) =>
        _client.removeRecordV2(recordId, cancellationToken);

    public Task<SonoranResponse> sendRecordDraftV2(SendRecordDraftV2Request request, CancellationToken cancellationToken = default) =>
        _client.sendRecordDraftV2(request, cancellationToken);

    public Task<SonoranResponse> lookupV2(LookupV2Request request, CancellationToken cancellationToken = default) =>
        _client.lookupV2(request, cancellationToken);

    public Task<SonoranResponse> lookupByValueV2(LookupByValueV2Request request, CancellationToken cancellationToken = default) =>
        _client.lookupByValueV2(request, cancellationToken);

    public Task<SonoranResponse> lookupCustomV2(LookupCustomV2Request request, CancellationToken cancellationToken = default) =>
        _client.lookupCustomV2(request, cancellationToken);

    public Task<SonoranResponse> getAccountV2(GetAccountV2Query? query = null, CancellationToken cancellationToken = default) =>
        _client.getAccountV2(query, cancellationToken);

    public Task<SonoranResponse> getAccountsV2(GetAccountsV2Query? query = null, CancellationToken cancellationToken = default) =>
        _client.getAccountsV2(query, cancellationToken);

    public Task<SonoranResponse> createCommunityLinkV2(CommunityLinkV2Request request, CancellationToken cancellationToken = default) =>
        _client.createCommunityLinkV2(request, cancellationToken);

    public Task<SonoranResponse> checkCommunityLinkV2(CommunityLinkV2Request request, CancellationToken cancellationToken = default) =>
        _client.checkCommunityLinkV2(request, cancellationToken);

    public Task<SonoranResponse> setAccountPermissionsV2(SetAccountPermissionsV2Request request, CancellationToken cancellationToken = default) =>
        _client.setAccountPermissionsV2(request, cancellationToken);

    public Task<SonoranResponse> heartbeatV2(int? serverId, int playerCount, CancellationToken cancellationToken = default) =>
        _client.heartbeatV2(serverId, playerCount, cancellationToken);

    public Task<SonoranResponse> getVersionV2(CancellationToken cancellationToken = default) =>
        _client.getVersionV2(cancellationToken);

    public Task<SonoranResponse> getTurnCredentialsV2(GetTurnCredentialsV2Query? query = null, CancellationToken cancellationToken = default) =>
        _client.getTurnCredentialsV2(query, cancellationToken);

    public Task<SonoranResponse> getServersV2(CancellationToken cancellationToken = default) =>
        _client.getServersV2(cancellationToken);

    public Task<SonoranResponse> setServersV2(IReadOnlyList<CadServerV2> servers, bool deployMap = false, CancellationToken cancellationToken = default) =>
        _client.setServersV2(servers, deployMap, cancellationToken);

    public Task<SonoranResponse> verifySecretV2(string secret, CancellationToken cancellationToken = default) =>
        _client.verifySecretV2(secret, cancellationToken);

    public Task<SonoranResponse> authorizeStreetSignsV2(int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.authorizeStreetSignsV2(serverId, cancellationToken);

    public Task<SonoranResponse> setPostalsV2(IReadOnlyList<PostalV2> postals, CancellationToken cancellationToken = default) =>
        _client.setPostalsV2(postals, cancellationToken);

    public Task<SonoranResponse> sendPhotoV2(SendPhotoV2Request request, CancellationToken cancellationToken = default) =>
        _client.sendPhotoV2(request, cancellationToken);

    public Task<SonoranResponse> getInfoV2(CancellationToken cancellationToken = default) =>
        _client.getInfoV2(cancellationToken);

    public Task<SonoranResponse> getCharactersV2(GetCharactersV2Query? query = null, CancellationToken cancellationToken = default) =>
        _client.getCharactersV2(query, cancellationToken);

    public Task<SonoranResponse> removeCharacterV2(int characterId, CancellationToken cancellationToken = default) =>
        _client.removeCharacterV2(characterId, cancellationToken);

    public Task<SonoranResponse> setSelectedCharacterV2(SetSelectedCharacterV2Request request, CancellationToken cancellationToken = default) =>
        _client.setSelectedCharacterV2(request, cancellationToken);

    public Task<SonoranResponse> getCharacterLinksV2(GetCharacterLinksV2Query? query = null, CancellationToken cancellationToken = default) =>
        _client.getCharacterLinksV2(query, cancellationToken);

    public Task<SonoranResponse> addCharacterLinkV2(string syncId, CharacterLinkTargetV2Request request, CancellationToken cancellationToken = default) =>
        _client.addCharacterLinkV2(syncId, request, cancellationToken);

    public Task<SonoranResponse> removeCharacterLinkV2(string syncId, CharacterLinkTargetV2Request request, CancellationToken cancellationToken = default) =>
        _client.removeCharacterLinkV2(syncId, request, cancellationToken);

    public Task<SonoranResponse> getUnitsV2(GetUnitsV2Query? query = null, CancellationToken cancellationToken = default) =>
        _client.getUnitsV2(query, cancellationToken);

    public Task<SonoranResponse> getCallsV2(GetCallsV2Query? query = null, CancellationToken cancellationToken = default) =>
        _client.getCallsV2(query, cancellationToken);

    public Task<SonoranResponse> getCurrentCallV2(string accountUuid, CancellationToken cancellationToken = default) =>
        _client.getCurrentCallV2(accountUuid, cancellationToken);

    public Task<SonoranResponse> updateUnitLocationsV2(UpdateUnitLocationsV2Request request, CancellationToken cancellationToken = default) =>
        _client.updateUnitLocationsV2(request, cancellationToken);

    public Task<SonoranResponse> setUnitPanicV2(SetUnitPanicV2Request request, CancellationToken cancellationToken = default) =>
        _client.setUnitPanicV2(request, cancellationToken);

    public Task<SonoranResponse> setUnitStatusV2(SetUnitStatusV2Request request, CancellationToken cancellationToken = default) =>
        _client.setUnitStatusV2(request, cancellationToken);

    public Task<SonoranResponse> kickUnitV2(KickUnitV2Request request, CancellationToken cancellationToken = default) =>
        _client.kickUnitV2(request, cancellationToken);

    public Task<SonoranResponse> getIdentifiersV2(string accountUuid, CancellationToken cancellationToken = default) =>
        _client.getIdentifiersV2(accountUuid, cancellationToken);

    public Task<SonoranResponse> getAccountUnitsV2(GetAccountUnitsV2Query request, CancellationToken cancellationToken = default) =>
        _client.getAccountUnitsV2(request, cancellationToken);

    public Task<SonoranResponse> selectIdentifierV2(string accountUuid, int identId, CancellationToken cancellationToken = default) =>
        _client.selectIdentifierV2(accountUuid, identId, cancellationToken);

    public Task<SonoranResponse> createIdentifierV2(string accountUuid, IdentifierV2Request request, CancellationToken cancellationToken = default) =>
        _client.createIdentifierV2(accountUuid, request, cancellationToken);

    public Task<SonoranResponse> updateIdentifierV2(string accountUuid, int identId, IdentifierV2Request request, CancellationToken cancellationToken = default) =>
        _client.updateIdentifierV2(accountUuid, identId, request, cancellationToken);

    public Task<SonoranResponse> deleteIdentifierV2(string accountUuid, int identId, CancellationToken cancellationToken = default) =>
        _client.deleteIdentifierV2(accountUuid, identId, cancellationToken);

    public Task<SonoranResponse> addIdentifiersToGroupV2(AddIdentifiersToGroupV2Request request, CancellationToken cancellationToken = default) =>
        _client.addIdentifiersToGroupV2(request, cancellationToken);

    public Task<SonoranResponse> createEmergencyCallV2(CreateEmergencyCallV2Request request, CancellationToken cancellationToken = default) =>
        _client.createEmergencyCallV2(request, cancellationToken);

    public Task<SonoranResponse> deleteEmergencyCallV2(int callId, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.deleteEmergencyCallV2(callId, serverId, cancellationToken);

    public Task<SonoranResponse> createDispatchCallV2(CreateDispatchCallV2Request request, CancellationToken cancellationToken = default) =>
        _client.createDispatchCallV2(request, cancellationToken);

    public Task<SonoranResponse> updateDispatchCallV2(int callId, UpdateDispatchCallV2Request request, CancellationToken cancellationToken = default) =>
        _client.updateDispatchCallV2(callId, request, cancellationToken);

    public Task<SonoranResponse> attachUnitsToDispatchCallV2(int callId, DispatchAttachmentV2Request request, CancellationToken cancellationToken = default) =>
        _client.attachUnitsToDispatchCallV2(callId, request, cancellationToken);

    public Task<SonoranResponse> detachUnitsFromDispatchCallV2(DispatchAttachmentV2Request request, CancellationToken cancellationToken = default) =>
        _client.detachUnitsFromDispatchCallV2(request, cancellationToken);

    public Task<SonoranResponse> setDispatchPostalV2(int callId, string postal, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.setDispatchPostalV2(callId, postal, serverId, cancellationToken);

    public Task<SonoranResponse> setDispatchPrimaryV2(int callId, int identId, bool trackPrimary = false, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.setDispatchPrimaryV2(callId, identId, trackPrimary, serverId, cancellationToken);

    public Task<SonoranResponse> addDispatchNoteV2(int callId, AddDispatchNoteV2Request request, CancellationToken cancellationToken = default) =>
        _client.addDispatchNoteV2(callId, request, cancellationToken);

    public Task<SonoranResponse> closeDispatchCallsV2(IReadOnlyList<int> callIds, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.closeDispatchCallsV2(callIds, serverId, cancellationToken);

    public Task<SonoranResponse> updateStreetSignsV2(UpdateStreetSignsV2Request request, CancellationToken cancellationToken = default) =>
        _client.updateStreetSignsV2(request, cancellationToken);

    public Task<SonoranResponse> setStreetSignConfigV2(IReadOnlyList<Dictionary<string, object?>> signs, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.setStreetSignConfigV2(signs, serverId, cancellationToken);

    public Task<SonoranResponse> setAvailableCalloutsV2(IReadOnlyList<AvailableCalloutV2> callouts, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.setAvailableCalloutsV2(callouts, serverId, cancellationToken);

    public Task<SonoranResponse> getPagerConfigV2(int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.getPagerConfigV2(serverId, cancellationToken);

    public Task<SonoranResponse> setPagerConfigV2(SetPagerConfigV2Request request, CancellationToken cancellationToken = default) =>
        _client.setPagerConfigV2(request, cancellationToken);

    public Task<SonoranResponse> setStationsV2(StationConfigV2 config, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.setStationsV2(config, serverId, cancellationToken);

    public Task<SonoranResponse> getBlipsV2(int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.getBlipsV2(serverId, cancellationToken);

    public Task<SonoranResponse> createBlipV2(CreateBlipV2Request request, CancellationToken cancellationToken = default) =>
        _client.createBlipV2(request, cancellationToken);

    public Task<SonoranResponse> updateBlipV2(int blipId, UpdateBlipV2Request request, CancellationToken cancellationToken = default) =>
        _client.updateBlipV2(blipId, request, cancellationToken);

    public Task<SonoranResponse> deleteBlipsV2(IReadOnlyList<int> ids, int? serverId = null, CancellationToken cancellationToken = default) =>
        _client.deleteBlipsV2(ids, serverId, cancellationToken);
}
