namespace Sonoran;

public sealed partial class SonoranClient
{
    public Task<SonoranResponse> getFiveMConfigurationV2(int? serverId = null, CancellationToken cancellationToken = default)
    {
        var id = ResolveServerId(serverId);
        return RequestAsync(HttpMethod.Get, $"v2/fivem/servers/{id}/configuration", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> acknowledgeFiveMConfigurationV2(int? serverId, long revision, CancellationToken cancellationToken = default)
    {
        var id = ResolveServerId(serverId);
        if (revision <= 0) throw new ArgumentOutOfRangeException(nameof(revision));
        return RequestAsync(HttpMethod.Post, $"v2/fivem/servers/{id}/configuration/acknowledge/{revision}", cancellationToken: cancellationToken);
    }
}
