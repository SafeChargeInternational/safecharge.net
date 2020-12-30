using Safecharge.Model.Common;
using Safecharge.Request.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>
    ///  Request to retrieve the available payment option for a specific merchant.
    /// </summary>
    /// <remarks>
    ///  Allows the merchant view the names, IDs and other information regarding the enabled payment methods and APMs,
    ///  which may be filtered based on country, currency and language.
    ///  It may be used by the merchant mostly in order to display the available payment methods in its payment page.
    /// </remarks>
    public class GetMerchantPaymentMethodsRequest : SafechargeRequest
    {
        private string currencyCode;
        private string countryCode;
        private string languageCode;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public GetMerchantPaymentMethodsRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMerchantPaymentMethodsRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="clientRequestId">Use this advanced field to prevent idempotency. Use it to uniquely identify the request you are submitting. If our system receives two calls with the same clientRequestId, it refuses the second call as it will assume idempotency.</param>
        public GetMerchantPaymentMethodsRequest(
            MerchantInfo merchantInfo,
            string sessionToken,
            string clientRequestId)
            : base(merchantInfo, ChecksumOrderMapping.ApiBasicChecksumMapping, sessionToken)
        {
            this.ClientRequestId = clientRequestId;
            this.RequestUri = this.CreateRequestUri(ApiConstants.GetMerchantPaymentMethodsUrl);
        }

        /// <summary>
        /// The three letter ISO currency code that the transaction is to be completed in.
        /// </summary>
        public string CurrencyCode
        {
            get { return this.currencyCode; }
            set
            {
                Guard.RequiresLength(value?.Length, 3, nameof(this.CurrencyCode));
                this.currencyCode = value;
            }
        }
        /// <summary>
        /// The two-letter ISO country code the transaction is to be completed in.
        /// </summary>
        public string CountryCode
        {
            get { return this.countryCode; }
            set
            {
                Guard.RequiresLength(value?.Length, 2, nameof(this.CountryCode));
                this.countryCode = value;
            }
        }

        /// <summary>
        /// The language the transaction is to be completed in.
        /// </summary>
        public string LanguageCode
        {
            get { return this.languageCode; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.LanguageCode));
                this.languageCode = value;
            }
        }

        /// <summary>
        /// The type of the payment methods to be returned.
        /// Possible values: DEPOSIT, WITHDRAWAL. If no value sent, then default value is DEPOSIT.
        /// </summary>
        public string Type { get; set; }
    }
}
