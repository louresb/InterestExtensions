using InterestExtension.Enums;

namespace InterestExtension.Tests;

[TestClass]
public sealed class ValidationTests
{
    [TestMethod]
    public void EveryCalculationRejectsNegativePrincipal()
        => AssertAllThrowForParameter(
            "principal",
            () => (-1m).CalculateSimpleInterest(0.05m, 1),
            () => (-1m).CalculateSimpleInterestAmount(0.05m, 1),
            () => (-1m).CalculateCompoundInterest(0.05m, 1),
            () => (-1m).CalculateCompoundInterest(0.05m, 1, InterestPeriod.Monthly),
            () => (-1m).CalculateCompoundInterestForPeriods(0.05m, 1, 12),
            () => (-1m).CalculateCompoundInterestAmount(0.05m, 1),
            () => (-1m).CalculateCompoundInterestAmount(0.05m, 1, InterestPeriod.Daily),
            () => (-1m).CalculateCompoundInterestAmountForPeriods(0.05m, 1, 12));

    [TestMethod]
    public void EveryCalculationRejectsNegativeInterestRate()
        => AssertAllThrowForParameter(
            "interestRate",
            () => 100m.CalculateSimpleInterest(-0.05m, 1),
            () => 100m.CalculateSimpleInterestAmount(-0.05m, 1),
            () => 100m.CalculateCompoundInterest(-0.05m, 1),
            () => 100m.CalculateCompoundInterest(-0.05m, 1, InterestPeriod.Monthly),
            () => 100m.CalculateCompoundInterestForPeriods(-0.05m, 1, 12),
            () => 100m.CalculateCompoundInterestAmount(-0.05m, 1),
            () => 100m.CalculateCompoundInterestAmount(-0.05m, 1, InterestPeriod.Daily),
            () => 100m.CalculateCompoundInterestAmountForPeriods(-0.05m, 1, 12));

    [TestMethod]
    public void EveryCalculationRejectsNegativePeriod()
        => AssertAllThrowForParameter(
            "period",
            () => 100m.CalculateSimpleInterest(0.05m, -1),
            () => 100m.CalculateSimpleInterestAmount(0.05m, -1),
            () => 100m.CalculateCompoundInterest(0.05m, -1),
            () => 100m.CalculateCompoundInterest(0.05m, -1, InterestPeriod.Monthly),
            () => 100m.CalculateCompoundInterestAmount(0.05m, -1),
            () => 100m.CalculateCompoundInterestAmount(0.05m, -1, InterestPeriod.Daily));

    [TestMethod]
    [DataRow(-1)]
    [DataRow(999)]
    public void CompoundCalculationsRejectUnknownPeriodType(int value)
    {
        var periodType = (InterestPeriod)value;

        AssertAllThrowForParameter(
            "periodType",
            () => 100m.CalculateCompoundInterest(0.05m, 1, periodType),
            () => 100m.CalculateCompoundInterestAmount(0.05m, 1, periodType));
    }

    [TestMethod]
    public void CustomCompoundCalculationsRejectNegativePeriodCount()
        => AssertAllThrowForParameter(
            "compoundingPeriodCount",
            () => 100m.CalculateCompoundInterestForPeriods(0.05m, -1, 12),
            () => 100m.CalculateCompoundInterestAmountForPeriods(0.05m, -1, 12));

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void CustomCompoundCalculationsRejectInvalidFrequency(int compoundingPeriodsPerYear)
        => AssertAllThrowForParameter(
            "compoundingPeriodsPerYear",
            () => 100m.CalculateCompoundInterestForPeriods(0.05m, 1, compoundingPeriodsPerYear),
            () => 100m.CalculateCompoundInterestAmountForPeriods(0.05m, 1, compoundingPeriodsPerYear));

    [TestMethod]
    public void ZeroValueIdentitiesStillRejectInvalidCustomFrequency()
        => AssertAllThrowForParameter(
            "compoundingPeriodsPerYear",
            () => 0m.CalculateCompoundInterestForPeriods(decimal.MaxValue, 0, 0),
            () => 0m.CalculateCompoundInterestAmountForPeriods(decimal.MaxValue, 0, 0));

    [TestMethod]
    public void ZeroValueIdentitiesStillRejectUnknownPeriodType()
    {
        var periodType = (InterestPeriod)999;

        AssertAllThrowForParameter(
            "periodType",
            () => 0m.CalculateCompoundInterest(decimal.MaxValue, 0, periodType),
            () => 0m.CalculateCompoundInterestAmount(decimal.MaxValue, 0, periodType));
    }

    [TestMethod]
    public void DailyCompoundingRejectsPeriodCountOverflow()
        => Assert.ThrowsExactly<OverflowException>(
            () => 100m.CalculateCompoundInterest(0.05m, int.MaxValue, InterestPeriod.Daily));

    [TestMethod]
    public void CalculationRejectsDecimalOverflow()
    {
        Assert.ThrowsExactly<OverflowException>(
            () => decimal.MaxValue.CalculateCompoundInterest(1m, 1));
        Assert.ThrowsExactly<OverflowException>(
            () => decimal.MaxValue.CalculateCompoundInterestForPeriods(1m, 1, 1));
        Assert.ThrowsExactly<OverflowException>(
            () => decimal.MaxValue.CalculateCompoundInterestAmountForPeriods(decimal.MaxValue, 1, 1));
    }

    [TestMethod]
    public void InterestOnlyCalculationsDoNotAddPrincipalBeforeReturning()
    {
        Assert.AreEqual(decimal.MaxValue * 0.5m, decimal.MaxValue.CalculateSimpleInterestAmount(0.5m, 1));
        Assert.AreEqual(decimal.MaxValue, decimal.MaxValue.CalculateCompoundInterestAmount(1m, 1));
    }

    [TestMethod]
    public void ZeroValueIdentitiesShortCircuitBeforeOverflow()
    {
        Assert.AreEqual(decimal.MaxValue, decimal.MaxValue.CalculateSimpleInterest(decimal.MaxValue, 0));
        Assert.AreEqual(0m, decimal.MaxValue.CalculateSimpleInterestAmount(decimal.MaxValue, 0));

        Assert.AreEqual(1m, 1m.CalculateCompoundInterest(decimal.MaxValue, 0, InterestPeriod.Daily));
        Assert.AreEqual(0m, 1m.CalculateCompoundInterestAmount(decimal.MaxValue, 0, InterestPeriod.Daily));

        Assert.AreEqual(decimal.MaxValue, decimal.MaxValue.CalculateCompoundInterest(0m, int.MaxValue, InterestPeriod.Daily));
        Assert.AreEqual(0m, decimal.MaxValue.CalculateCompoundInterestAmount(0m, int.MaxValue, InterestPeriod.Daily));

        Assert.AreEqual(0m, 0m.CalculateCompoundInterest(decimal.MaxValue, int.MaxValue, InterestPeriod.Daily));
        Assert.AreEqual(0m, 0m.CalculateCompoundInterestAmount(decimal.MaxValue, int.MaxValue, InterestPeriod.Daily));
    }

    private static void AssertAllThrowForParameter(string parameterName, params Action[] calculations)
    {
        foreach (var calculation in calculations)
        {
            var exception = Assert.ThrowsExactly<ArgumentOutOfRangeException>(calculation);
            Assert.AreEqual(parameterName, exception.ParamName);
        }
    }
}
