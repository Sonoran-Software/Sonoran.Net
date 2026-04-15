# Sonoran.Net

`Sonoran.Net` is the C# package for Sonoran CAD, CMS, and Radio integrations.

This repository is currently focused on a typed Sonoran CAD v2 client that mirrors the Lua and JavaScript SDK helper names while using C# request/query models instead of raw JSON strings.

Install it with:

```sh
dotnet add package Sonoran.Net
```

## Status

The package is in active development.

Current repository contents include:

- a typed CAD v2 client
- strongly typed request/query models for CAD v2 helpers
- normalized `SonoranResponse` handling
- automatic retry handling for CAD v2 `429` responses
- automated version bump and NuGet release workflow

## Example Usage

```csharp
using Sonoran;

using var sonoran = new SonoranClient(new SonoranClientOptions
{
    product = SonoranProduct.CAD,
    apiKey = "your-cad-api-key",
    communityId = "your-community-id",
    defaultServerId = 1
});

var version = await sonoran.Cad.getVersionV2();

var emergencyCall = await sonoran.Cad.createEmergencyCallV2(new CreateEmergencyCallV2Request
{
    ServerId = 1,
    IsEmergency = true,
    Caller = "John Doe",
    Location = "101 Alta Street",
    Description = "Structure fire with visible smoke.",
    DeleteAfterMinutes = 30
});
```

## Core Types

### `SonoranClientOptions`

```csharp
var options = new SonoranClientOptions
{
    product = SonoranProduct.CAD,
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

- `product`
- `apiKey`
- `communityId`
- `apiUrl`
- `defaultServerId`
- `timeout`
- `headers`

### `SonoranResponse`

Every public CAD v2 helper returns a normalized response:

```csharp
if (emergencyCall.success)
{
    Console.WriteLine(emergencyCall.data);
}
else
{
    Console.WriteLine(emergencyCall.reason);
}
```

The client automatically retries CAD v2 `429 Too Many Requests` responses up to 2 times and respects `Retry-After` when it is provided.

## CAD v2 Surface

The CAD surface is available under `sonoran.Cad.*`. Root-level helpers are still available for backward compatibility.

The client mirrors the current CAD v2 helper names, including methods such as:

- `getVersionV2()`
- `getInfoV2()`
- `getCharactersV2(...)`
- `createEmergencyCallV2(...)`
- `createDispatchCallV2(...)`
- `setUnitStatusV2(...)`
- `getBlipsV2(...)`

That keeps usage consistent across Lua, JavaScript, and C# while still using typed request models where C# benefits from them.
