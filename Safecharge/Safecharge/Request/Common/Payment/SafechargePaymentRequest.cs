using Safecharge.Model.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request.Common.Payment
{
    /// <summary>
    /// Abstract class to be used as a base for payment requests.
    /// </summary>
    public abstract class SafechargePaymentRequest : SafechargeOrderDetailsRequest
    {
        private string customSiteName;
        private string productId;
        private string customData;
        private string relatedTransactionId;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public SafechargePaymentRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafechargePaymentRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="checksumOrderMapping">Type of checksum.</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        public SafechargePaymentRequest(
            MerchantInfo merchantInfo,
            ChecksumOrderMapping checksumOrderMapping,
            string sessionToken,
            string currency,
            string amount)
            : base(merchantInfo, checksumOrderMapping, sessionToken, currency, amount)
        {
        }

        /// <summary>
        /// Transaction Type of the request. Possible values for payment request: Auth / Sale / PreAuth.
        /// </summary>
        public string TransactionType { get; set; }

        public string CustomSiteName
        {
            get { return this.customSiteName; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 50, nameof(this.CustomSiteName));
                this.customSiteName = value;
            }
        }

        public string ProductId
        {
            get { return this.productId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 50, nameof(this.ProductId));
                this.productId = value;
            }
        }

        public string CustomData
        {
            get { return this.customData; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomData));
                this.customData = value;
            }
        }

        public string RelatedTransactionId
        {
            get { return this.relatedTransactionId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 19, nameof(this.RelatedTransactionId));
                this.relatedTransactionId = value;
            }
        }
    }
}
