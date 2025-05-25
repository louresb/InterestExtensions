using InterestExtension.Enums;

namespace InterestExtension;

/// <summary>
/// Provides extension methods for calculating simple and compound interest.
/// </summary>
public static class InterestCalculator
{
    /// <summary>
    /// Calculates the total amount after applying simple interest.
    /// </summary>
    /// <param name="principal">The initial principal amount.</param>
    /// <param name="interestRate">The interest rate per period (e.g., 0.05 for 5%).</param>
    /// <param name="period">The number of periods.</param>
    /// <returns>The total amount after applying simple interest.</returns>
    public static decimal CalculateSimpleInterest(this decimal principal, decimal interestRate, int period)
    {
        if (principal < 0) throw new ArgumentOutOfRangeException(nameof(principal), "Principal cannot be negative.");
        if (interestRate < 0) throw new ArgumentOutOfRangeException(nameof(interestRate), "Interest rate cannot be negative.");
        if (period < 0) throw new ArgumentOutOfRangeException(nameof(period), "Period cannot be negative.");

        return principal + (principal * interestRate * period);
    }

    /// <summary>
    /// Calculates the total amount after applying compound interest compounded annually.
    /// </summary>
    /// <param name="principal">The initial principal amount.</param>
    /// <param name="interestRate">The interest rate per period (e.g., 0.05 for 5%).</param>
    /// <param name="period">The number of periods.</param>
    /// <returns>The total amount after applying compound interest.</returns>
    public static decimal CalculateCompoundInterest(this decimal principal, decimal interestRate, int period)
    {
        if (principal < 0) throw new ArgumentOutOfRangeException(nameof(principal));
        if (interestRate < 0) throw new ArgumentOutOfRangeException(nameof(interestRate));
        if (period < 0) throw new ArgumentOutOfRangeException(nameof(period));

        return principal * (decimal)Math.Pow(1 + (double)interestRate, period);
    }

    /// <summary>
    /// Calculates the total amount after applying compound interest with a specified compounding period.
    /// </summary>
    /// <param name="principal">The initial principal amount.</param>
    /// <param name="interestRate">The interest rate per period (e.g., 0.05 for 5%).</param>
    /// <param name="period">The number of periods.</param>
    /// <param name="periodType">The compounding period type (Yearly, Monthly, Daily).</param>
    /// <returns>The total amount after applying compound interest.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when an invalid period type is provided.</exception>
    public static decimal CalculateCompoundInterest(
        this decimal principal,
        decimal interestRate,
        int period,
        InterestPeriod periodType)
    {
        if (principal < 0) throw new ArgumentOutOfRangeException(nameof(principal));
        if (interestRate < 0) throw new ArgumentOutOfRangeException(nameof(interestRate));
        if (period < 0) throw new ArgumentOutOfRangeException(nameof(period));

        var n = periodType switch
        {
            InterestPeriod.Yearly => 1,
            InterestPeriod.Monthly => 12,
            InterestPeriod.Daily => 365,
            _ => throw new ArgumentOutOfRangeException(nameof(periodType), "Invalid interest period.")
        };

        return principal * (decimal)Math.Pow(1 + ((double)interestRate / n), n * period);
    }

    /// <summary>
    /// Calculates only the simple interest earned (not the final amount).
    /// </summary>
    /// <param name="principal">The initial principal amount.</param>
    /// <param name="interestRate">The interest rate per period.</param>
    /// <param name="period">The number of periods.</param>
    /// <returns>The interest earned.</returns>
    public static decimal CalculateSimpleInterestAmount(this decimal principal, decimal interestRate, int period) =>
        principal * interestRate * period;

    /// <summary>
    /// Calculates only the compound interest earned (not the final amount).
    /// </summary>
    /// <param name="principal">The initial principal amount.</param>
    /// <param name="interestRate">The interest rate per period.</param>
    /// <param name="period">The number of periods.</param>
    /// <param name="periodType">The compounding period type.</param>
    /// <returns>The interest earned.</returns>
    public static decimal CalculateCompoundInterestAmount(
        this decimal principal,
        decimal interestRate,
        int period,
        InterestPeriod periodType)
    {
        int n = periodType switch
        {
            InterestPeriod.Yearly => 1,
            InterestPeriod.Monthly => 12,
            InterestPeriod.Daily => 365,
            _ => 1
        };
        return principal * (decimal)Math.Pow(1 + ((double)interestRate / n), n * period) - principal;
    }
}