namespace Sonoran;

public sealed partial class SonoranClient
{
    public Task<SonoranResponse> getCharactersV2(GetCharactersV2Query? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/civilian/characters", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> removeCharacterV2(int characterId, CancellationToken cancellationToken = default)
    {
        AssertPositiveInteger(characterId, nameof(characterId));
        return RequestAsync(HttpMethod.Delete, $"v2/civilian/characters/{characterId}", cancellationToken: cancellationToken);
    }

    public Task<SonoranResponse> setSelectedCharacterV2(SetSelectedCharacterV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Put, "v2/civilian/selected-character", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> getCharacterLinksV2(GetCharacterLinksV2Query? query = null, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Get, "v2/civilian/character-links", query: ToQueryDictionary(query), cancellationToken: cancellationToken);

    public Task<SonoranResponse> addCharacterLinkV2(string syncId, CharacterLinkTargetV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Put, $"v2/civilian/character-links/{EncodePathSegment(syncId)}", body: request, cancellationToken: cancellationToken);

    public Task<SonoranResponse> removeCharacterLinkV2(string syncId, CharacterLinkTargetV2Request request, CancellationToken cancellationToken = default) =>
        RequestAsync(HttpMethod.Delete, $"v2/civilian/character-links/{EncodePathSegment(syncId)}", body: request, cancellationToken: cancellationToken);
}
