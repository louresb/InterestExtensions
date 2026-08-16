# Changelog

All notable changes to this project are documented in this file. The project follows [Semantic Versioning](https://semver.org/).

## [3.0.0] - 2026-08-16

### Added

- `netstandard2.0` package asset while retaining the optimized `net8.0` asset.
- .NET 8 and .NET 10 test execution.
- Annual overload for `CalculateCompoundInterestAmount`.
- XML API documentation, portable symbols, Source Link metadata, and `.snupkg` generation.
- Package validation against 2.0.1.
- Reproducible CI artifacts, coverage reporting, locked dependencies, and monthly Dependabot updates.
- Safe NuGet release workflow based on a published GitHub Release and Trusted Publishing.

### Changed

- Compound calculations now use decimal exponentiation instead of converting through `double`.
- Documentation now defines `interestRate` as a nominal annual fractional rate and `period` as whole years, matching the existing formulas.
- Daily compounding is explicitly documented as 365 compounding periods per year.
- Package version is now published only through the dedicated release workflow; pushes to `main` never publish.

### Fixed

- All public methods now reject negative principal, interest rate, and period values consistently.
- `CalculateCompoundInterestAmount` now rejects undefined `InterestPeriod` values instead of silently treating them as yearly.
- Package contents now include the XML documentation promised by the README.

### Breaking changes

- Invalid calls that previously returned a value can now throw `ArgumentOutOfRangeException`.
- Valid compound calculations can differ in their least significant decimal digits because the `double` conversion was removed.
- Consumers that depend on exact 2.x behavior must remain on 2.0.1 until they have reviewed these changes.

## [2.0.1] - 2025-05-25

- Fixed the build workflow and added Coverlet-based test coverage collection.

[3.0.0]: https://github.com/louresb/InterestExtensions/compare/v2.0.1...v3.0.0
[2.0.1]: https://github.com/louresb/InterestExtensions/tree/v2.0.1
