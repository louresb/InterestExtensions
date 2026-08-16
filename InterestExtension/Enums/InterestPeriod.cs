namespace InterestExtension.Enums;

/// <summary>
/// Specifies how often compound interest is applied within a year.
/// </summary>
public enum InterestPeriod
{
    /// <summary>
    /// Compounds once per year.
    /// </summary>
    Yearly,

    /// <summary>
    /// Compounds twelve times per year.
    /// </summary>
    Monthly,

    /// <summary>
    /// Compounds 365 times per year.
    /// </summary>
    Daily
}
