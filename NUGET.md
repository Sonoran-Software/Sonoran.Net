# Sonoran.Net

`Sonoran.Net` is the C# package for Sonoran CAD integrations.

## Install

```sh
dotnet add package Sonoran.Net
```

## Current Status

This package is under active development.

The repository currently includes:

- NuGet package metadata
- shared configuration and response models
- automated GitHub Actions publishing with trusted publishing

## Example Configuration

```csharp
using Sonoran;

var options = new SonoranClientOptions
{
    apiKey = "your-cad-api-key",
    communityId = "your-community-id",
    apiUrl = "https://api.sonorancad.com",
    defaultServerId = 1,
    timeout = TimeSpan.FromSeconds(30)
};
```

## Response Shape

```csharp
using Sonoran;
using System.Text.Json.Nodes;

var success = new SonoranResponse
{
    success = true,
    data = JsonNode.Parse("""{"ok":true}""")
};

var failure = new SonoranResponse
{
    success = false,
    reason = "Request failed."
};
```

## Repository

[GitHub Repository](https://github.com/Sonoran-Software/Sonoran.Net)
