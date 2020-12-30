using Safecharge.Model.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request.Common.Transaction
{
    /// <summary>
    /// Abstract class to be used as a base for transaction related requests.
    /// </summary>
    public abstract class SafechargeTransactionRequest : SafechargeRequest
    {
        private string amount;
        private string currency;
        private string comment;
        private string clientUniqueId;
        private string relatedTransactionId;
        private string customSiteName;
        private string productId;
        private string customData;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public SafechargeTransactionRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafechargeTransactionRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="checksumOrderMapping">Type of checksum.</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="relatedTransactionId"></param>
        public SafechargeTransactionRequest(
            MerchantInfo merchantInfo,
            ChecksumOrderMapping checksumOrderMapping,
            string sessionToken,
            string currency,
            string amount,
            string relatedTransactionId)
            : base(merchantInfo, checksumOrderMapping, sessionToken)
        {
            this.Currency = currency;
            this.Amount = amount;
            this.RelatedTransactionId = relatedTransactionId;
        }

        /// <summary>
        /// The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.
        /// </summary>
        public string Amount
        {
            get { return this.amount; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.Amount));
                this.amount = value;
            }
        }

        /// <summary>
        /// The three character ISO currency code of the transaction.
        /// </summary>
        public string Currency
        {
            get { return this.currency; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.Currency));
                this.currency = value;
            }
        }

        /// <summary>
        /// The authorization code of the related auth transaction, to be compared to the original one.
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// Enables the addition of a free text comment to the request.
        /// </summary>
        public string Comment
        {
            get { return this.comment; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.Comment));
                this.comment = value;
            }
        }

        /// <summary>
        /// ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: 
        /// reconciliation, identifying the transaction in the event of any issues, etc.
        /// </summary>
        public string ClientUniqueId
        {
            get { return this.clientUniqueId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringId, nameof(this.ClientUniqueId));
                this.clientUniqueId = value;
            }
        }

        /// <summary>
        /// The ID of the original auth transaction.
        /// </summary>
        public string RelatedTransactionId
        {
            get { return this.relatedTransactionId; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.RelatedTransactionId));
                this.relatedTransactionId = value;
            }
        }

        /// <summary>
        /// URLs to redirect to in case of success, failure, etc. Also URL to send the direct merchant notification(DMN) message to.
        /// </summary>
        public UrlDetails UrlDetails { get; set; }

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
    }
}
