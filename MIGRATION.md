# Migrating from 2.x to 3.0

Version 3 preserves the package ID, assembly, `InterestExtension` namespace, `InterestCalculator` class, `InterestPeriod` enum, and all existing method signatures. The major version communicates intentional changes to validation and numerical behavior.

## Review invalid-input handling

Every public calculation now throws `ArgumentOutOfRangeException` when `principal`, `interestRate`, or `period` is negative.

Both compound overloads that accept `InterestPeriod` now throw `ArgumentOutOfRangeException` for undefined enum values. In 2.0.1, `CalculateCompoundInterestAmount` silently treated an undefined value as `Yearly`.

If an application intentionally sent invalid values, validate or normalize them before calling version 3.

## Review numerical assertions

Compound calculations no longer convert `decimal` values to `double` for `Math.Pow`. Version 3 uses decimal exponentiation, so the least significant decimal digits can differ from 2.x.

Avoid asserting a rounded display value against the raw result. Apply an explicit domain rule instead:

```csharp
decimal raw = principal.CalculateCompoundInterest(rate, years, InterestPeriod.Monthly);
decimal amount = decimal.Round(raw, 2, MidpointRounding.ToEven);
```

Choose `ToEven`, `AwayFromZero`, or another rule based on the financial product and jurisdiction; the library deliberately does not choose one.

## Confirm rate and period semantics

- `interestRate` is a nominal annual fractional rate (`0.05m` means 5% per year).
- `period` is a number of whole years.
- Monthly compounding uses 12 periods per year.
- Daily compounding uses 365 periods per year and does not inspect calendar dates.

These definitions clarify the formulas already used by 2.x; they do not introduce a new formula.

## Optional annual interest-only overload

Version 3 adds this convenience overload:

```csharp
decimal interest = principal.CalculateCompoundInterestAmount(rate, years);
```

It is equivalent to passing `InterestPeriod.Yearly`.
