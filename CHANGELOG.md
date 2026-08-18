# Changelog

## [3.0.0] - 2026-08-18

- Added .NET Standard 2.0 support.
- Added annual interest-only calculations and custom compounding periods.
- Compound calculations now use decimal arithmetic throughout.
- Negative inputs and unsupported `InterestPeriod` values now throw `ArgumentOutOfRangeException`.

**Breaking:** invalid-input behavior changed, and compound results can differ from 2.x in their least significant decimal digits.

## [2.0.1] - 2025-05-25

- Fixed the build workflow and added Coverlet-based test coverage collection.

[3.0.0]: https://github.com/louresb/InterestExtensions/compare/v2.0.1...v3.0.0
[2.0.1]: https://github.com/louresb/InterestExtensions/tree/v2.0.1
