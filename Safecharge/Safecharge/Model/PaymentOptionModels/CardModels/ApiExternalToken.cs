using Newtonsoft.Json;

namespace Safecharge.Model.PaymentOptionModels.CardModels
{
    /// <summary>
    /// Holder for Checkout Page's Payment Gateway external token data.
    /// Parameters arriving from a third-party payment provider that also generate card tokens
    /// (currently relevant only to CreditGuard as a third-party payment provider which generate card tokens).
    /// </summary>
    public class ApiExternalToken
    {
        [JsonProperty("externalToken_tokenValue")]
        public string TokenValue { get; set; }

        [JsonProperty("externalToken_cardBin")]
        public string CardBin { get; set; }

        [JsonProperty("externalToken_cardMask")]
        public string CardMask { get; set; }

        [JsonProperty("externalToken_cardLength")]
        public string CardLength { get; set; }

        [JsonProperty("externalToken_cardName")]
        public string CardName { get; set; }

        [JsonProperty("externalToken_cardExpiration")]
        public string CardExpiration { get; set; }

        [JsonProperty("externalToken_cardTypeId")]
        public string CardTypeId { get; set; }

        [JsonProperty("externalToken_cardTypeName")]
        public string CardTypeName { get; set; }

        [JsonProperty("externalToken_creditCompanyId")]
        public string CreditCompanyId { get; set; }

        [JsonProperty("externalToken_creditCompanyName")]
        public string CreditCompanyName { get; set; }

        [JsonProperty("externalToken_cardBrandId")]
        public string CardBrandId { get; set; }

        [JsonProperty("externalToken_cardBrandName")]
        public string CardBrandName { get; set; }

        [JsonProperty("externalToken_cardAcquirerId")]
        public string CardAcquirerId { get; set; }

        [JsonProperty("externalToken_cardAcquirerName")]
        public string CardAcquirerName { get; set; }

        [JsonProperty("externalToken_blockedCard")]
        public string BlockedCard { get; set; }

        [JsonProperty("externalToken_extendedCardType")]
        public string ExtendedCardType { get; set; }

        [JsonProperty("externalToken_clubName")]
        public string ClubName { get; set; }

        [JsonProperty("externalToken_Indication")]
        public string Indication { get; set; }

        [JsonProperty("externalToken_tokenProvider")]
        public string TokenProvider { get; set; }
    }
}
