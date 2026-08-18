# InterestExtensions

[![NuGet Version](https://img.shields.io/nuget/v/InterestExtensions.svg?color=blue&label=NuGet%20Version)](https://www.nuget.org/packages/InterestExtensions)
[![NuGet Downloads](https://img.shields.io/nuget/dt/InterestExtensions.svg?color=orange&label=NuGet%20Downloads)](https://www.nuget.org/packages/InterestExtensions)
[![CI](https://github.com/louresb/InterestExtensions/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/louresb/InterestExtensions/actions/workflows/ci.yml?query=branch%3Amain)

Small, dependency-free extension methods for simple and compound interest calculations.

## Installation

```powershell
dotnet add package InterestExtensions
```

## Compatibility

Targets .NET Standard 2.0 and .NET 8. Tested on .NET 8 and .NET 10.

## Calculation contract

- `principal` is a non-negative decimal amount.
- `interestRate` is a non-negative nominal annual rate expressed as a fraction: use `0.05m` for 5% per year.
- `period` is a non-negative number of whole years.
- `compoundingPeriodCount` is the total number of times interest is applied.
- `compoundingPeriodsPerYear` is a positive frequency used to derive the periodic rate.
- `Yearly`, `Monthly`, and `Daily` compound 1, 12, and 365 times per year respectively.
- Daily compounding uses a fixed 365-day year; it is not a date-based day-count convention.
- Results are returned without implicit rounding. Choose the scale and midpoint rule required by your currency and domain.
- Invalid inputs throw `ArgumentOutOfRangeException`; calculations outside the range of `decimal` throw `OverflowException`.

The formulas are:

```text
simple total   = principal × (1 + annual rate × years)
periodic rate  = annual rate / periods per year
compound total = principal × (1 + periodic rate)^period count
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

// 18 monthly compounding periods, equivalent to 18 months.
decimal eighteenMonthTotal = principal.CalculateCompoundInterestForPeriods(
    annualRate,
    compoundingPeriodCount: 18,
    compoundingPeriodsPerYear: 12);

decimal eighteenMonthInterest = principal.CalculateCompoundInterestAmountForPeriods(
    annualRate,
    compoundingPeriodCount: 18,
    compoundingPeriodsPerYear: 12);

// InterestExtensions does not choose a financial rounding policy for you.
decimal displayAmount = decimal.Round(monthlyTotal, 2, MidpointRounding.ToEven);
```

## API overview

| Method | Result |
| --- | --- |
| `CalculateSimpleInterest` | Principal plus simple interest |
| `CalculateSimpleInterestAmount` | Simple interest only |
| `CalculateCompoundInterest` | Principal plus compound interest for whole years |
| `CalculateCompoundInterestAmount` | Compound interest only for whole years |
| `CalculateCompoundInterestForPeriods` | Principal plus compound interest for a custom period count and frequency |
| `CalculateCompoundInterestAmountForPeriods` | Compound interest only for a custom period count and frequency |

## Scope

InterestExtensions provides deterministic simple and compound interest calculations using `decimal`. Date-based calculations and product-specific financial rules are intentionally out of scope.

## Contributing

Contributions are welcome. See the [contribution guide](https://github.com/louresb/InterestExtensions/blob/main/CONTRIBUTING.md).
