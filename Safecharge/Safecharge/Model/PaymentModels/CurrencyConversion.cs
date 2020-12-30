namespace Safecharge.Model.PaymentModels
{
    /// <summary>
    /// Holder for currency conversion information.
    /// </summary>
    public class CurrencyConversion
    {
        public string Type { get; set; }

        public string OriginalAmount { get; set; }

        public string OriginalCurrency { get; set; }
    }
}
