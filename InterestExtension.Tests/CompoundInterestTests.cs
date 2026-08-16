using InterestExtension.Enums;

namespace InterestExtension.Tests;

[TestClass]
public sealed class CompoundInterestTests
{
    [TestMethod]
    public void CalculateCompoundInterestReturnsExactAnnualResult()
        => Assert.AreEqual(102.8338248981532688384m, 100m.CalculateCompoundInterest(0.004m, 7));

    [TestMethod]
    public void CalculateCompoundInterestReturnsExactMonthlyResult()
        => Assert.AreEqual(
            102.83908863189753692594868626m,
            100m.CalculateCompoundInterest(0.004m, 7, InterestPeriod.Monthly));

    [TestMethod]
    public void CalculateCompoundInterestReturnsExactDailyResult()
        => Assert.AreEqual(
            102.83955266413341871521872628m,
            100m.CalculateCompoundInterest(0.004m, 7, InterestPeriod.Daily));

    [TestMethod]
    public void AnnualOverloadsAreEquivalent()
    {
        var totalWithDefault = 100m.CalculateCompoundInterest(0.05m, 5);
        var totalWithPeriod = 100m.CalculateCompoundInterest(0.05m, 5, InterestPeriod.Yearly);
        var interestWithDefault = 100m.CalculateCompoundInterestAmount(0.05m, 5);
        var interestWithPeriod = 100m.CalculateCompoundInterestAmount(0.05m, 5, InterestPeriod.Yearly);

        Assert.AreEqual(totalWithPeriod, totalWithDefault);
        Assert.AreEqual(interestWithPeriod, interestWithDefault);
    }

    [TestMethod]
    [DataRow(InterestPeriod.Yearly)]
    [DataRow(InterestPeriod.Monthly)]
    [DataRow(InterestPeriod.Daily)]
    public void InterestAmountEqualsTotalMinusPrincipal(InterestPeriod periodType)
    {
        const decimal principal = 250m;
        var total = principal.CalculateCompoundInterest(0.0375m, 9, periodType);
        var interest = principal.CalculateCompoundInterestAmount(0.0375m, 9, periodType);

        Assert.AreEqual(total - principal, interest);
    }

    [TestMethod]
    public void ZeroRateOrPeriodLeavesPrincipalUnchanged()
    {
        Assert.AreEqual(100m, 100m.CalculateCompoundInterest(0m, 7));
        Assert.AreEqual(100m, 100m.CalculateCompoundInterest(0.05m, 0, InterestPeriod.Daily));
        Assert.AreEqual(0m, 100m.CalculateCompoundInterestAmount(0m, 7));
        Assert.AreEqual(0m, 100m.CalculateCompoundInterestAmount(0.05m, 0, InterestPeriod.Monthly));
    }

    [TestMethod]
    public void ZeroPrincipalReturnsZero()
    {
        Assert.AreEqual(0m, 0m.CalculateCompoundInterest(0.05m, 10, InterestPeriod.Monthly));
        Assert.AreEqual(0m, 0m.CalculateCompoundInterestAmount(0.05m, 10, InterestPeriod.Daily));
    }

    [TestMethod]
    public void ResultsAreNotRoundedByTheLibrary()
    {
        var result = 100m.CalculateCompoundInterest(0.004m, 7, InterestPeriod.Monthly);

        Assert.AreNotEqual(decimal.Round(result, 2), result);
    }
}
