namespace InterestExtension.Tests;

[TestClass]
public sealed class CustomCompoundingPeriodTests
{
    [TestMethod]
    public void CalculateCompoundInterestForPeriodsReturnsExactResult()
        => Assert.AreEqual(
            1196.1474756866648607810499868m,
            1000m.CalculateCompoundInterestForPeriods(
                0.12m,
                compoundingPeriodCount: 18,
                compoundingPeriodsPerYear: 12));

    [TestMethod]
    public void CustomMonthlyPeriodsMatchWholeYearOverload()
    {
        var customTotal = 750m.CalculateCompoundInterestForPeriods(0.08m, 36, 12);
        var customInterest = 750m.CalculateCompoundInterestAmountForPeriods(0.08m, 36, 12);
        var monthlyTotal = 750m.CalculateCompoundInterest(0.08m, 3, Enums.InterestPeriod.Monthly);
        var monthlyInterest = 750m.CalculateCompoundInterestAmount(0.08m, 3, Enums.InterestPeriod.Monthly);

        Assert.AreEqual(monthlyTotal, customTotal);
        Assert.AreEqual(monthlyInterest, customInterest);
    }

    [TestMethod]
    public void CustomFrequencySupportsNonPresetPeriods()
    {
        var quarterly = 1000m.CalculateCompoundInterestForPeriods(0.08m, 8, 4);
        var semiannual = 1000m.CalculateCompoundInterestForPeriods(0.08m, 4, 2);

        Assert.AreEqual(1171.6593810022656m, quarterly);
        Assert.AreEqual(1169.85856m, semiannual);
    }

    [TestMethod]
    public void InterestAmountEqualsTotalMinusPrincipal()
    {
        const decimal principal = 1000m;
        var total = principal.CalculateCompoundInterestForPeriods(0.12m, 18, 12);
        var interest = principal.CalculateCompoundInterestAmountForPeriods(0.12m, 18, 12);

        Assert.AreEqual(total - principal, interest);
    }

    [TestMethod]
    public void ZeroValueIdentitiesReturnWithoutRoundingOrOverflow()
    {
        Assert.AreEqual(decimal.MaxValue, decimal.MaxValue.CalculateCompoundInterestForPeriods(0m, int.MaxValue, 1));
        Assert.AreEqual(0m, decimal.MaxValue.CalculateCompoundInterestAmountForPeriods(0m, int.MaxValue, 1));

        Assert.AreEqual(1m, 1m.CalculateCompoundInterestForPeriods(decimal.MaxValue, 0, 1));
        Assert.AreEqual(0m, 1m.CalculateCompoundInterestAmountForPeriods(decimal.MaxValue, 0, 1));

        Assert.AreEqual(0m, 0m.CalculateCompoundInterestForPeriods(decimal.MaxValue, int.MaxValue, 1));
        Assert.AreEqual(0m, 0m.CalculateCompoundInterestAmountForPeriods(decimal.MaxValue, int.MaxValue, 1));
    }

    [TestMethod]
    public void ResultsAreNotRoundedByTheLibrary()
    {
        var result = 1000m.CalculateCompoundInterestForPeriods(0.12m, 18, 12);

        Assert.AreNotEqual(decimal.Round(result, 2), result);
    }
}
