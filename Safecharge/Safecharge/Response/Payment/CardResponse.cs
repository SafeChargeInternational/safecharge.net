using Safecharge.Model.PaymentOptionModels.CardModels;

namespace Safecharge.Response.Payment
{
    /// <summary>
    /// Holder for card data response.
    /// </summary>
    public class CardResponse
    {
        public ApiExternalToken ExternalToken { get; set; }

        public string CcCardNumber { get; set; }

        public string Bin { get; set; }

        public string LastFourDigits { get; set; }

        public string CcExpMonth { get; set; }

        public string CcExpYear { get; set; }

        public string AcquirerId { get; set; }

        public string Cvv2Reply { get; set; }

        public string AvsCode { get; set; }

        public string CcTempToken { get; set; }

        public string IsVerified { get; set; }

        public ThreeDResponse ThreeD { get; set; }
    }
}
