namespace Sonoran;

public sealed partial class SonoranClient
{
    public Task<SonoranResponse> getIntegrationPanelsV2(CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/integration-panels", cancellationToken: cancellationToken);

    public Task<SonoranResponse> getIntegrationPanelV2(string panelKey, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, $"v2/integration-panels/{EncodePathSegment(panelKey)}", cancellationToken: cancellationToken);

    public Task<SonoranResponse> setIntegrationPanelV2(string panelKey, IntegrationPanelDefinitionV2 definition, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Put, $"v2/integration-panels/{EncodePathSegment(panelKey)}", body: new { definition }, cancellationToken: cancellationToken);

    public Task<SonoranResponse> deleteIntegrationPanelV2(string panelKey, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Delete, $"v2/integration-panels/{EncodePathSegment(panelKey)}", cancellationToken: cancellationToken);

    public Task<SonoranResponse> setIntegrationPanelStateV2(
        string panelKey,
        string instanceKey,
        IReadOnlyDictionary<string, object?> state,
        int? serverId = null,
        CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(serverId);
        return RequestAsync(
            HttpMethod.Put,
            $"v2/integration-panels/servers/{resolvedServerId}/panels/{EncodePathSegment(panelKey)}/instances/{EncodePathSegment(instanceKey)}/state",
            body: new { state },
            cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> getIntegrationPanelActionsV2(
        string panelKey,
        GetIntegrationPanelActionsV2Query? query = null,
        CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(query?.ServerId);
        return RequestAsync(
            HttpMethod.Get,
            $"v2/integration-panels/servers/{resolvedServerId}/panels/{EncodePathSegment(panelKey)}/actions",
            query: ToQueryDictionary(new { query?.After, query?.Limit }),
            cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> acknowledgeIntegrationPanelActionV2(
        string panelKey,
        string eventId,
        AcknowledgeIntegrationPanelActionV2Request request,
        CancellationToken cancellationToken = default)
    {
        var resolvedServerId = ResolveServerId(request.ServerId);
        return RequestAsync(
            HttpMethod.Post,
            $"v2/integration-panels/servers/{resolvedServerId}/panels/{EncodePathSegment(panelKey)}/actions/{EncodePathSegment(eventId)}/ack",
            body: WithoutServerId(request),
            cancellationToken: cancellationToken);
    }
}
