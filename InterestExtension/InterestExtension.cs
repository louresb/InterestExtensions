namespace InterestExtension
{
    public static class InterestCalculator
    {
        public static decimal CalculateSimpleInterest(this decimal principal, decimal interestRate, int period)
        {
            decimal interest = principal * interestRate * period;
            decimal simpleAmount = principal + interest;
            return simpleAmount;
        }

        public static decimal CalculateCompoundInterest(this decimal principal, decimal interestRate, int period)
        {
            decimal compoundAmount = principal * (decimal)Math.Pow(1 + (double)interestRate, period);
            decimal interest = compoundAmount - principal;
            return compoundAmount;
        }
    }
}
