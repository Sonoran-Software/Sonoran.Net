namespace Sonoran;

public sealed partial class SonoranClient
{
    public Task<SonoranResponse> getUnitsV2(GetUnitsV2Query? query = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(query?.ServerId);
        var queryData = query is null ? null : new { query.IncludeOffline, query.OnlyUnits, query.Limit, query.Offset };
        return RequestAsync(HttpMethod.Get, $"v2/emergency/servers/{resolvedServerId}/units", query: ToQueryDictionary(queryData), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getCallsV2(GetCallsV2Query? query = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(query?.ServerId);
        var queryData = query is null ? null : new { query.ClosedLimit, query.ClosedOffset, query.Type };
        return RequestAsync(HttpMethod.Get, $"v2/emergency/servers/{resolvedServerId}/calls", query: ToQueryDictionary(queryData), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getCurrentCallV2(string accountUuid, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/emergency/accounts/{EncodePathSegment(accountUuid)}/current-call", cancellationToken: cancellationToken);

    public Task<SonoranResponse> updateUnitLocationsV2(UpdateUnitLocationsV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Patch, $"v2/emergency/servers/{resolvedServerId}/unit-locations", body: new { request.Updates }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setUnitPanicV2(SetUnitPanicV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Patch, $"v2/emergency/servers/{resolvedServerId}/units/panic", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setUnitStatusV2(SetUnitStatusV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Patch, $"v2/emergency/servers/{resolvedServerId}/units/status", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> kickUnitV2(KickUnitV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Delete, $"v2/emergency/servers/{resolvedServerId}/units/kick", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getIdentifiersV2(string accountUuid, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/emergency/accounts/{EncodePathSegment(accountUuid)}/identifiers", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getAccountUnitsV2(GetAccountUnitsV2Query request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.AccountUuid);
        var query = new { request.OnlyOnline, request.OnlyUnits, request.Limit, request.Offset };
        return RequestAsync(HttpMethod.Get, $"v2/emergency/servers/{resolvedServerId}/accounts/{EncodePathSegment(request.AccountUuid)}/units", query: ToQueryDictionary(query), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> selectIdentifierV2(string accountUuid, int identId, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(identId, nameof(identId));
        return RequestAsync(HttpMethod.Put, $"v2/emergency/accounts/{EncodePathSegment(accountUuid)}/selected-identifier", body: new { identId }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> createIdentifierV2(string accountUuid, IdentifierV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Post, $"v2/emergency/accounts/{EncodePathSegment(accountUuid)}/identifiers", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> updateIdentifierV2(string accountUuid, int identId, IdentifierV2Request request, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(identId, nameof(identId));
        return RequestAsync(HttpMethod.Patch, $"v2/emergency/accounts/{EncodePathSegment(accountUuid)}/identifiers/{identId}", body: request, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> deleteIdentifierV2(string accountUuid, int identId, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(identId, nameof(identId));
        return RequestAsync(HttpMethod.Delete, $"v2/emergency/accounts/{EncodePathSegment(accountUuid)}/identifiers/{identId}", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> addIdentifiersToGroupV2(AddIdentifiersToGroupV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.GroupName);
        return RequestAsync(HttpMethod.Put, $"v2/emergency/servers/{resolvedServerId}/identifier-groups/{EncodePathSegment(request.GroupName)}", body: WithoutKeys(request, nameof(request.ServerId), nameof(request.GroupName)), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> createEmergencyCallV2(CreateEmergencyCallV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Post, $"v2/emergency/servers/{resolvedServerId}/calls/911", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> deleteEmergencyCallV2(int callId, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        AssertPositiveInteger(callId, nameof(callId));
        return RequestAsync(HttpMethod.Delete, $"v2/emergency/servers/{resolvedServerId}/calls/911/{callId}", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> createDispatchCallV2(CreateDispatchCallV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Post, $"v2/emergency/servers/{resolvedServerId}/dispatch-calls", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> updateDispatchCallV2(int callId, UpdateDispatchCallV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        AssertPositiveInteger(callId, nameof(callId));
        return RequestAsync(HttpMethod.Patch, $"v2/emergency/servers/{resolvedServerId}/dispatch-calls/{callId}", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> attachUnitsToDispatchCallV2(int callId, DispatchAttachmentV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        AssertPositiveInteger(callId, nameof(callId));
        return RequestAsync(HttpMethod.Post, $"v2/emergency/servers/{resolvedServerId}/dispatch-calls/{callId}/attachments", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> detachUnitsFromDispatchCallV2(DispatchAttachmentV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Delete, $"v2/emergency/servers/{resolvedServerId}/dispatch-calls/attachments", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setDispatchPostalV2(int callId, string postal, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        AssertPositiveInteger(callId, nameof(callId));
        return RequestAsync(HttpMethod.Patch, $"v2/emergency/servers/{resolvedServerId}/dispatch-calls/{callId}/postal", body: new { postal }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setDispatchPrimaryV2(int callId, int identId, bool trackPrimary = false, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        AssertPositiveInteger(callId, nameof(callId));
        AssertPositiveInteger(identId, nameof(identId));
        return RequestAsync(HttpMethod.Patch, $"v2/emergency/servers/{resolvedServerId}/dispatch-calls/{callId}/primary", body: new { identId, trackPrimary }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> addDispatchNoteV2(int callId, AddDispatchNoteV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        AssertPositiveInteger(callId, nameof(callId));
        return RequestAsync(HttpMethod.Post, $"v2/emergency/servers/{resolvedServerId}/dispatch-calls/{callId}/notes", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> closeDispatchCallsV2(IReadOnlyList<int> callIds, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Post, $"v2/emergency/servers/{resolvedServerId}/dispatch-calls/close", body: new { callIds }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> updateStreetSignsV2(UpdateStreetSignsV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Patch, $"v2/emergency/servers/{resolvedServerId}/street-signs", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setStreetSignConfigV2(IReadOnlyList<Dictionary<string, object?>> signs, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Put, $"v2/emergency/servers/{resolvedServerId}/street-sign-config", body: new { signs }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setAvailableCalloutsV2(IReadOnlyList<AvailableCalloutV2> callouts, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Put, $"v2/emergency/servers/{resolvedServerId}/callouts", body: new { callouts }, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getPagerConfigV2(int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Get, $"v2/emergency/servers/{resolvedServerId}/pager-config", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setPagerConfigV2(SetPagerConfigV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Put, $"v2/emergency/servers/{resolvedServerId}/pager-config", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setStationsV2(StationConfigV2 config, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Put, $"v2/emergency/servers/{resolvedServerId}/stations", body: config, cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getBlipsV2(int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Get, $"v2/emergency/servers/{resolvedServerId}/blips", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> createBlipV2(CreateBlipV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(HttpMethod.Post, $"v2/emergency/servers/{resolvedServerId}/blips", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> updateBlipV2(int blipId, UpdateBlipV2Request request, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        AssertPositiveInteger(blipId, nameof(blipId));
        return RequestAsync(HttpMethod.Patch, $"v2/emergency/servers/{resolvedServerId}/blips/{blipId}", body: WithoutServerId(request), cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> deleteBlipsV2(IReadOnlyList<int> ids, int? serverId = null, CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Post, $"v2/emergency/servers/{resolvedServerId}/blips/delete", body: new { ids }, cancellationToken: cancellationToken);
    }
}
