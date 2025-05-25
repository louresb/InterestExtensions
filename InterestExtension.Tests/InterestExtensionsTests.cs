using InterestExtension.Enums;

namespace InterestExtension.Tests;

[TestClass]
public class InterestExtensionsTests
{
    [TestMethod]
    public void ShouldCalculateSimpleInterestCorrectly() =>
        Assert.AreEqual(102.80m, 100m.CalculateSimpleInterest(0.004m, 7));

    [TestMethod]
    public void ShouldCalculateCompoundInterestCorrectly() =>
        Assert.AreEqual(102.83m, Math.Round(100m.CalculateCompoundInterest(0.004m, 7), 2));

    [TestMethod]
    public void ShouldCalculateCompoundInterestWithMonthlyCompounding()
    {
        var result = Math.Round(100m.CalculateCompoundInterest(0.004m, 7, InterestPeriod.Monthly), 2);
        Assert.AreEqual(102.84m, result); 
    }

    [TestMethod]
    public void ShouldCalculateSimpleInterestAmountCorrectly() =>
        Assert.AreEqual(2.80m, 100m.CalculateSimpleInterestAmount(0.004m, 7));

    [TestMethod]
    public void ShouldCalculateCompoundInterestAmountWithDailyCompounding()
    {
        var result = Math.Round(100m.CalculateCompoundInterestAmount(0.004m, 7, InterestPeriod.Daily), 2);
        Assert.AreEqual(2.84m, result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ShouldThrowOnNegativePrincipalSimple()
    {
        _ = (-1m).CalculateSimpleInterest(0.05m, 1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ShouldThrowOnNegativePeriodCompound()
    {
        _ = 100m.CalculateCompoundInterest(0.05m, -1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ShouldThrowOnInvalidPeriodType()
    {
        _ = 100m.CalculateCompoundInterest(0.05m, 1, (InterestPeriod)999);
    }
}