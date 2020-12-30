namespace Safecharge.Model.Common
{
    /// <summary>
    /// Holder for AmountDetails info.
    /// </summary>
    /// <remarks>
    /// The items and amountDetails prices should be summed up in the amount parameter and sent separately.
    /// All prices must be in the same currency.
    /// </remarks>
    public class AmountDetails
    {
        public string TotalHandling { get; set; }

        public string TotalShipping { get; set; }

        public string TotalTax { get; set; }

        public string TotalDiscount { get; set; }
    }
}
