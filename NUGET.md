# Sonoran.Net

`Sonoran.Net` is the C# package for Sonoran CAD integrations.

## Install

```sh
dotnet add package Sonoran.Net
```

## Example Configuration

```csharp
using Sonoran;

using var sonoran = new SonoranClient(new SonoranClientOptions
{
    apiKey = "your-cad-api-key",
    communityId = "your-community-id",
    apiUrl = "https://api.sonorancad.com",
    defaultServerId = 1,
    timeout = TimeSpan.FromSeconds(30)
});
```

## Example Usage

```csharp
var response = await sonoran.createEmergencyCallV2(new CreateEmergencyCallV2Request
{
    ServerId = 1,
    IsEmergency = true,
    Caller = "John Doe",
    Location = "101 Alta Street",
    Description = "Structure fire with visible smoke.",
    DeleteAfterMinutes = 30
});
```

The client automatically retries CAD v2 `429 Too Many Requests` responses up to 2 times and respects `Retry-After` when it is provided.

## Repository

[GitHub Repository](https://github.com/Sonoran-Software/Sonoran.Net)
