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

        public string Last4Digits { get; set; }

        public string CcExpMonth { get; set; }

        public string CcExpYear { get; set; }

        public string AcquirerId { get; set; }

        public string Cvv2Reply { get; set; }

        public string AvsCode { get; set; }

        public string CcTempToken { get; set; }

        public string IsVerified { get; set; }

        public ThreeDResponse ThreeD { get; set; }

        public string CardType { get; set; }

        public string CardBrand { get; set; }

        public string UniqueCc { get; set; }
    }
}
