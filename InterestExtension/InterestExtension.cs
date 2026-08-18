using InterestExtension.Enums;

namespace InterestExtension;

/// <summary>
/// Provides extension methods for calculating simple and compound interest.
/// </summary>
public static class InterestCalculator
{
    private const int MonthsPerYear = 12;
    private const int DaysPerYear = 365;

    /// <summary>
    /// Calculates the total amount after applying simple interest.
    /// </summary>
    /// <param name="principal">The initial principal amount.</param>
    /// <param name="interestRate">The annual interest rate expressed as a fraction (for example, 0.05 for 5%).</param>
    /// <param name="period">The number of whole years.</param>
    /// <returns>The total amount after applying simple interest.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="principal"/>, <paramref name="interestRate"/>, or <paramref name="period"/> is negative.
    /// </exception>
    /// <exception cref="OverflowException">Thrown when the result exceeds the range of <see cref="decimal"/>.</exception>
    /// <remarks>The result is not rounded. Apply the rounding policy required by your financial domain.</remarks>
    public static decimal CalculateSimpleInterest(this decimal principal, decimal interestRate, int period)
    {
        ValidateInputs(principal, interestRate, period);

        if (principal == 0 || interestRate == 0 || period == 0)
        {
            return principal;
        }

        return principal + (principal * interestRate * period);
    }

    /// <summary>
    /// Calculates the total amount after applying compound interest compounded annually.
    /// </summary>
    /// <param name="principal">The initial principal amount.</param>
    /// <param name="interestRate">The annual interest rate expressed as a fraction (for example, 0.05 for 5%).</param>
    /// <param name="period">The number of whole years.</param>
    /// <returns>The total amount after applying compound interest.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="principal"/>, <paramref name="interestRate"/>, or <paramref name="period"/> is negative.
    /// </exception>
    /// <exception cref="OverflowException">Thrown when the result exceeds the range of <see cref="decimal"/>.</exception>
    /// <remarks>The result is not rounded. Apply the rounding policy required by your financial domain.</remarks>
    public static decimal CalculateCompoundInterest(this decimal principal, decimal interestRate, int period)
        => principal.CalculateCompoundInterest(interestRate, period, InterestPeriod.Yearly);

    /// <summary>
    /// Calculates the total amount after applying compound interest with a specified compounding frequency.
    /// </summary>
    /// <param name="principal">The initial principal amount.</param>
    /// <param name="interestRate">The nominal annual interest rate expressed as a fraction (for example, 0.05 for 5%).</param>
    /// <param name="period">The number of whole years.</param>
    /// <param name="periodType">How often interest is compounded within each year.</param>
    /// <returns>The total amount after applying compound interest.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="principal"/>, <paramref name="interestRate"/>, or <paramref name="period"/> is negative,
    /// or when <paramref name="periodType"/> is not a defined <see cref="InterestPeriod"/> value.
    /// </exception>
    /// <exception cref="OverflowException">Thrown when the number of compounding periods or the result exceeds the supported range.</exception>
    /// <remarks>
    /// Daily compounding uses a fixed 365-day year. The result is not rounded; apply the rounding policy required by your financial domain.
    /// </remarks>
    public static decimal CalculateCompoundInterest(
        this decimal principal,
        decimal interestRate,
        int period,
        InterestPeriod periodType)
    {
        ValidateInputs(principal, interestRate, period);

        var periodsPerYear = GetPeriodsPerYear(periodType);

        if (principal == 0 || interestRate == 0 || period == 0)
        {
            return principal;
        }

        return principal * CalculateCompoundFactor(interestRate, period, periodsPerYear);
    }

