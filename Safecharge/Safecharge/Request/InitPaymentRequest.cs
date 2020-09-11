using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels.InitPayment;
using Safecharge.Request.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>
    /// Request for initiation of payment process for transactions.
    /// </summary>
    public class InitPaymentRequest : SafechargeRequest
    {
        private string orderId;
        private string userTokenId;
        private string clientUniqueId;
        private string currency;
        private string amount;
        private string customData;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public InitPaymentRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InitPaymentRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="paymentOption">Details about the payment method.</param>
        public InitPaymentRequest(
            MerchantInfo merchantInfo,
            string sessionToken,
            string currency,
            string amount,
            InitPaymentPaymentOption paymentOption)
            : base(merchantInfo, ChecksumOrderMapping.ApiGenericChecksumMapping, sessionToken)
        {
            this.Currency = currency;
            this.Amount = amount;
            this.PaymentOption = paymentOption;
            this.RequestUri = this.CreateRequestUri(ApiConstants.InitPaymentUrl);
        }

        public string OrderId
        {
            get { return this.orderId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringId, nameof(this.OrderId));
                this.orderId = value;
            }
        }

        public string UserTokenId
        {
            get { return this.userTokenId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.UserTokenId));
                this.userTokenId = value;
            }
        }

        public string ClientUniqueId
        {
            get { return this.clientUniqueId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.ClientUniqueId));
                this.clientUniqueId = value;
            }
        }

        /// <summary>
        /// The three character ISO currency code.
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
        /// The transaction amount.
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

        public InitPaymentPaymentOption PaymentOption { get; set; }

        public UrlDetails UrlDetails { get; set; }

        public string CustomData
        {
            get { return this.customData; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomData));
                this.customData = value;
            }
        }

        public UserAddress BillingAddress { get; set; }
    }
}
