# Sonoran.Net

`Sonoran.Net` is the C# Sonoran CAD v2 SDK.

## Release Automation

Publishing is handled by GitHub Actions with NuGet trusted publishing.

Required one-time setup:

1. In nuget.org, create a trusted publishing policy for:
   - Repository Owner: `Sonoran-Software`
   - Repository: `Sonoran.Net`
   - NuGet Owner: `SonoranSoftware`
   - Workflow File: `release.yml`
2. In GitHub, add a repository variable named `NUGET_USER` with the nuget.org profile name that owns the package.

After that, every push to `master` will:

- compute the next patch version from the latest `v*` tag
- build and pack `Sonoran.Net`
- publish the package to nuget.org using OIDC
- create a Git tag and GitHub release for that version
