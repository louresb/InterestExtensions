# InterestExtensions

[![NuGet Version](https://img.shields.io/nuget/v/InterestExtensions.svg?color=blue&label=NuGet%20Version)](https://www.nuget.org/packages/InterestExtensions)
[![NuGet Downloads](https://img.shields.io/nuget/dt/InterestExtensions.svg?color=orange&label=NuGet%20Downloads)](https://www.nuget.org/packages/InterestExtensions)
[![CI](https://github.com/louresb/InterestExtensions/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/louresb/InterestExtensions/actions/workflows/ci.yml?query=branch%3Amain)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg)](https://github.com/louresb/InterestExtensions/blob/v3.0.0/LICENSE)

Small, dependency-free extension methods for simple and compound interest calculations.

## Installation

```powershell
dotnet add package InterestExtensions
```

## Supported platforms

| Package asset | Intended consumers |
| --- | --- |
| `netstandard2.0` | .NET implementations that support .NET Standard 2.0 |
| `net8.0` | .NET 8 and later, including .NET 10 |

The package is built as `netstandard2.0;net8.0` and tested on both .NET 8 and .NET 10. A separate `net10.0` assembly is unnecessary because .NET 10 consumes the compatible `net8.0` asset.

## Calculation contract

- `principal` is a non-negative decimal amount.
- `interestRate` is a non-negative nominal annual rate expressed as a fraction: use `0.05m` for 5% per year.
- `period` is a non-negative number of whole years.
- `Yearly`, `Monthly`, and `Daily` compound 1, 12, and 365 times per year respectively.
- Daily compounding uses a fixed 365-day year; it is not a date-based day-count convention.
- Results are returned without implicit rounding. Choose the scale and midpoint rule required by your currency and domain.
- Invalid negative inputs or an unknown `InterestPeriod` throw `ArgumentOutOfRangeException`.
- Calculations that exceed the range of `decimal` throw `OverflowException`.

The formulas are:

```text
simple total   = principal × (1 + annual rate × years)
compound total = principal × (1 + annual rate / frequency)^(frequency × years)
```

## Usage

```csharp
using InterestExtension;
using InterestExtension.Enums;

decimal principal = 1_000m;
decimal annualRate = 0.05m; // 5% per year
int years = 3;

decimal simpleTotal = principal.CalculateSimpleInterest(annualRate, years);
decimal simpleInterest = principal.CalculateSimpleInterestAmount(annualRate, years);

decimal annualTotal = principal.CalculateCompoundInterest(annualRate, years);
decimal annualInterest = principal.CalculateCompoundInterestAmount(annualRate, years);

decimal monthlyTotal = principal.CalculateCompoundInterest(
    annualRate,
    years,
    InterestPeriod.Monthly);

decimal dailyInterest = principal.CalculateCompoundInterestAmount(
    annualRate,
    years,
    InterestPeriod.Daily);

// InterestExtensions does not choose a financial rounding policy for you.
decimal displayAmount = decimal.Round(monthlyTotal, 2, MidpointRounding.ToEven);
```

## API overview

| Method | Result |
| --- | --- |
| `CalculateSimpleInterest` | Principal plus simple interest |
| `CalculateSimpleInterestAmount` | Simple interest only |
| `CalculateCompoundInterest` | Principal plus compound interest |
| `CalculateCompoundInterestAmount` | Compound interest only |

The compound methods have an annual overload and an overload that accepts `InterestPeriod`.

## Version 3

Version 3 keeps the existing namespace, class, enum, and method signatures while making the calculation contract consistent. It validates all methods uniformly, rejects unknown enum values, adds the annual `CalculateCompoundInterestAmount` overload, and uses deterministic decimal exponentiation instead of converting through `double`.

Existing 2.x consumers should read the [migration guide](https://github.com/louresb/InterestExtensions/blob/v3.0.0/MIGRATION.md) before opting into 3.0.0. See the [changelog](https://github.com/louresb/InterestExtensions/blob/v3.0.0/CHANGELOG.md) for the complete release notes.

## Scope and limitations

InterestExtensions is a small mathematical utility, not a regulatory or accounting engine. It does not model dates, leap years, 30/360 or Actual/Actual conventions, fees, taxes, variable rates, currencies, or product-specific rounding rules.

## Contributing

Contributions are welcome. See the [contribution guide](https://github.com/louresb/InterestExtensions/blob/v3.0.0/CONTRIBUTING.md) for the local workflow and release policy.

## License

Licensed under the [MIT License](https://github.com/louresb/InterestExtensions/blob/v3.0.0/LICENSE). 
