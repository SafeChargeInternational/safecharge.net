namespace Safecharge.Response.Payment
{
    /// <summary>
    /// Holder for APM data response.
    /// </summary>
    public class AlternativePaymentMethodResponse
    {
        public string ExternalAccountId { get; set; }

        public string ExternalAccountDescription { get; set; }

        public string ExternalTransactionId { get; set; }

        public string ApmReferenceId { get; set; }

        public string OrderTransactionId { get; set; }

        public string ApmPayerInfo { get; set; }

        public string PaymentMethod { get; set; }
    }
}
