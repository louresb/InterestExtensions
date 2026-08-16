# Contributing

## Local development

Install the .NET 10 SDK. The solution builds the library for `netstandard2.0` and `net8.0`, and runs its tests on .NET 8 and .NET 10.

```powershell
dotnet tool restore
dotnet restore InterestExtension.sln --locked-mode
dotnet build InterestExtension.sln --configuration Release --no-restore
dotnet test InterestExtension.sln --configuration Release --no-build --settings coverlet.runsettings --collect:"XPlat Code Coverage"
dotnet pack InterestExtension/InterestExtension.csproj --configuration Release --no-build --no-restore
```

CI additionally restores `InterestExtension.PackageSmoke` from the newly created local package into an isolated package cache and runs it on .NET 8 and .NET 10. This verifies the actual `.nupkg`, its XML documentation, README, icon, and symbol package rather than only testing a project reference.

When intentionally updating a NuGet dependency, regenerate the lock files with:

```powershell
dotnet restore InterestExtension.sln --force-evaluate
```

The repository-level `NuGet.Config` clears machine-specific feeds and restores dependencies only from NuGet.org.

## Pull requests

- Add or update tests for behavior changes.
- Preserve the public API unless the change is explicitly planned for a major release.
- Update `README.md`, `MIGRATION.md`, and `CHANGELOG.md` when the public contract changes.
- Keep calculations unrounded; rounding rules belong to the consuming financial domain.

## Release policy

A push to `main` runs CI but never publishes a package.

To release a stable version:

1. Set the version in `InterestExtension.csproj` and update the changelog.
2. Merge a green pull request.
3. Create a `vMAJOR.MINOR.PATCH` tag that exactly matches the project version.
4. Publish a non-prerelease GitHub Release for that tag.
5. The protected `nuget.org` environment authenticates through NuGet Trusted Publishing and publishes the package.

The NuGet.org Trusted Publishing policy must match owner `louresb`, repository `InterestExtensions`, workflow file `release.yml`, and GitHub environment `nuget.org`. The repository variable `NUGET_USER` must contain the NuGet.org profile name, not an email address.

Duplicate package versions fail by design; the release workflow never uses `--skip-duplicate`.
