# Sonoran.Net

`Sonoran.Net` is the C# package for Sonoran CAD, CMS, and Radio integrations.

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
