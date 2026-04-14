# Sonoran.Net

`Sonoran.Net` is the C# package for Sonoran CAD, CMS, and Radio integrations.

This repository is currently focused on mirroring the Sonoran CAD v2 API surface exposed by the existing Sonoran Lua and JavaScript SDKs, with matching naming, response normalization, and release automation.

Install it with:

```sh
dotnet add package Sonoran.Net
```

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
