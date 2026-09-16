namespace Sonoran;

public sealed record CadPermissionSetV2
{
    public int Version { get; init; } = 2;
    public IReadOnlyList<string> Grants { get; init; } = new string[0];
}

public sealed record CadPermissionV2
{
    public string Id { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
    public int? TemplateId { get; init; }
    public string? TemplateName { get; init; }
    public string Action { get; init; } = string.Empty;
}

public sealed record CadPermissionCatalogV2
{
    public int Version { get; init; } = 2;
    public string CommunityUuid { get; init; } = string.Empty;
    public IReadOnlyList<CadPermissionV2> Permissions { get; init; } = new CadPermissionV2[0];
    public IReadOnlyDictionary<string, IReadOnlyList<string>> LegacyGrants { get; init; } = new Dictionary<string, IReadOnlyList<string>>();
}

public sealed record CadAccountPermissionsV2
{
    public CadPermissionSetV2 Permissions { get; init; } = new();
    public bool Owner { get; init; }
    public bool Migrated { get; init; }
    public int Status { get; init; }
}

public sealed record CadReplaceAccountPermissionsV2Response
{
    public string AccountUuid { get; init; } = string.Empty;
    public CadPermissionSetV2 Permissions { get; init; } = new();
}