    /// <summary>
    /// Calculates only the simple interest earned, excluding the principal.
    /// </summary>
    /// <param name="principal">The initial principal amount.</param>
    /// <param name="interestRate">The annual interest rate expressed as a fraction (for example, 0.05 for 5%).</param>
    /// <param name="period">The number of whole years.</param>
    /// <returns>The interest earned.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="principal"/>, <paramref name="interestRate"/>, or <paramref name="period"/> is negative.
    /// </exception>
    /// <exception cref="OverflowException">Thrown when the result exceeds the range of <see cref="decimal"/>.</exception>
    /// <remarks>The result is not rounded. Apply the rounding policy required by your financial domain.</remarks>
    public static decimal CalculateSimpleInterestAmount(this decimal principal, decimal interestRate, int period)
    {
        ValidateInputs(principal, interestRate, period);

        if (principal == 0 || interestRate == 0 || period == 0)
        {
            return 0m;
        }

        return principal * interestRate * period;
    }

    /// <summary>
    /// Calculates only the compound interest earned with annual compounding, excluding the principal.
    /// </summary>
    /// <param name="principal">The initial principal amount.</param>
    /// <param name="interestRate">The annual interest rate expressed as a fraction (for example, 0.05 for 5%).</param>
    /// <param name="period">The number of whole years.</param>
    /// <returns>The interest earned.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="principal"/>, <paramref name="interestRate"/>, or <paramref name="period"/> is negative.
    /// </exception>
    /// <exception cref="OverflowException">Thrown when the result exceeds the range of <see cref="decimal"/>.</exception>
    /// <remarks>The result is not rounded. Apply the rounding policy required by your financial domain.</remarks>
    public static decimal CalculateCompoundInterestAmount(this decimal principal, decimal interestRate, int period)
        => principal.CalculateCompoundInterestAmount(interestRate, period, InterestPeriod.Yearly);

    /// <summary>
    /// Calculates only the compound interest earned, excluding the principal.
    /// </summary>
    /// <param name="principal">The initial principal amount.</param>
    /// <param name="interestRate">The nominal annual interest rate expressed as a fraction (for example, 0.05 for 5%).</param>
    /// <param name="period">The number of whole years.</param>
    /// <param name="periodType">How often interest is compounded within each year.</param>
    /// <returns>The interest earned.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="principal"/>, <paramref name="interestRate"/>, or <paramref name="period"/> is negative,
    /// or when <paramref name="periodType"/> is not a defined <see cref="InterestPeriod"/> value.
    /// </exception>
    /// <exception cref="OverflowException">Thrown when the number of compounding periods or the result exceeds the supported range.</exception>
    /// <remarks>
    /// Daily compounding uses a fixed 365-day year. The result is not rounded; apply the rounding policy required by your financial domain.
    /// </remarks>
    public static decimal CalculateCompoundInterestAmount(
        this decimal principal,
        decimal interestRate,
        int period,
        InterestPeriod periodType)
    {
        ValidateInputs(principal, interestRate, period);

        var periodsPerYear = GetPeriodsPerYear(periodType);

        if (principal == 0 || interestRate == 0 || period == 0)
        {
            return 0m;
        }

        return principal * (CalculateCompoundFactor(interestRate, period, periodsPerYear) - 1m);
    }

    private static void ValidateInputs(decimal principal, decimal interestRate, int period)
    {
        if (principal < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(principal), principal, "Principal cannot be negative.");
        }

        if (interestRate < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(interestRate), interestRate, "Interest rate cannot be negative.");
        }

        if (period < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(period), period, "Period cannot be negative.");
        }
    }

    private static int GetPeriodsPerYear(InterestPeriod periodType)
        => periodType switch
        {
            InterestPeriod.Yearly => 1,
            InterestPeriod.Monthly => MonthsPerYear,
            InterestPeriod.Daily => DaysPerYear,
            _ => throw new ArgumentOutOfRangeException(nameof(periodType), periodType, "Invalid interest period.")
        };

    private static decimal CalculateCompoundFactor(decimal interestRate, int period, int periodsPerYear)
    {
        var totalPeriods = checked(periodsPerYear * period);
        var periodicRate = interestRate / periodsPerYear;

        return Pow(1m + periodicRate, totalPeriods);
    }

    private static decimal Pow(decimal value, int exponent)
    {
        var result = 1m;

        while (exponent > 0)
        {
            if ((exponent & 1) == 1)
            {
                result *= value;
            }

            exponent >>= 1;

            if (exponent > 0)
            {
                value *= value;
            }
        }

        return result;
    }
}
