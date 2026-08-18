# Contributing

## Local development

Install the .NET 10 SDK, then run:

```powershell
dotnet tool restore
dotnet restore InterestExtension.sln --locked-mode
dotnet build InterestExtension.sln --configuration Release --no-restore
dotnet test InterestExtension.sln --configuration Release --no-build --settings coverlet.runsettings --collect:"XPlat Code Coverage"
dotnet pack InterestExtension/InterestExtension.csproj --configuration Release --no-build --no-restore
```

## Pull requests

- Add or update tests for behavior changes.
- Preserve the public API unless the change is planned for a major release.
- Update the README and changelog when the public contract changes.
- Keep calculations unrounded; rounding rules belong to the consuming domain.

## Releasing

After merging a green pull request, tag the commit on `main` with the version declared in `InterestExtension.csproj` and push the tag:

```powershell
git switch main
git pull --ff-only
$version = dotnet msbuild InterestExtension/InterestExtension.csproj -nologo -getProperty:PackageVersion
git tag "v$version"
git push origin "v$version"
```

The tag starts the workflow that validates and publishes the package through NuGet Trusted Publishing. Duplicate or mismatched versions fail without publishing.
