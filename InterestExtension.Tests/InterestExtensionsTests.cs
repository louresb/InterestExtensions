namespace InterestExtension.Tests
{
    [TestClass]
    public class InterestExtensionsTests
    {
        [TestMethod]
        public void ShouldCalculateSimpleInterestCorrectly()
        {
            decimal principal = 100;
            decimal interestRate = 0.004m;
            int period = 7;

            decimal result = principal.CalculateSimpleInterest(interestRate, period);

            decimal expected = 102.80m;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldCalculateCompoundInterestCorrectly()
        {
            decimal principal = 100;
            decimal interestRate = 0.004m;
            int period = 7;

            decimal result = principal.CalculateCompoundInterest(interestRate, period);

            decimal expected = 102.83m;
            Assert.AreEqual(expected, Math.Round(result, 2));
        }
    }
}