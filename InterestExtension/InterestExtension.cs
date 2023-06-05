namespace InterestExtension
{
    public static class InterestCalculator
    {
        public static decimal CalculateSimpleInterest(this decimal principal, decimal interestRate, int period)
        {
            decimal interest = principal * interestRate * period;
            decimal finalAmount = principal + interest;
            return finalAmount;
        }

        public static decimal CalculateCompoundInterest(this decimal principal, decimal interestRate, int period)
        {
            decimal finalAmount = principal * (decimal)Math.Pow(1 + (double)interestRate, period);
            decimal interest = finalAmount - principal;
            return finalAmount;
        }
    }
}
