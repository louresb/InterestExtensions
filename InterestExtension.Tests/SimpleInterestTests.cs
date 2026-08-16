namespace InterestExtension.Tests;

[TestClass]
public sealed class SimpleInterestTests
{
    [TestMethod]
    public void CalculateSimpleInterestReturnsTotalAmount()
        => Assert.AreEqual(102.80m, 100m.CalculateSimpleInterest(0.004m, 7));

    [TestMethod]
    public void CalculateSimpleInterestAmountReturnsOnlyEarnedInterest()
        => Assert.AreEqual(2.80m, 100m.CalculateSimpleInterestAmount(0.004m, 7));

    [TestMethod]
    public void ZeroRateOrPeriodLeavesPrincipalUnchanged()
    {
        Assert.AreEqual(100m, 100m.CalculateSimpleInterest(0m, 7));
        Assert.AreEqual(100m, 100m.CalculateSimpleInterest(0.05m, 0));
        Assert.AreEqual(0m, 100m.CalculateSimpleInterestAmount(0m, 7));
        Assert.AreEqual(0m, 100m.CalculateSimpleInterestAmount(0.05m, 0));
    }

    [TestMethod]
    public void ZeroPrincipalReturnsZero()
    {
        Assert.AreEqual(0m, 0m.CalculateSimpleInterest(0.05m, 10));
        Assert.AreEqual(0m, 0m.CalculateSimpleInterestAmount(0.05m, 10));
    }
}
