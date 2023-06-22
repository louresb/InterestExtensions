# InterestExtensions

[![NuGet Version](https://img.shields.io/nuget/v/InterestExtensions.svg?color=blue&label=NuGet%20Version)](https://www.nuget.org/packages/InterestExtensions)
[![NuGet Downloads](https://img.shields.io/nuget/dt/InterestExtensions.svg?color=orange&label=NuGet%20Downloads)](https://www.nuget.org/packages/InterestExtensions)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg)](https://github.com/louresb/InterestExtensions/blob/main/LICENSE)
[![Development Status](https://img.shields.io/badge/status-active-brightgreen.svg)](https://github.com/louresb/InterestExtensions)

This extension facilitates the calculation of both simple interest and compound interest.

### Usage Examples

Here are some examples of how to use the extension:

```csharp
decimal principal = 100;
decimal interestRate = 0.004m;
int period = 7;

// Calculate simple interest
decimal simpleInterest = principal.CalculateSimpleInterest(interestRate, period);
Console.WriteLine($"Simple interest: {simpleInterest}");

// Calculate compound interest
decimal compoundInterest = principal.CalculateCompoundInterest(interestRate, period);
Console.WriteLine($"Compound interest: {compoundInterest}"); 
```

### Installation
To install the package via NuGet, you can use the following command:

```powershell

dotnet add package InterestExtensions

```

You can also visit the [NuGet package page](https://www.nuget.org/packages/InterestExtensions) for more information and to download the package.

### Contribution
Contributions are welcome! If you encounter any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.

## License
[MIT License](https://github.com/louresb/InterestExtensions/blob/main/LICENSE) © [Bruno Loures](https://github.com/louresb)
<br />
<br />

<details>
  <summary>Credits</summary>
  Icon - <a href="https://www.flaticon.com/free-icon/tax_5772764?related_id=5772747&origin=search">Freepik - Flaticon</a>
</details>
