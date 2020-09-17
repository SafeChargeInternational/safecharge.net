namespace Safecharge.Response.Payment
{
    /// <summary>
    /// Holder for Payment option data response.
    /// </summary>
    public class PaymentOptionResponse
    {
        public string RedirectUrl { get; set; }

        public string UserPaymentOptionId { get; set; }

        public CardResponse Card { get; set; }

        public AlternativePaymentMethodResponse AlternativePaymentMethod { get; set; }
    }
}
