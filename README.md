# Sonoran.Net

`Sonoran.Net` is the C# package for Sonoran CAD integrations.

This repository is being built to mirror the CAD v2 API surface exposed by the existing Sonoran Lua and JavaScript SDKs, with matching naming, response normalization, and release automation.

## Status

The package is in active development.

Current repository contents include:

- package metadata for NuGet
- shared response and configuration models
- automated version bump and NuGet release workflow

The full CAD v2 client surface is still being filled in.

## Installation

```sh
dotnet add package Sonoran.Net
```

## What The Package Exposes Today

### `SonoranClientOptions`

Use `SonoranClientOptions` to define the settings a Sonoran CAD client will use.

```csharp
using Sonoran;

var options = new SonoranClientOptions
{
    apiKey = "your-cad-api-key",
    communityId = "your-community-id",
    apiUrl = "https://api.sonorancad.com",
    defaultServerId = 1,
    timeout = TimeSpan.FromSeconds(30),
    headers = new Dictionary<string, string>
    {
        ["X-Trace-Id"] = Guid.NewGuid().ToString("N")
    }
};
```

Supported properties:

- `apiKey`
- `communityId`
- `apiUrl`
- `defaultServerId`
- `timeout`
- `headers`

### `SonoranResponse`

The SDK is designed around a normalized response shape:

```csharp
using Sonoran;
using System.Text.Json.Nodes;

var response = new SonoranResponse
{
    success = true,
    data = JsonNode.Parse("""{"ok":true}""")
};
```

Failure responses use `reason`:

```csharp
var failure = new SonoranResponse
{
    success = false,
    reason = "Request failed."
};
```

## Planned CAD v2 Surface

The target public API mirrors the other Sonoran CAD SDKs, including helpers such as:

- `getVersionV2()`
- `getInfoV2()`
- `getCharactersV2(...)`
- `createDispatchCallV2(...)`
- `setUnitStatusV2(...)`
- `getBlipsV2(...)`

That keeps naming and usage consistent across Lua, JavaScript, and C# integrations.

## NuGet Package Readme

NuGet package readme content is now embedded from `NUGET.md` during packing.

That means you should not set the package readme URL to:

- `https://github.com/.../blob/.../README.md`

If you ever need to provide an external markdown URL manually on nuget.org, it must be a raw GitHub URL instead:

- `https://raw.githubusercontent.com/...`

For this package, the preferred approach is the packaged readme, so no external readme URL is required.

## Release Automation

Publishing is handled by GitHub Actions with NuGet trusted publishing.

Current release workflow:

- any push to `master` triggers the workflow in `.github/workflows/build.yml`
- the workflow computes the next patch version from the latest `v*` tag
- the package is built and packed
- NuGet publish uses GitHub OIDC trusted publishing
- a matching Git tag and GitHub release are created automatically

Required one-time setup for maintainers:

1. In nuget.org, create a trusted publishing policy for:
   - Repository Owner: `Sonoran-Software`
   - Repository: `Sonoran.Net`
   - NuGet Owner: `SonoranSoftware`
   - Workflow File: `build.yml`
2. In GitHub, add a repository variable named `NUGET_USER` if you want to override the default NuGet owner.
