using Safecharge.Response.Common;

namespace Safecharge.Response
{
    /// <summary>
    /// Response received from the SafeCharge's servers to the <see cref="Request.GetCardDetailsRequest"/>.
    /// </summary>
    public class GetCardDetailsResponse : SafechargeResponse
    {
        public string Brand { get; set; }

        public string CardType { get; set; }

        public string Program { get; set; }

        public string VisaDirectSupport { get; set; }

        public string DccAllowed { get; set; }

        public string IssuerCountry { get; set; }

        public string Currency { get; set; }
    }
}
