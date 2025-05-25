# InterestExtensions

[![NuGet Version](https://img.shields.io/nuget/v/InterestExtensions.svg?color=blue&label=NuGet%20Version)](https://www.nuget.org/packages/InterestExtensions)
[![NuGet Downloads](https://img.shields.io/nuget/dt/InterestExtensions.svg?color=orange&label=NuGet%20Downloads)](https://www.nuget.org/packages/InterestExtensions)
[![Build Status](https://github.com/louresb/InterestExtensions/actions/workflows/main.yml/badge.svg)](https://github.com/louresb/InterestExtensions/actions)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg)](https://github.com/louresb/InterestExtensions/blob/main/LICENSE)
[![Development Status](https://img.shields.io/badge/status-active-brightgreen.svg)](https://github.com/louresb/InterestExtensions)

This extension facilitates the calculation of both simple interest and compound interest.  

---

## Installation

To install the package via NuGet, you can use the following command:

```powershell
dotnet add package InterestExtensions
```

You can also visit the [NuGet package page](https://www.nuget.org/packages/InterestExtensions) for more information and to download the package.

---

## Features

- Simple and compound interest calculations
- Overloads for different compounding periods (yearly, monthly, daily)
- XML documentation for IntelliSense and API docs
- Thoroughly unit-tested
- .NET 8 and above supported

---

## Usage Examples

```csharp
using InterestExtension;
using InterestExtension.Enums;

decimal principal = 100m;
decimal interestRate = 0.004m;
int period = 7;

// Calculate simple interest (final amount)
decimal simple = principal.CalculateSimpleInterest(interestRate, period);

// Calculate simple interest amount (just the interest earned)
decimal simpleAmount = principal.CalculateSimpleInterestAmount(interestRate, period);

// Calculate compound interest (final amount, annual compounding)
decimal compound = principal.CalculateCompoundInterest(interestRate, period);

// Calculate compound interest (monthly compounding)
decimal compoundMonthly = principal.CalculateCompoundInterest(interestRate, period, InterestPeriod.Monthly);

// Calculate only the interest earned
decimal earned = principal.CalculateCompoundInterestAmount(interestRate, period, InterestPeriod.Daily);
```

---

## Contributing

Contributions are welcome! If you encounter any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.

---

## License

[MIT License](https://github.com/louresb/InterestExtensions/blob/main/LICENSE) © [Bruno Loures](https://github.com/louresb)
